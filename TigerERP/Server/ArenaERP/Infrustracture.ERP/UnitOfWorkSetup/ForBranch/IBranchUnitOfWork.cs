using Infrustracture.ERP.RepositorySetup.ForBranch;
using ModelClass.ERP.UnitOfWorkRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrustracture.ERP.UnitOfWorkSetup.ForBranch
{
    public interface IBranchUnitOfWork : IUnitOfWork
    {
        IBranchRepository Branch { get; }

    }
}
