using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MISA.Web05;
using Dapper;
using MySqlConnector;
using MISA.Web05.Interfaces.Repository;

namespace MISA.Web05.Infrastructure.Repository
{
    public class DepartmentRepository : BaseRepository<Department>, IDepartmentRepository
    {

    }
}
