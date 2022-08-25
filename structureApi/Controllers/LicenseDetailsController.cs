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
    public class LicenseDetailsController : ControllerBase
    {
        #region Constructor
        //Khai báo interface
        ILicenseDetailRepository _repository;
        ILicenseDetailService _service;

        //Khởi tạo
        public LicenseDetailsController(ILicenseDetailRepository repository, ILicenseDetailService service)
        {
            _repository = repository;
            _service = service;
        }
        #endregion
        #region Method

        /// <summary>
        /// Lấy toàn bộ dữ liệu tài sản theo trang
        /// </summary>
        /// <returns>Danh sách tài sản theo trang</returns>
        /// NDHoang(05/07/2022)
        [HttpGet("filter")]
        public IActionResult GetPaging(int pageIndex, int pageSize, string? filter = "", string licenseId="")
        {
            try
            {
                //Trả kết quả về client:
                var res = _repository.GetPaging(pageIndex, pageSize, filter, licenseId);
                return Ok(res);
            }
            catch (Exception ex)
            {

                return HandleException(ex);
            }
        }

        /// <summary>
        /// Lấy ra bộ phận sử dụng và detail json của license detail
        /// </summary>
        /// <param name="licenseDetailId"></param>
        /// <returns></returns>
        /// NDHoang(05/07/2022)
        [HttpGet("GetMoneySource/{licenseDetailId}")]
        public IActionResult GetMoneySource(Guid licenseDetailId)
        {
            try
            {
                var res = _repository.GetMoneySource(licenseDetailId);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        /// Lấy toàn bộ license detail theo license id của master
        /// </summary>
        /// <param name="licenseId"></param>
        /// <returns></returns>

        [HttpGet("getDetailAssets")]
        public IActionResult GetDetailAssets(Guid licenseId)
        {
            try
            {
                var res = _repository.GetDetailAssets(licenseId);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        /// Lấy toàn bộ license detail theo license id của master
        /// </summary>
        /// <param name="licenseId"></param>
        /// <returns></returns>

        [HttpGet("getDetail")]
        public IActionResult GetLicenseInsertById(Guid licenseId)
        {
            try
            {
                var res = _repository.GetLicenseInsertById(licenseId);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        /// Xóa nhiều bản ghi
        /// </summary>
        /// <returns>Số bản ghi xóa thành công một bản ghi</returns>
        /// NDHoang(30/06/2022)
        [HttpDelete("filterId")]
        public IActionResult Delete(string? licenseDetailIds)
        {
            try
            {
                //Trả kết quả về client:
                var res = _repository.Delete(licenseDetailIds);
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
