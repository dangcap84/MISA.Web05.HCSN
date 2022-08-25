using MISA.Web05.Core.Interfaces.Repository;
using MISA.Web05.Core.Interfaces.Service;
using MISA.Web05.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web05.Core.Services
{
    public class FixedAssetCategoryService : BaseService<FixedAssetCategory>, IFixedAssetCategoryService
    {
        #region Constructor
        public FixedAssetCategoryService(IFixedAssetCategoryRepository repository) : base(repository)
        {

        }
        #endregion

    }
}
