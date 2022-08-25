using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web05.Interfaces.Service
{
    public interface IFixedAssetService : IBaseService<FixedAsset>
    {
        #region method
        /// <summary>
        /// Thực hiện nhập khẩu dữ liệu
        /// </summary>
        /// <param name="fileImport">Tệp nhập khẩu dữ liệu</param>
        /// <returns>Danh sách nhân viên trong tệp thực hiện nhập khẩu kèm theo trạng thái đã nhập được chưa</returns>
        /// NDHoang(22/07/2022)
        IEnumerable<FixedAsset> Import(IFormFile fileImport);
        #endregion
    }
}
