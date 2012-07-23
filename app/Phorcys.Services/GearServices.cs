using System;
using System.Collections.Generic;
using Phorcys.Core;
using Phorcys.Data;
using log4net;

namespace Phorcys.Services {
  public class GearServices : IGearServices {
    public IPhorcysRepository<Gear> Repository = new PhorcysRepository<Gear>();
    protected static readonly ILog log = LogManager.GetLogger(typeof(LocationServices));


    public Gear Create(Gear gear)
    {
      Gear retVal = new Gear();
           try {
        Repository.SaveOrUpdate(gear);
        Repository.DbContext.CommitChanges();
      }
      catch (Exception e) {
        log.Error("Unable to save gear " + gear.Title, e);
      }
      return retVal;
    }

    public Gear Delete(Gear gear) {
      Gear retVal = new Gear();

      try {
        Repository.Delete(gear);
        Repository.DbContext.CommitChanges();
      }
      catch (Exception e) {
        log.Error("Cound not delete piece of gear " + gear.Title + ". A dive probably references this piece of gear");
        throw e;
      }
      return retVal;

    }

    public Gear Get(int id)
    {
      return Repository.Get(id);
    }

    public IList<Gear> GetAllSystemAndUser(int systemId, int userId) {
      throw new NotImplementedException();
    }

    public Gear Save(Gear gear) {
      throw new NotImplementedException();
    }
  }
}
