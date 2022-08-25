using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web05.Interfaces.Repository
{
    public interface IBaseRepository<MisaEntity>
    {
        #region Method
        /// <summary>
        /// Lấy toàn bộ dữ liệu của bảng
        /// </summary>
        /// <returns>Danh sách đối tượng</returns>
        /// NDHoang(26/06/2022)
        IEnumerable<MisaEntity> Get();
        /// <summary>
        /// Lấy dữ liệu bảng theo id
        /// </summary>
        /// <param name="id">khóa chính của đối tượng</param>
        /// <returns>Trả về đối tượng với id = id</returns>
        /// NDHoang(07/04/2022)
        MisaEntity Get(Guid id);

        /// <summary>
        /// Lấy mã tăng
        /// </summary>
        /// <returns>Trả về mã tăng một đơn vị</returns>
        string GetNewCode();

        /// <summary>
        /// Thêm mới bảng
        /// </summary>
        /// <param name="entity">Đối tượng thực thi</param>
        /// <returns>res = 1: một đối tượng được thêm mới</returns>
        /// NDHoang(26/06/2022)
        int Insert(MisaEntity entity);

        /// <summary>
        /// Cập nhật bảng
        /// </summary>
        /// <param name="entity">Đối tượng thực thi</param>
        /// <returns>res = 1: Cập nhật thành công một đối tượng</returns>
        /// NDHoang(26/06/2022)
        int Update(MisaEntity entity);

        /// <summary>
        /// Xóa bảng
        /// </summary>
        /// <param name="entity">Đối tượng thực thi</param>
        /// <returns>res = 1: Xóa thành công một đối tượng</returns>
        /// NDHoang(26/06/2022)
        int Delete(string? entityIds);
        #endregion
    }
}
