using MISA.Web05.Core.Modals;
using MISA.Web05.Interfaces.Repository;
using MISA.Web05.Interfaces.Service;
using MISA.Web05.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web05.Service
{
    public class LicenseDetailService : BaseService<LicenseDetail>, ILicenseDetailService
    {
        #region Constructor
        public LicenseDetailService(ILicenseDetailRepository repository) : base(repository)
        {

        }
        #endregion
    }
}
