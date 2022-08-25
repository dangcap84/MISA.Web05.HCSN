using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Web05.Exceptions;
using MISA.Web05.Interfaces.Repository;
using MISA.Web05.Interfaces.Service;
using MISA.Web05.Core.Resources;
using Microsoft.AspNetCore.Authorization;

namespace MISA.Web05.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class FixedAssetsController : ControllerBase
    {
        #region Constructor
        //Khai báo interface
        IFixedAssetRepository _repository;
        IFixedAssetService _service;

        //Khởi tạo
        public FixedAssetsController(IFixedAssetRepository repository, IFixedAssetService service)
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
        public IActionResult GetPaging(int pageIndex, int pageSize, string? filter = "", string? DepartmentName = "", string? CategoryName = "")
        {
            try
            {
                //Trả kết quả về client:
                var res = _repository.GetPaging(pageIndex, pageSize, filter, DepartmentName, CategoryName);
                return Ok(res);
            }
            catch (Exception ex)
            {

                return HandleException(ex);
            }
        }

        /// <summary>
        /// Lấy toàn bộ dữ liệu tài sản
        /// </summary>
        /// <returns>Danh sách tài sản</returns>
        /// NDHoang(05/07/2022)
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                //Trả kết quả về client:
                var fixedassets = _repository.Get();
                return Ok(fixedassets);
            }
            catch (MISAValidateException ex)
            {
                //Ghi vào log hệ thống:
                //......
                return HandleException(ex);
            }
        }

        /// <summary>
        /// Lấy toàn bộ dữ liệu tài sản theo id
        /// </summary>
        /// <returns>Tài sản với khóa chính = id</returns>
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

        [HttpGet("NewFixedAssetCode")]
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
        /// Thêm mới tài sản
        /// </summary>
        /// <returns>1: Thêm mới thành công một tài sản</returns>
        /// NDHoang(30/06/2022)
        [HttpPost]
        public IActionResult Post(FixedAsset fixed_Asset)
        {
            try
            {
                //Trả kết quả về client:
                var res = _service.InsertService(fixed_Asset);
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
        /// Sửa mới tài sản
        /// </summary>
        /// <returns>1: Sửa thành công một tài sản</returns>
        /// NDHoang(30/06/2022)
        [HttpPut]
        public IActionResult Put(FixedAsset fixed_Asset)
        {
            try
            {
                //Trả kết quả về client:
                var res = _service.UpdateService(fixed_Asset);
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
        /// Xóa tài sản
        /// </summary>
        /// <returns>Số bản ghi xóa thành công một tài sản</returns>
        /// NDHoang(30/06/2022)
        [HttpDelete("filterId")]
        public IActionResult Delete(string? fixed_Asset_ids)
        {
            try
            {
                //Trả kết quả về client:
                var res = _service.DeleteService(fixed_Asset_ids);
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
        /// Import file dữ liệu
        /// </summary>
        /// <param name="fileImport"> file dữ liệu</param>
        /// <returns>Danh sách import</returns>
        [HttpPost("import")]
        public IActionResult Import(IFormFile fileImport)
            {
                try
                {
                //Trả kết quả về client:
                    var fixedAssets = _service.Import(fileImport);
                    return Ok(fixedAssets);
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
        #endregion
    }
}
