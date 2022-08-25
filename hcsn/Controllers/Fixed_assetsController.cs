using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using hcsn.Modals;
using Dapper;
using MySqlConnector;

namespace hcsn.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class Fixed_assetsController : ControllerBase
    {
        string _conectionString;
        MySqlConnection _mySqlConnectionString;
        public Fixed_assetsController()
        {
            _conectionString = "Host=3.0.89.182;" +
                "Port=3306;" +
                "Database=MISA.WEB05.NDHOANG;" +
                "User Id = dev;" +
                "Password= 12345678";
            _mySqlConnectionString = new MySqlConnection(_conectionString);
        }
        /// <summary>
        /// Lấy toàn bộ dữ liệu tài sản
        /// </summary>
        /// NDHoang(27/06/2022)
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                //Khai báo thông tin csdl:
                //Lấy dữ liệu:
                var sqlQuery = "SELECT * FROM fixed_asset";
                var assets = _mySqlConnectionString.Query<fixed_asset>(sqlQuery).ToList();
                //Trả dữ liệu về cho client:
                return Ok(assets);
            }
            catch (Exception ex)
            {
                //Ghi vào log hệ thống:
                //......
                var res = new
                {
                    devMsg = ex.Message,
                    userMsg = "Có lỗi xảy ra vui lòng liên hệ MISA để được trợ giúp",

                };
                return StatusCode(500, res);
            }
        }

        /// <summary>
        /// Lấy toàn bộ dữ liệu tài sản theo id
        /// </summary>
        /// NDHoang(27/06/2022)
        [HttpGet("{fixed_asset_id}")]
        public IActionResult GetById(Guid fixed_asset_id)
        {
            try
            {
                //Khai báo try vẫn:
                var sqlQuery = $"SELECT * FROM fixed_asset WHERE fixed_asset_id = '{fixed_asset_id.ToString()}'";
                var assets = _mySqlConnectionString.QueryFirstOrDefault<fixed_asset>(sqlQuery);
                //Trả dữ liệu về cho client:
                return Ok(assets);
            }
            catch (Exception ex)
            {
                //Ghi vào log hệ thống:
                //......
                var res = new
                {
                    devMsg = ex.Message,
                    userMsg = "Có lỗi xảy ra vui lòng liên hệ MISA để được trợ giúp",

                };
                return StatusCode(500, res);
            }
        }

        /// <summary>
        /// Thêm mới phòng ban
        /// </summary>
        /// NDHoang(27/06/2022)
        [HttpPost]
        public IActionResult Post(fixed_asset fixed_asset)
        {
            try
            {
                //Validate dữ liệu
                //1.Mã tài sản không được phép để trống
                if (string.IsNullOrEmpty(fixed_asset.fixed_asset_code))
                {
                    var resService = new
                    {
                        userMsg = "Mã tài sản không được phép bỏ trống",
                    };
                    return BadRequest(resService);
                }
                //2.Tên tài sản không được chưa từ Hoàng
                if (fixed_asset.fixed_asset_name.Contains("Hoàng"))
                {
                    var resService = new
                    {
                        userMsg = "Tên tài sản không được phép bỏ chứa từ Hoàng",
                    };
                    return BadRequest(resService);
                }
                //
                //Khai báo try vẫn thêm mới:
                var sqlQuery = "Proc_InsertAsset";
                var res = _mySqlConnectionString.Execute(sqlQuery, param: fixed_asset, commandType: System.Data.CommandType.StoredProcedure);
                return StatusCode(201,res);
            }
            catch (Exception ex)
            {
                //Ghi vào log hệ thống:
                //......
                var res = new
                {
                    devMsg = ex.Message,
                    userMsg = "Có lỗi xảy ra vui lòng liên hệ MISA để được trợ giúp",

                };
                return StatusCode(500, res);
            }
        }

        /// <summary>
        /// Sửa dữ liệu tài sản
        /// </summary>
        /// NDHoang(27/06/2022)
        [HttpPut]
        public IActionResult Put(fixed_asset fixed_asset)
        {
            try
            {
                //Khai báo try vẫn thêm mới:
                var sqlQuery = "Proc_UpdateAsset";
                var res = _mySqlConnectionString.Execute(sqlQuery, param: fixed_asset, commandType: System.Data.CommandType.StoredProcedure);
                return StatusCode (201,res);
            }
            catch (Exception ex)
            {
                //Ghi vào log hệ thống:
                //......
                var res = new
                {
                    devMsg = ex.Message,
                    userMsg = "Có lỗi xảy ra vui lòng liên hệ MISA để được trợ giúp",

                };
                return StatusCode(500, res);
            }
        }

        /// <summary>
        /// Xóa dữ liệu tài sản
        /// </summary>
        /// NDHoang(27/06/2022)
        [HttpDelete("{fixed_asset_id}")]
        public IActionResult Delete( Guid fixed_asset_id)
        {
            try
            {
                //Khai báo truy vẫn xóa:
                var sqlQuery = $"DELETE FROM fixed_asset WHERE fixed_asset_id = '{fixed_asset_id.ToString()}'";
                var assets = _mySqlConnectionString.Query<fixed_asset>(sqlQuery);
                return StatusCode(201,1);
            }
            catch (Exception ex)
            {
                //Ghi vào log hệ thống:
                //......
                var res = new
                {
                    devMsg = ex.Message,
                    userMsg = "Có lỗi xảy ra vui lòng liên hệ MISA để được trợ giúp",

                };
                return StatusCode(500, res);
            }
        }
    }
}
