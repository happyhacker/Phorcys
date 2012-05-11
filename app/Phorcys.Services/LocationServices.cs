using System;
using System.Collections;
using System.Collections.Generic;
using Phorcys.Core;
using Phorcys.Data;
using log4net;

namespace Phorcys.Services {
  public class LocationServices : ILocationServices {
    public IPhorcysRepository<DiveLocation> Repository = new PhorcysRepository<DiveLocation>();
    protected static readonly ILog log = LogManager.GetLogger(typeof(LocationServices));

    public DiveLocation Create(DiveLocation location) {
      DiveLocation retVal = new DiveLocation();

      try {
        Repository.SaveOrUpdate(location);
        Repository.DbContext.CommitChanges();
      }
      catch (Exception e) {
        log.Error("Unable to save Dive Location " + location.Title, e);
      }
      return retVal;
    }

    public DiveLocation Delete(DiveLocation location) {
      DiveLocation retVal = new DiveLocation();

      try {
        Repository.Delete(location);
        Repository.DbContext.CommitChanges();
      }
      catch (Exception e) {
        log.Error("Cound not delete Dive Location " + location.Title + " for " + location.User.LoginId);
        throw e;
      }
      return retVal;
    }

    public DiveLocation Get(int id) {
      return Repository.Get(id);
    }

    public IList<DiveLocation> GetAllSystemAndUser(int systemId, int userId) {
      IList<DiveLocation> locations = null;
      try {
        locations = Repository.GetAllSystemAndUser(systemId, userId);
      }
      catch (Exception e) {
        log.Error("Unable to retrieve Dive Locations for user " + userId, e);
      }
      return locations;
    }

    public DiveLocation Save(DiveLocation location) {
      DiveLocation retVal = new DiveLocation();
      try {
        retVal = Repository.SaveOrUpdate(location);
        Repository.DbContext.CommitChanges();
      }
      catch (Exception e) {
        log.Error("Unable to save Dive Location " + location.Title, e);
      }
      return retVal;
    }
  }
}
