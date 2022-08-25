using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web05.Interfaces.Service
{
    public interface ILicenseAddService : IBaseService<LicenseAdd>
    {
        /// <summary>
        /// Hàm insert license
        /// </summary>
        /// <param name="licenseInsert"></param>
        /// <returns></returns>
        object Insert(LicenseAdd licenseInsert);
        /// <summary>
        /// Sửa bản ghi master - detail
        /// </summary>
        /// <param name="licenseInsert"></param>
        /// <param name="licenseId"></param>
        /// <returns></returns>
        object Update(Guid licenseId, LicenseAdd licenseInsert);
    }
}
