using Dapper;
using MISA.Web05.Interfaces.Repository;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web05.Infrastructure.Repository
{
    public class LicenseAddRepository : BaseRepository<LicenseAdd>, ILicenseAddRepository
    {

        /// <summary>
        /// Kiểm tra tồn tại mã tài sản
        /// </summary>
        /// <param name="fixedAssetCode">Đối tượng tham chiếu</param>
        /// <param name="fixedAssetId">Đối tượng tham chiếu</param>
        /// <returns>false: không tồn tại mã, true: đã có mã</returns>
        /// NDHoang(04/07/2022)
        public bool CheckUpdateLicenseCodeExis(string licenseCode, Guid licenseId)
        {
            using (sqlConnection = new MySqlConnection(connectionString))
            {
                var sql = "SELECT LicenseCode FROM License WHERE LicenseCode = @licenseCode AND LicenseId != @licenseId";
                var parameter = new DynamicParameters();
                parameter.Add("@licenseCode", licenseCode);
                parameter.Add("@licenseId", licenseId);
                var res = sqlConnection.QueryFirstOrDefault(sql: sql, param: parameter);
                if (res == null)
                    return false;
                return true;
            }
        }
        
        /// <summary>
        /// Thêm mảng master-detail
        /// </summary>
        /// <param name="licenseInsert"></param>
        /// <returns></returns>
        public object MultiAdd(LicenseAdd licenseInsert)
        {
            using (sqlConnection = new MySqlConnection(connectionString))
            {
                //License license = new License();
                var parameter = new DynamicParameters();
                licenseInsert.LicenseId = Guid.NewGuid();

                var sqlInsertMaster = $"INSERT INTO License (LicenseId, LicenseCode, UseDate, WriteUpdate, Description, Total) VALUES " +
                    "(@LicenseId, @LicenseCode, @UseDate, @WriteUpdate, @Description, @Total)";
                parameter.Add("@LicenseId", licenseInsert.LicenseId);
                parameter.Add("@LicenseCode", licenseInsert.LicenseCode);
                parameter.Add("@UseDate", licenseInsert.UseDate);
                parameter.Add("@WriteUpdate", licenseInsert.WriteUpdate);
                parameter.Add("@Description", licenseInsert.Description);
                parameter.Add("@Total", licenseInsert.Total);
                var masterRes = sqlConnection.Execute(sqlInsertMaster, parameter);

                var count = 0;

                for (int i = 0; i < licenseInsert.licenseDetails.Length; i++)
                {
                    // Sinh Id không trùng cho licenseDetail
                    licenseInsert.licenseDetails[i].LicenseDetailId = Guid.NewGuid();

                    var sqlInsertDetail = $"INSERT INTO LicenseDetail (LicenseDetailId, LicenseId, FixedAssetId, DetailJson) VALUES (@LicenseDetailId,@LicenseId,@FixedAssetId,@DetailJson)";

                    parameter.Add("@FixedAssetId", licenseInsert.licenseDetails[i].FixedAssetId);
                    parameter.Add("@LicenseId", licenseInsert.LicenseId);
                    parameter.Add("@LicenseDetailId", licenseInsert.licenseDetails[i].LicenseDetailId);
                    parameter.Add("@DetailJson", licenseInsert.licenseDetails[i].DetailJson);
                    var res = sqlConnection.Execute(sqlInsertDetail, parameter);
                    count += res;
                }


                var lastRes = new
                {
                    detail = count,
                    masterRes = masterRes
                };
                return lastRes;
            }
        }

        /// <summary>
        /// Sửa 1 bản ghi master - detail
        /// </summary>
        /// <param name="licenseInsert"></param>
        /// <returns></returns>
        public object UpdateLicenseAdd(Guid licenseId, LicenseAdd licenseAdd)
        {
            using (sqlConnection = new MySqlConnection(connectionString))
            {
                var parameter = new DynamicParameters();

                var sqlUpdateMaster = $"UPDATE License SET LicenseCode = @LicenseCode, UseDate = @UseDate, WriteUpdate = @WriteUpdate, " +
                    $"Description = @Description, Total = @Total WHERE LicenseId = @LicenseId";
                parameter.Add("@LicenseId", licenseId);
                parameter.Add("@LicenseCode", licenseAdd.LicenseCode);
                parameter.Add("@UseDate", licenseAdd.UseDate);
                parameter.Add("@WriteUpdate", licenseAdd.WriteUpdate);
                parameter.Add("@Description", licenseAdd.Description);
                parameter.Add("@Total", licenseAdd.Total);
                // Sửa bán ghi license master
                var masterRes = sqlConnection.Execute(sqlUpdateMaster, parameter);

                var sqlOldLicenseDetails = $"SELECT * FROM LicenseDetail WHERE LicenseId = @LicenseId";
                var oldLicenseDetails = sqlConnection.Query<LicenseDetail>(sqlOldLicenseDetails, parameter);
                // Filter từ mảng cũ thành mảng mới vừa được push lên, tài sản nào cũ có rồi thì để nguyên, chưa có thì xóa đi để push cái mới
                for (int i = 0; i < oldLicenseDetails.Count(); i++)
                {
                    var check = true;
                    //Chạy qua mảng mới kiểm tra xem tài sản nào đã có rồi thì bỏ ra khỏi mảng mới
                    for (int j = 0; j < licenseAdd.licenseDetails.Length; j++)
                    {
                        if (oldLicenseDetails.ElementAt(i).FixedAssetId.Equals(licenseAdd.licenseDetails[j].FixedAssetId) == true)
                        {
                            parameter.Add("@DetailJson", licenseAdd.licenseDetails[j].DetailJson);
                            parameter.Add("@FixedAssetId", licenseAdd.licenseDetails[j].FixedAssetId);
                            var sqlUpdateDetail = $"UPDATE LicenseDetail SET DetailJson = @DetailJson WHERE LicenseId = @LicenseId AND FixedAssetId = @FixedAssetId";
                            var resUpdateDetail = sqlConnection.Execute(sqlUpdateDetail, parameter);
                            licenseAdd.licenseDetails = licenseAdd.licenseDetails.Where(val => val.FixedAssetId != licenseAdd.licenseDetails[j].FixedAssetId).ToArray();
                            check = false;
                            break;
                        }
                    }
                    //Nếu không có tài sản nào trùng thì xóa bỏ mảng tài sản cũ đi
                    if (check == true)
                    {
                        parameter.Add("@FixedAssetId", oldLicenseDetails.ElementAt(i).FixedAssetId);
                        var deleteOldDetail = $"DELETE FROM LicenseDetail WHERE LicenseId = @LicenseId AND FixedAssetId = @FixedAssetId";
                        var resDeleteOldDetail = sqlConnection.Execute(deleteOldDetail, parameter);
                    }
                }

                //Nếu có tài sản mới thì thêm vào bảng detail
                var count = 0;
                for (int i = 0; i < licenseAdd.licenseDetails.Length; i++)
                {
                    // Sinh Id không trùng cho licenseDetail
                    licenseAdd.licenseDetails[i].LicenseDetailId = Guid.NewGuid();

                    var sqlInsertDetail = $"INSERT INTO LicenseDetail (LicenseDetailId, LicenseId, FixedAssetId, DetailJson) VALUES (@LicenseDetailId,@LicenseId,@FixedAssetId,@DetailJson)";

                    parameter.Add("@FixedAssetId", licenseAdd.licenseDetails[i].FixedAssetId);
                    parameter.Add("@LicenseId", licenseId);
                    parameter.Add("@LicenseDetailId", licenseAdd.licenseDetails[i].LicenseDetailId);
                    parameter.Add("@DetailJson", licenseAdd.licenseDetails[i].DetailJson);
                    var res = sqlConnection.Execute(sqlInsertDetail, parameter);
                    count += res;
                }
                return new
                {
                    masterRes = masterRes,
                    detailRes = count
                };
            }
        }
    }
}
