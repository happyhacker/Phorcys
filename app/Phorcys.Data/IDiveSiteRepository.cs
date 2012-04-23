using System.Collections.Generic;
using SharpArch.Core.PersistenceSupport;

namespace Phorcys.Data {
  public interface IDiveSiteRepository<T> : IRepository<T> {
    IList<T> GetAllForUser(int userId, int systemId);
  }
}
