﻿using SharpArch.Core.PersistenceSupport;
using SharpArch.Core.DomainModel;

namespace Phorcys.Tests.Integration.Phorcys.Data.TestDoubles
{
    /// <summary>
    /// A test double for <see cref="IEntityDuplicateChecker" />.  The default implementation
    /// always assumes that a duplicate does not exist.  This may be modified to acccount for 
    /// different testing scenarios.  This test double gets registered within 
    /// <see cref="ServiceLocatorInitializer" />.
    /// </summary>
    public class EntityDuplicateCheckerStub : IEntityDuplicateChecker
    {
        public bool DoesDuplicateExistWithTypedIdOf<IdT>(IEntityWithTypedId<IdT> entity)
        {
            return false;
        }
    }
}
