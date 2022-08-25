using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web05.Interfaces.Service
{
    public interface IBaseService<MisaEntity>
    {
        #region Method
        /// <summary>
        /// Thêm mới service
        /// </summary>
        /// <param name="entity">Đối tượng thực thi</param>
        /// <returns>Đối tượng</returns>
        /// NDHoang(04/07/2022)
        int InsertService(MisaEntity entity);

        /// <summary>
        /// Thêm mới service
        /// </summary>
        /// <param name="entity">Đối tượng thực thi</param>
        /// <returns>Đối tượng</returns>
        /// NDHoang(04/07/2022)
        int DeleteService(string? entity);

        /// <summary>
        /// Thêm mới service
        /// </summary>
        /// <param name="entity">Đối tượng thực thi</param>
        /// <returns>Đối tượng</returns>
        /// NDHoang(04/07/2022)
        int UpdateService(MisaEntity entity);
        #endregion
    }
}
