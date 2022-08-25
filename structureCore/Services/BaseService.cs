using MISA.Web05.Exceptions;
using MISA.Web05.Interfaces.Repository;
using MISA.Web05.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web05.Services
{
    public class BaseService<MisaEntity> : IBaseService<MisaEntity>
    {
        #region Constructor
        ///Tạo biến sử dụng IBaseRepository
        protected IBaseRepository<MisaEntity> _repository;
        /// <summary>
        /// Danh sách lỗi validate là một mảng gồm các lỗi validate tương ứng
        /// </summary>
        /// NDHoang(04/07/2022)
        protected List<string> ErrorValidateMsgs;
        /// <summary>
        /// Biến IsValid thay đổi giá trị thành true mỗi khi có trường dữ liệu không hợp lệ
        /// </summary>
        /// NDHoang(04/07/2022)
        protected bool IsValid = true;

        ///Khởi tạo
        public BaseService(IBaseRepository<MisaEntity> repository)
        {
            _repository = repository;
            ErrorValidateMsgs = new List<string>();
        }
        #endregion
        #region Method
        /// <summary>
        /// Thêm mới
        /// </summary>
        /// <param name="entity">Đối tượng thực thi vd: tài sản, phòng ban</param>
        /// <returns>res = 1, Thêm mới thành công một</returns>
        /// <exception cref="MISAValidateException">Hàm kiểm tra validate những lỗi riêng biệt của dự án
        /// vd: lỗi trùng mã, lỗi không có dữ liệu
        /// </exception>
        /// NDHoang(04/07/2022)
        public int InsertService(MisaEntity entity)
        {
            //Validate dữ liệu: 
            var isValid = Validate(entity);
            //Thực hiện thêm mới:
            if (isValid)
            {
                var res = _repository.Insert(entity);
                return res;
            }
            else
            {
                throw new MISAValidateException(ErrorValidateMsgs);
            }
        }

        /// <summary>
        /// Xóa
        /// </summary>
        /// <param name="entity">Đối tượng thực thi vd: tài sản, phòng ban</param>
        /// <returns>res = 1, Thêm mới thành công một</returns>
        /// <exception cref="MISAValidateException">Hàm kiểm tra validate những lỗi riêng biệt của dự án
        /// vd: lỗi trùng mã, lỗi không có dữ liệu
        /// </exception>
        /// NDHoang(04/07/2022)
        public int DeleteService(string? entity)
        {
            //Validate dữ liệu: 
            var isValid = DeleteValidate(entity);
            //Thực hiện thêm mới:
            if (isValid)
            {
                var res = _repository.Delete(entity);
                return res;
            }
            else
            {
                throw new MISAValidateException(ErrorValidateMsgs);
            }
        }

        /// <summary>
        /// Thực hiện cập nhật
        /// </summary>
        /// <param name="entity">Đối tượng thực thi vd: tài sản, phòng ban</param>
        /// <returns>res = 1 : Thêm mới một đối tượng</returns>
        /// NDHoang(04/07/2022)
        public int UpdateService(MisaEntity entity)
        {
            //Validate dữ liệu: 
            var isValid = UpdateValidate(entity);
            //Thực hiện thêm mới:
            if (isValid)
            {
                var res = _repository.Update(entity);
                return res;
            }
            else
            {
                throw new MISAValidateException(ErrorValidateMsgs);
            }
        }

        /// <summary>
        /// Thực hiên validate dữ liệu bảng
        /// </summary>
        /// <param name="entity">Đối tượng thực thi vd: tài sản, phòng ban</param>
        /// <returns>true: hợp lệ; false: không hợp lệ</returns>
        /// NDHoang(04/07/2022)
        protected virtual bool Validate(MisaEntity entity)
        {
            return true;
        }

        /// <summary>
        /// Thực hiên validate dữ liệu bảng
        /// </summary>
        /// <param name="entity">Đối tượng thực thi vd: tài sản, phòng ban</param>
        /// <returns>true: hợp lệ; false: không hợp lệ</returns>
        /// NDHoang(04/07/2022)
        protected virtual bool DeleteValidate(string? entity)
        {
            return true;
        }

        /// <summary>
        /// Thực hiên validate dữ liệu bảng khi update
        /// </summary>
        /// <param name="entity">Đối tượng thực thi vd: tài sản, phòng ban</param>
        /// <returns>true: hợp lệ; false: không hợp lệ</returns>
        /// NDHoang(04/07/2022)
        protected virtual bool UpdateValidate(MisaEntity entity)
        {
            return true;
        }
        #endregion

    }
}
