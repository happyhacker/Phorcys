using System.Collections.Generic;
using NHibernate.Criterion;
using SharpArch.Core.PersistenceSupport;

namespace Phorcys.Data {
  public interface IPhorcysRepository<T> : IRepository<T> {
    IList<T> GetAllSystemAndUser(int userId, int systemId);
    IList<T> GetByCriteria(DetachedCriteria criteria);
  }
}
