using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Web05.Core.Interfaces.Repository;
using MISA.Web05.Core.Interfaces.Service;
using MISA.Web05.Exceptions;
using MISA.Web05.Core.Resources;
using Microsoft.AspNetCore.Authorization;

namespace MISA.Web05.Api.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class FixedAssetCategoriesController : ControllerBase
    {
        #region Constructor
        //Khai báo interface
        IFixedAssetCategoryService _service;
        IFixedAssetCategoryRepository _repository;

        //Khởi tạo
        public FixedAssetCategoriesController(IFixedAssetCategoryService service, IFixedAssetCategoryRepository repository)
        {
            _service = service;
            _repository = repository;
        }
        #endregion
        #region Method
        /// <summary>
        /// Lấy toàn bộ dữ liệu loại tài sản
        /// </summary>
        /// <returns>Danh sách loại tài sản</returns>
        /// NDHoang(05/07/2022)
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                //Trả kết quả về client:
                var data = _repository.Get();
                return Ok(data);
            }
            catch (MISAValidateException ex)
            {
                //Ghi vào log hệ thống:
                //......
                return HandleException(ex);
            }
        }

        /// <summary>
        /// Lấy toàn bộ dữ liệu loại tài sản theo id
        /// </summary>
        /// <returns>Loại tài sản có khóa chính = id</returns>
        /// NDHoang(30/06/2022)
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                //Trả kết quả về client:
                var departments = _repository.Get(id);
                return Ok(departments);
            }
            catch (MISAValidateException ex)
            {
                //Ghi vào log hệ thống:
                //......
                return HandleException(ex);
            }
        }

        /// <summary>
        /// Thêm mới loại tài sản
        /// </summary>
        /// <param name="department"></param>
        /// <returns>1: thêm mới thành công một loại tài sản</returns>
        [HttpPost]
        public IActionResult Post(FixedAssetCategory fixed_asset_category)
        {
            try
            {
                //Trả kết quả về client:
                var res = _service.InsertService(fixed_asset_category);
                return StatusCode(201, res);
            }
            catch (MISAValidateException ex)
            {
                //Ghi vào log hệ thống:

                return HandleException(ex);
            }
        }

        /// <summary>
        /// Sửa mới tài sản
        /// </summary>
        /// <returns>1: Sửa thành công một loại tài sản</returns>
        /// NDHoang(30/06/2022)
        [HttpPut]
        public IActionResult Put(FixedAssetCategory fixed_asset_category)
        {
            try
            {
                //Trả kết quả về client:
                var res = _service.UpdateService(fixed_asset_category);
                return Ok(res);
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
        /// <returns>1: Xóa thành công một tài sản</returns>
        /// NDHoang(30/06/2022)
        [HttpDelete("{fixed_asset_category_id}")]
        public IActionResult Delete(string? fixed_asset_category_ids)
        {
            try
            {
                //Trả kết quả về client:
                var res = _repository.Delete(fixed_asset_category_ids);
                return Ok(res);
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
        /// <param name="ex">Dối tượng exception</param>
        /// <returns>400: Lỗi dữ liệu, 500: Lỗi hệ thống</returns>
        private IActionResult HandleException(Exception ex)
        {
            var res = new
            {
                devMsg = ex.Message,
                userMsg = Resource.ResourceManager.GetString("MisaError"),

            };
            if (ex is MISAValidateException)
            {
                return StatusCode(400, res);
            }
            else
            {
                return StatusCode(500, res);
            }
        }
        #endregion
    }
}
