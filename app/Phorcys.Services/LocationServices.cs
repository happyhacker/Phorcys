using System.Collections;
using System.Collections.Generic;
using Phorcys.Core;
using Phorcys.Data;
using SharpArch.Core.PersistenceSupport;
using SharpArch.Data.NHibernate;

namespace Phorcys.Services {
  public class LocationServices : ILocationServices {
    public IPhorcysRepository<DiveLocation> Repository = new PhorcysRepository<DiveLocation>();

    public DiveLocation Get(int id) {
      return Repository.Get(id);
    }

    public IList<DiveLocation> GetAllSystemAndUser(int systemId, int userId) {
      IList<DiveLocation> locations = Repository.GetAllSystemAndUser(systemId, userId);
      return locations;
    }
  }
}
