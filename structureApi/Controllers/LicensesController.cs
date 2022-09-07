using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Web05.Core.Resources;
using MISA.Web05.Exceptions;
using MISA.Web05.Interfaces.Repository;
using MISA.Web05.Interfaces.Service;

namespace MISA.Web05.Api.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class LicensesController : ControllerBase
    {
        #region Constructor
        //Khai báo interface
        ILicenseRepository _repository;
        ILicenseService _service;

        //Khởi tạo
        public LicensesController(ILicenseRepository repository, ILicenseService service)
        {
            _repository = repository;
            _service = service;
        }
        #endregion
        #region Method
        /// <summary>
        /// Lấy toàn bộ dữ liệu chứng từ theo trang
        /// </summary>
        /// <returns>Danh sách Chứng từ theo trang</returns>
        /// NDHoang(05/07/2022)
        [HttpGet("filter")]
        public IActionResult GetPaging(int pageIndex, int pageSize, string? filter = "")
        {
            try
            {
                //Trả kết quả về client:
                var res = _repository.GetPaging(pageIndex, pageSize, filter);
                return Ok(res);
            }
            catch (Exception ex)
            {

                return HandleException(ex);
            }
        }

        /// <summary>
        /// Lấy toàn bộ dữ liệu chứng từ
        /// </summary>
        /// <returns>Danh sách chứng từ</returns>
        /// NDHoang(05/07/2022)
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                //Trả kết quả về client:
                var licenses = _repository.Get();
                return Ok(licenses);
            }
            catch (MISAValidateException ex)
            {
                //Ghi vào log hệ thống:
                //......
                return HandleException(ex);
            }
        }

        /// <summary>
        /// Lấy toàn bộ dữ liệu chứng từ theo id
        /// </summary>
        /// <returns>chứng từ với khóa chính = id</returns>
        /// NDHoang(30/06/2022)
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                //Trả kết quả về client:
                var fixedassets = _repository.Get(id);
                return Ok(fixedassets);
            }
            catch (MISAValidateException ex)
            {
                //Ghi vào log hệ thống:
                //......
                return HandleException(ex);
            }
        }

        [HttpGet("NewLicenseCode")]
        public IActionResult GetNewCode()
        {
            try
            {
                //Trả kết quả về client:
                var fixedAssets = _repository.GetNewCode();
                return Ok(fixedAssets);
            }
            catch (MISAValidateException ex)
            {
                //Ghi vào log hệ thống:
                //......
                return HandleException(ex);
            }
        }

        /// <summary>
        /// Thêm mới chứng từ
        /// </summary>
        /// <returns>1: Thêm mới thành công một chứng từ</returns>
        /// NDHoang(30/06/2022)
        [HttpPost]
        public IActionResult Post(License license)
        {
            try
            {
                //Trả kết quả về client:
                var res = _service.InsertService(license);
                return StatusCode(201, res);
            }
            catch (MISAValidateException ex)
            {
                //Ghi vào log hệ thống:
                //......
                return HandleException(ex);
            }
        }

        /// <summary>
        /// Sửa mới chứng từ
        /// </summary>
        /// <returns>1: Sửa thành công một chứng từ</returns>
        /// NDHoang(30/06/2022)
        [HttpPut]
        public IActionResult Put(License license)
        {
            try
            {
                //Trả kết quả về client:
                var res = _service.UpdateService(license);
                return StatusCode(201, res);
            }
            catch (MISAValidateException ex)
            {
                //Ghi vào log hệ thống:
                //......
                return HandleException(ex);
            }
        }

        /// <summary>
        /// Xóa chứng từ
        /// </summary>
        /// <returns>Số bản ghi xóa thành công một chứng từ</returns>
        /// NDHoang(30/06/2022)
        [HttpDelete("filterId")]
        public IActionResult Delete(string? licenseIds)
        {
            try
            {
                //Trả kết quả về client:
                var res = _repository.Delete(licenseIds);
                return StatusCode(201, res);
            }
            catch (MISAValidateException ex)
            {
                //Ghi vào log hệ thống:
                //......
                return HandleException(ex);
            }
        }

        /// <summary>
        /// Xử lý exception
        /// </summary>
        /// <param name="ex">Đối tượng Exception</param>
        /// <returns>400: Lỗi dữ liệu, 500: Lỗi hệ thống</returns>
        /// NDHoang(05/07/2022)
        private IActionResult HandleException(Exception ex)
        {
            if (ex is MISAValidateException)
            {
                var res = new
                {
                    devMsg = ex.Message,
                    data = ex.Data,
                    userMsg = ex.Message,

                };
                return StatusCode(400, res);
            }
            else
            {
                var res = new
                {
                    devMsg = ex.Message,
                    userMsg = Resource.ResourceManager.GetString("MisaError"),

                };
                return StatusCode(500, res);
            }
        }
        #endregion
    }
}
