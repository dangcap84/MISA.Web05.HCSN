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
    public class LicenseAddsController : ControllerBase
    {
        ILicenseAddRepository _repository;
        ILicenseAddService _service;
        public LicenseAddsController(ILicenseAddService service, ILicenseAddRepository repository)
        {
            _repository = repository;
            _service = service;
        }
        /// <summary>
        /// Thêm bản ghi master - detail
        /// </summary>
        /// <param name="licenseInsert"></param>
        /// <returns></returns>
        [HttpPost("multiData")]
        public IActionResult PostMulti(LicenseAdd licenseInsert)
        {
            try
            {
                var res = _service.Insert(licenseInsert);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
        /// <summary>
        /// Sửa bản ghi master - detail
        /// </summary>
        /// <param name="licenseInsert"></param>
        /// <returns></returns>
        [HttpPut("{licenseId}")]
        public IActionResult UpdateLicenseAdd(Guid licenseId, LicenseAdd licenseInsert)
        {
            try
            {
                var res = _service.Update(licenseId, licenseInsert);
                return StatusCode(201, res);
            }
            catch (Exception ex)
            {
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
    }
}
