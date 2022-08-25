using Dapper;
using MySqlConnector;
using MISA.Web05;
using MISA.Web05.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web05.Infrastructure.Repository
{
    public class FixedAssetRepository : BaseRepository<FixedAsset>, IFixedAssetRepository
    {
        #region Method
        /// <summary>
        /// Kiểm tra tồn tại mã tài sản
        /// </summary>
        /// <param name="fixedAssetCode">Đối tượng tham chiếu</param>
        /// <returns>false: không tồn tại mã, true: đã có mã</returns>
        /// NDHoang(04/07/2022)
        public bool CheckFixedAssetCodeExis(string fixedAssetCode)
        {
            using (sqlConnection = new MySqlConnection(connectionString))
            {
                var sql = "SELECT FixedAssetCode FROM FixedAsset WHERE FixedAssetCode = @fixedAssetCode";
                var parameter = new DynamicParameters();
                parameter.Add("@fixedAssetCode", fixedAssetCode);
                var res = sqlConnection.QueryFirstOrDefault(sql: sql, param: parameter);
                if (res == null)
                    return false;
                return true;
            }
        }

        /// <summary>
        /// Kiểm tra tồn tại chứng từ chưa
        /// </summary>
        /// <param name="fixedAssetId">Đối tượng tham chiếu</param>
        /// <returns>false: không tồn tại mã, true: đã có mã</returns>
        /// NDHoang(04/07/2022)
        public bool CheckLicenseExis(string? fixedAssetId)
        {
            using (sqlConnection = new MySqlConnection(connectionString))
            {
                //Tách chuỗi đầu vào thành list các id
                var listIdSelected = fixedAssetId.Split(",");
                //Gán câu truy vấn
                var sql = $"SELECT * FROM LicenseDetail WHERE FixedAssetId IN (";
                //Tạo mảng parameter
                DynamicParameters parameter = new DynamicParameters();
                //Duyệt tất cả id rồi gán lại vào
                for (int i = 0; i < listIdSelected.Length; i++)
                {
                    if (!string.IsNullOrEmpty(listIdSelected[i]))
                    {
                        sql += $"@{tableName}Ids{i},";
                        parameter.Add($"@{tableName}Ids{i}", Guid.Parse(listIdSelected[i]));
                    }
                }
                //Bỏ dấu phảy cuối chuỗi
                sql = sql.Substring(0, sql.Length - 1);
                //Đóng ngoặc
                sql += ")";
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
        public bool CheckUpdateFixedAssetCodeExis(string fixedAssetCode, Guid fixedAssetId)
        {
            using (sqlConnection = new MySqlConnection(connectionString))
            {
                var sql = "SELECT FixedAssetCode FROM FixedAsset WHERE FixedAssetCode = @fixedAssetCode AND FixedAssetId != @fixedAssetId";
                var parameter = new DynamicParameters();
                parameter.Add("@fixedAssetCode", fixedAssetCode);
                parameter.Add("@fixedAssetId", fixedAssetId);
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
        public override IEnumerable<FixedAsset> Get()
        {
            using (sqlConnection = new MySqlConnection(connectionString))
            {
                var sql = $"Proc_GetFixedAsset";
                var fixedAssets = sqlConnection.Query<FixedAsset>(sql, commandType: System.Data.CommandType.StoredProcedure);
                return fixedAssets;
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
        public object GetPaging(int pageIndex, int pageSize, string? filter = "", string? departmentName = "", string? categoryName = "")
        {
            using (sqlConnection = new MySqlConnection(connectionString))
            {
                var sql = $"Proc_PagingFixedAsset";
                //Khởi tạo parameters
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@h_Filter", filter);
                parameters.Add("@h_PageIndex", pageIndex);
                parameters.Add("@h_DepartmentName", departmentName);
                parameters.Add("@h_CategoryName", categoryName);
                parameters.Add("@h_PageSize", pageSize);
                parameters.Add("@h_TotalRecord", direction:System.Data.ParameterDirection.Output);
                parameters.Add("@h_RecordStart", direction: System.Data.ParameterDirection.Output);
                parameters.Add("@h_RecordEnd", direction: System.Data.ParameterDirection.Output);

                var fixedAssets = sqlConnection.Query<FixedAsset>(sql, param: parameters, commandType: System.Data.CommandType.StoredProcedure);
                //Truyền dữ liệu vào out put
                int totalRecord = parameters.Get<int>("@h_TotalRecord");
                int recordStart = parameters.Get<int>("@h_RecordStart");
                int recordEnd = parameters.Get<int>("@h_RecordEnd");
                //Đầu ra
                return new {
                    TotalRecord = totalRecord,
                    RecordStart = recordStart,
                    RecordEnd = recordEnd,
                    Data = fixedAssets
                };
            }
        }

        /// <summary>
        /// Import
        /// </summary>
        /// <param name="fixedAssets">Danh sách đầu vào</param>
        /// <returns>Số lượng bản ghi insert</returns>
        public int Import(IEnumerable<FixedAsset> fixedAssets)
        {
            using (sqlConnection = new MySqlConnection(connectionString))
            {
                var fixedAssetsInserted = 0;
                foreach( var fixedAsset in fixedAssets)
                {
                    var rowInserted = sqlConnection.Execute("Proc_InsertFixedAsset", fixedAsset, commandType: System.Data.CommandType.StoredProcedure);
                    if(rowInserted != null)
                    {
                        fixedAssetsInserted++;
                    }
                }
                return fixedAssetsInserted;
            }
        }


        #endregion
    }
}
