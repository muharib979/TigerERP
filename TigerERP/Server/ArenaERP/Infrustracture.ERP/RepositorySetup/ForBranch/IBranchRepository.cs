﻿using ModelClass.ERP.DTO;
using ModelClass.ERP.UnitOfWorkRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrustracture.ERP.RepositorySetup.ForBranch
{
    public interface IBranchRepository : IRepository<Branch, int>
    {
    }
}
