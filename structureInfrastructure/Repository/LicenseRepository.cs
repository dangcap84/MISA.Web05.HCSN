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
    public class LicenseRepository : BaseRepository<License>, ILicenseRepository
    {
        #region Methods
        /// <summary>
        /// Kiểm tra tồn tại mã tài sản
        /// </summary>
        /// <param name="fixedAssetCode">Đối tượng tham chiếu</param>
        /// <returns>false: không tồn tại mã, true: đã có mã</returns>
        /// NDHoang(04/07/2022)
        public bool CheckLicenseCodeExis(string licenseCode)
        {
            using (sqlConnection = new MySqlConnection(connectionString))
            {
                var sql = "SELECT LicenseCode FROM License WHERE LicenseCode = @licenseCode";
                var parameter = new DynamicParameters();
                parameter.Add("@licenseCode", licenseCode);
                var res = sqlConnection.QueryFirstOrDefault(sql: sql, param: parameter);
                if (res == null)
                    return false;
                return true;
            }
        }

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
        /// Override lại hàm get dữ liệu của Base
        /// </summary>
        /// <returns>Danh sách tài sản</returns>
        /// NDHoang(04/07/2022)
        public override IEnumerable<License> Get()
        {
            using (sqlConnection = new MySqlConnection(connectionString))
            {
                var sql = $"Proc_GetLicense";
                var licenses = sqlConnection.Query<License>(sql, commandType: System.Data.CommandType.StoredProcedure);
                return licenses;
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
        public object GetPaging(int pageIndex, int pageSize, string? filter = "")
        {
            using (sqlConnection = new MySqlConnection(connectionString))
            {
                var sql = $"Proc_PagingLicense";
                //Khởi tạo parameters
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@h_Filter", filter);
                parameters.Add("@h_PageIndex", pageIndex);
                parameters.Add("@h_PageSize", pageSize);
                parameters.Add("@h_TotalRecord", direction: System.Data.ParameterDirection.Output);
                parameters.Add("@h_RecordStart", direction: System.Data.ParameterDirection.Output);
                parameters.Add("@h_RecordEnd", direction: System.Data.ParameterDirection.Output);

                var licenses = sqlConnection.Query<License>(sql, param: parameters, commandType: System.Data.CommandType.StoredProcedure);
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
                    Data = licenses
                };
            }
        }
        #endregion
    }
}
