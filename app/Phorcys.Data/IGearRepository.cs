using System;
using System.Collections.Generic;
using SharpArch.Core.PersistenceSupport;

namespace Phorcys.Data
{
    public interface IGearRepository<T> : IRepository<T> {
        IList<T> GetAllForUser(int userId);
    }
}