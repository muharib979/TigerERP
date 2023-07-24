using DatabaseContext;
using Infrustracture.ERP.RepositorySetup.ForBranch;
using Microsoft.EntityFrameworkCore;
using ModelClass.ERP.UnitOfWorkRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrustracture.ERP.UnitOfWorkSetup.ForBranch
{
    public class BranchUnitOfWork : UnitOfWork, IBranchUnitOfWork
    {
        public IBranchRepository Branch { get; private set; }
        public BranchUnitOfWork(DatabaseContextDb dbContext
            , IBranchRepository _branchRepository) : base((DbContext)dbContext)
        {
            Branch = _branchRepository;
        }

    }
}
