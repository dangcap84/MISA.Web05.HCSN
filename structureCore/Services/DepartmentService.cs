using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MISA.Web05.Exceptions;
using MISA.Web05.Interfaces.Repository;
using MISA.Web05.Interfaces.Service;
using MISA.Web05.Services;

namespace MISA.Web05.Service
{
    public class DepartmentService : BaseService<Department>, IDepartmentService
    {
        #region Constructor
        public DepartmentService(IDepartmentRepository repository) : base(repository)
        {

        }
        #endregion

    }
}
