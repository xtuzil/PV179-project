using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CactusDAL.UnitOfWork
{
    public interface IUnitOfWorkProvider
    {
        void Create();
        IUnitOfWork GetUnitOfWorkInstance();
    }
}
