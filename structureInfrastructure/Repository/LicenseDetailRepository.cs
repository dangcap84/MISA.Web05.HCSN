using Dapper;
using MISA.Web05.Core.Modals;
using MISA.Web05.Interfaces.Repository;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web05.Infrastructure.Repository
{
    public class LicenseDetailRepository : BaseRepository<LicenseDetail>, ILicenseDetailRepository
    {
        /// <summary>
        /// Override lại hàm get dữ liệu của Base
        /// </summary>
        /// <returns>Danh sách tài sản</returns>
        /// NDHoang(04/07/2022)
        public override IEnumerable<LicenseDetail> Get()
        {
            using (sqlConnection = new MySqlConnection(connectionString))
            {
                var sql = $"Proc_GetLicenseDetail";
                var licenseDetails = sqlConnection.Query<LicenseDetail>(sql, commandType: System.Data.CommandType.StoredProcedure);
                return licenseDetails;
            }
        }

        /// <summary>
        /// Lấy ra 1 mảng detail từ 1 id bảng master
        /// </summary>
        /// <param name="licenseId"></param>
        /// <returns></returns>
        public List<object> GetDetailAssets(Guid licenseId)
        {
            using (sqlConnection = new MySqlConnection(connectionString))
            {
                var sqlCommand = "SELECT * FROM View_LicenseDetail vld WHERE vld.LicenseId = @LicenseId";
                var parameter = new DynamicParameters();
                parameter.Add("@LicenseId", licenseId);
                var res = sqlConnection.Query<object>(sqlCommand, parameter);
                return res.ToList();
            }
        }

        /// <summary>
        /// Lấy ra 1 bản ghi có cả master, cả detail để bind vào form
        /// </summary>
        /// <param name="licenseId"></param>
        /// <returns></returns>
        public object GetLicenseInsertById(Guid licenseId)
        {
            using (sqlConnection = new MySqlConnection(connectionString))
            {
                var sqlGetLicenseMaster = "SELECT * FROM License WHERE LicenseId = @LicenseId";
                var parameter = new DynamicParameters();
                parameter.Add("@LicenseId", licenseId);
                var license = sqlConnection.QueryFirstOrDefault<License>(sqlGetLicenseMaster, parameter);
                var sqlGetLicenseDetail = "SELECT* FROM View_LicenseDetail vld WHERE vld.LicenseId = @LicenseId";
                var detailAssets = sqlConnection.Query<object>(sqlGetLicenseDetail, parameter);

                var res = new
                {
                    LicenseId = license.LicenseId,
                    LicenseCode = license.LicenseCode,
                    UseDate = license.UseDate,
                    WriteUpdate = license.WriteUpdate,
                    Description = license.Description,
                    Total = license.Total,
                    detailAssets = detailAssets.ToArray()
                };
                return res;
            }
        }

        /// <summary>
        /// Lấy ra bộ phận sử dụng và detail json từ detail json
        /// </summary>
        /// <param name="licenseDetailId"></param>
        /// <returns></returns>
        public object GetMoneySource(Guid licenseDetailId)
        {
            using (sqlConnection = new MySqlConnection(connectionString))
            {
                var sqlGet = $"SELECT ld.DetailJson, vfa.DepartmentName, vfa.FixedAssetName FROM LicenseDetail ld LEFT JOIN view_fixedasset vfa ON ld.FixedAssetId " +
                $"= vfa.FixedAssetId WHERE ld.LicenseDetailId = @LicenseDetailId";
                var parameter = new DynamicParameters();
                parameter.Add("@LicenseDetailId", licenseDetailId);
                var res = sqlConnection.QueryFirstOrDefault<object>(sqlGet, parameter);
                return res;
            }
        }

        /// <summary>
        /// Thực hiên phân trang
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns>Danh sách tài sản theo điều kiện tương ứng</returns>
        /// <exception cref="NotImplementedException"></exception>
        /// NDHoang(04/07/2022)
        public object GetPaging(int pageIndex, int pageSize, string? filter = "", string LicenseId = "")
        {
            using (sqlConnection = new MySqlConnection(connectionString))
            {
                var sql = $"Proc_PagingLicenseDetail";
                //Khởi tạo parameters
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@h_Filter", filter);
                parameters.Add("@h_PageIndex", pageIndex);
                parameters.Add("@h_LicenseId", LicenseId);
                parameters.Add("@h_PageSize", pageSize);
                parameters.Add("@h_TotalRecord", direction: System.Data.ParameterDirection.Output);
                parameters.Add("@h_RecordStart", direction: System.Data.ParameterDirection.Output);
                parameters.Add("@h_RecordEnd", direction: System.Data.ParameterDirection.Output);

                var fixedAssets = sqlConnection.Query<FixedAsset>(sql, param: parameters, commandType: System.Data.CommandType.StoredProcedure);
                var licenseDetails = sqlConnection.Query<LicenseDetail>(sql, param: parameters, commandType: System.Data.CommandType.StoredProcedure);
                //Truyền dữ liệu vào out put
                int totalRecord = parameters.Get<int>("@h_TotalRecord");
                int recordStart = parameters.Get<int>("@h_RecordStart");
                int recordEnd = parameters.Get<int>("@h_RecordEnd");
                //Đầu ra
                return new
                {
                    TotalRecord = totalRecord,
                    RecordStart = recordStart,
                    RecordEnd = recordEnd,
                    Data = fixedAssets,
                    Id = licenseDetails
                };
            }
        }

        /// <summary>
        /// Thêm mảng license detail
        /// </summary>
        public object MultiInsert(LicenseDetail[] licenseDetails, Guid licenseId)
        {


            var count = 0;

            for (int i = 0; i < licenseDetails.Length; i++)
            {
                licenseDetails[i].LicenseId = licenseId;
                licenseDetails[i].LicenseDetailId = Guid.NewGuid();
                int res = Insert(licenseDetails[i]);

                count += res;            }

            return count;
        }

        /// <summary>
        /// Sửa 1 bản ghi Json detail
        /// </summary>
        /// <param name="licenseInsert"></param>
        /// <returns></returns>
        public int UpdateLicenseDetail(Guid licenseDetailId, string detailJson)
        {
            using (sqlConnection = new MySqlConnection(connectionString))
            {
                var sqlUpdate = "UPDATE LicenseDetail SET DetailJson = @DetailJson WHERE LicenseDetailId = @LicenseDetailId";
                var parameter = new DynamicParameters();
                parameter.Add("@DetailJson", detailJson);
                parameter.Add("@LicenseDetailId", licenseDetailId);
                var res = sqlConnection.Execute(sqlUpdate, parameter);
                return res;
            }
        }
    }
}
