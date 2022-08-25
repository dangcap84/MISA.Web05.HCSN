    using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using hcsn.Modals;
using Dapper;
using MySqlConnector;

namespace hcsn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        /// <summary>
        /// Lấy toàn bộ dữ liệu phòng ban
        /// </summary>
        /// NDHoang(27/06/2022)
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                //Khai báo thông tin csdl:
                var connectionString = "Host=3.0.89.182;" +
                    "Port=3306;" +
                    "Database=MISA.WEB05.NDHOANG;" +
                    "User Id = dev;" +
                    "Password= 12345678";
                //Khởi tạo kết nối:
                var sqlConnection = new MySqlConnection(connectionString);
                //Lấy dữ liệu:
                var sqlQuery = "SELECT * FROM department";
                var departments = sqlConnection.Query<department>(sqlQuery).ToList();
                //Trả dữ liệu về cho client:
                return Ok(departments);
            }
            catch (Exception ex)
            {

                throw ;
            }
        }

        /// <summary>
        /// Lấy toàn bộ dữ liệu phòng ban theo id
        /// </summary>
        /// NDHoang(27/06/2022)
        [HttpGet("{department_id}")]
        public IActionResult GetById(Guid department_id)
        {
            try
            {
                //Khai báo thông tin csdl:
                var connectionString = "Host=3.0.89.182;" +
                    "Port=3306;" +
                    "Database=MISA.WEB05.NDHOANG;" +
                    "User Id = dev;" +
                    "Password= 12345678";
                //Khởi tạo kết nối:
                var sqlConnection = new MySqlConnection(connectionString);
                //Khai báo try vẫn:
                var sqlQuery = $"SELECT * FROM department WHERE department_id = '{department_id.ToString()}'";
                var departments = sqlConnection.QueryFirstOrDefault<department>(sqlQuery);
                //Trả dữ liệu về cho client:
                return Ok(departments);
            }
            catch (Exception ex)
            {

                throw ;
            }
        }

        /// <summary>
        /// Thêm mới phòng ban
        /// </summary>
        /// NDHoang(27/06/2022)
        [HttpPost]
        public IActionResult Post(department department)
        {
            try
            {
                //Khai báo thông tin csdl:
                var connectionString = "Host=3.0.89.182;" +
                    "Port=3306;" +
                    "Database=MISA.WEB05.NDHOANG;" +
                    "User Id = dev;" +
                    "Password= 12345678";
                //Khởi tạo kết nối:
                var sqlConnection = new MySqlConnection(connectionString);
                //Khai báo try vẫn thêm mới:
                var sqlQuery = "Proc_InsertDepartment";
                var res = sqlConnection.Execute(sqlQuery, param: department,commandType:System.Data.CommandType.StoredProcedure);
                return Ok(res);
            }
            catch (Exception ex)
            {

                throw ;
            }
        }

        /// <summary>
        /// Sửa dữ liệu phòng ban
        /// </summary>
        /// NDHoang(27/06/2022)
        [HttpPut]
        public IActionResult Put(department department)
        {
            try
            {
                //Khai báo thông tin csdl:
                var connectionString = "Host=3.0.89.182;" +
                    "Port=3306;" +
                    "Database=MISA.WEB05.NDHOANG;" +
                    "User Id = dev;" +
                    "Password= 12345678";
                //Khởi tạo kết nối:
                var sqlConnection = new MySqlConnection(connectionString);
                //Khai báo try vẫn thêm mới:
                var sqlQuery = "Proc_UpdateDepartment";
                var res = sqlConnection.Execute(sqlQuery, param: department, commandType: System.Data.CommandType.StoredProcedure);
                return Ok(res);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        /// <summary>
        /// Xóa liệu phòng ban
        /// </summary>
        /// NDHoang(27/06/2022)
        [HttpDelete("{department_id}")]
        public IActionResult Delete(Guid department_id)
        {
            try
            {
                //Khai báo thông tin csdl:
                var connectionString = "Host=3.0.89.182;" +
                    "Port=3306;" +
                    "Database=MISA.WEB05.NDHOANG;" +
                    "User Id = dev;" +
                    "Password= 12345678";
                //Khởi tạo kết nối:
                var sqlConnection = new MySqlConnection(connectionString);
                //Khai báo try vẫn thêm mới:
                var sqlQuery = $"DELETE FROM department WHERE department_id = '{department_id.ToString()}'";
                var assets = sqlConnection.Query<fixed_asset>(sqlQuery);
                return Ok(assets);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
