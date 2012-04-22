using System;
using System.Collections.Generic;
using SharpArch.Core.PersistenceSupport;

namespace Phorcys.Data {
  public interface IPhorcysRepository<T> : IRepository<T> {
    IList<T> GetSystemAndUserRecords(NHibernate.Criterion.DetachedCriteria detachedCriteria);
      IList<T> GetAllForUser(int userId, int systemId);
  }
}
