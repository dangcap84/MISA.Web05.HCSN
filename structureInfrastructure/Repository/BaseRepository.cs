using Dapper;
using MySqlConnector;
using MISA.Web05.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MISA.Web05.Core.Resources;
using System.Text.RegularExpressions;

namespace MISA.Web05.Infrastructure.Repository
{
    public class BaseRepository<MisaEntity> : IBaseRepository<MisaEntity>
    {
        #region Properties
        //Khai báo các biến kế thừa
        protected string connectionString;
        protected MySqlConnection sqlConnection;
        protected string tableName;
        #endregion
        #region Methods
        /// <summary>
        /// Khởi tạo chuỗi kết nối
        /// </summary>
        /// NDHoang(04/07/2022)
        public BaseRepository()
        {
            connectionString = Resource.ResourceManager.GetString("Host") +
                Resource.ResourceManager.GetString("Port") +
                Resource.ResourceManager.GetString("Database") +
                Resource.ResourceManager.GetString("UserId") +
                Resource.ResourceManager.GetString("Password");
            tableName = typeof(MisaEntity).Name;
        }

        /// <summary>
        /// Get
        /// </summary>
        /// <returns>Danh sách tài sản</returns>
        /// NDHoang(04/07/2022)
        public virtual IEnumerable<MisaEntity> Get()
        {
            using (sqlConnection = new MySqlConnection(connectionString))
            {
                var sql = $"Select * FROM {tableName}";
                var entities = sqlConnection.Query<MisaEntity>(sql);
                return entities;
            }
        }

        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Đối tượng thực thi vd: tài sản, phòng ban với id tương ứng</returns>
        /// NDHoang(04/07/2022)
        public MisaEntity Get(Guid id)
        {
            using (sqlConnection = new MySqlConnection(connectionString))
            {
                var sql = $"SELECT * FROM {tableName} WHERE {tableName}Id = @{tableName}Id";
                var parameter = new DynamicParameters();
                parameter.Add($"@{tableName}Id", id);
                var entitiy = sqlConnection.QueryFirstOrDefault<MisaEntity>(sql: sql, param: parameter);
                return entitiy;
            }
        }

        /// <summary>
        /// Lấy mã tài sản tăng
        /// </summary>
        /// <returns>Mã tài sản tăng>
        public string GetNewCode()
        {
            using (sqlConnection = new MySqlConnection(connectionString))
            {
                //Truy vấn lấy ra mã tài sản lớn nhất danh sách đã sắp xếp
                var sql = $"SELECT {tableName}Code FROM View_{tableName} ORDER BY CreatedDate DESC LIMIT 1";
                //Thực hiên lấy
                var entityCode = sqlConnection.QueryFirstOrDefault<string>(sql: sql);
                //Dùng regex tách phần số ra khỏi chuỗi
                string[] numbers = Regex.Split(entityCode, @"\D+");
                //Biến lưu giá trị số của mã
                string codeNumber = "";
                //Biến lưu giá trị try prase
                var tryPrase = 1;
                //Biến lưu giá trị số cuối chuỗi
                var lastString = entityCode.Substring(entityCode.Length - 1);
                //Lặp tới chuỗi số cuối cùng thì gán cho codeNumber
                for (var i = 0; i < numbers.Length; i++)
                {
                    if (int.TryParse(numbers[i],out tryPrase))
                    {
                        codeNumber = (int.Parse(numbers[i]) + 1).ToString();
                    }
                }
                //Nếu có chuỗi số và kí tự cuối chuỗi không phải là kí tự thì ghép chuỗi codeNumber vào
                //Nếu không thì thêm một đơn vị vào cuối chuỗi
                if(numbers.Length > 0 && int.TryParse(lastString, out tryPrase))
                {
                    entityCode = entityCode.Substring(0,entityCode.Length - codeNumber.Length);
                    entityCode += codeNumber;
                } else if(numbers.Length > 0)
                {
                    entityCode += "1";
                }
                return entityCode;
            }
        }

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="entity">Đối tượng thực thi vd: tài sản, phòng ban</param>
        /// <returns>1: một tài sản được thêm mới thành công</returns>
        /// NDHoang(04/07/2022)
        public int Insert(MisaEntity entity)
        {
            using (sqlConnection = new MySqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (var transaction = sqlConnection.BeginTransaction())
                {
                    var sql = $"Proc_Insert{tableName}";
                    var res = sqlConnection.Execute(sql: sql, param: entity, transaction : transaction, commandType: System.Data.CommandType.StoredProcedure);
                    transaction.Commit();
                    return res;
                }
            }
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="entity">Đối tượng thực thi vd: tài sản, phòng ban</param>
        /// <returns>1: update thành công một tài sản</returns>
        /// NDHoang(04/07/2022)
        public int Update(MisaEntity entity)
        {
            using (sqlConnection = new MySqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (var transaction = sqlConnection.BeginTransaction())
                {
                    var sql = $"Proc_Update{tableName}";
                    var res = sqlConnection.Execute(sql: sql, param: entity, transaction: transaction, commandType: System.Data.CommandType.StoredProcedure);
                    transaction.Commit();
                    return res;
                }
            }
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="entityIds">danh sach Id đầu vào</param>
        /// <returns>2: xóa thành công hai tài sản</returns>
        /// NDHoang(04/07/2022)
        public int Delete(string? entityIds)
        {
            using (sqlConnection = new MySqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (var transaction = sqlConnection.BeginTransaction())
                {
                    //Tách chuỗi đầu vào thành list các id
                    var listIdSelected = entityIds.Split(",");
                    //Gán câu truy vấn
                    var sql = $"DELETE FROM {tableName} WHERE {tableName}Id IN (";
                    //Tạo mảng parameter
                    DynamicParameters parameter = new DynamicParameters();
                    //Duyệt tất cả id rồi gán lại vào
                    for (int i = 0; i < listIdSelected.Length; i++)
                    {
                        var id = listIdSelected[i];
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
                    var res = sqlConnection.Execute(sql: sql, transaction: transaction, param: parameter);
                    transaction.Commit();
                    return res;
                }
            }
        }
        #endregion
    }
}
