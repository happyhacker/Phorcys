using System;
using System.Collections.Generic;
using Phorcys.Core;
using Phorcys.Data;
using Phorcys.Services.Services;
using SharpArch.Data.NHibernate;
using log4net;

namespace Phorcys.Services {
  public class GearServices : IGearServices {
    public IPhorcysRepository<Gear> GearRepository = new PhorcysRepository<Gear>();
    public IPhorcysRepository<Tank> TankRepository = new PhorcysRepository<Tank>();

    protected static readonly ILog log = LogManager.GetLogger(typeof(LocationServices));


    public Gear Save(Gear gear) {
      try {
        GearRepository.SaveOrUpdate(gear);
        GearRepository.DbContext.CommitChanges();
      }
      catch (Exception e) {
        log.Error("Unable to save gear " + gear.Title, e);
      }
      return gear;
    }

    public Tank Save(Tank tank) {
       try {
        TankRepository.SaveOrUpdate(tank);
        TankRepository.DbContext.CommitChanges();
      }
      catch (Exception e) {
        log.Error("Unable to save tank " + tank.Title, e);
      }
      return tank;
    }

    public Gear Delete(Gear gear) {
      Gear retVal = new Gear();

      try {
        GearRepository.Delete(gear);
        GearRepository.DbContext.CommitChanges();
      }
      catch (Exception e) {
        log.Error("Cound not delete piece of gear " + gear.Title + ". A dive probably references this piece of gear");
        throw e;
      }
      return retVal;

    }

    public Gear GetGear(int id) {
      return GearRepository.Get(id);
    }

    public Tank GetTank(int id)
    {
      return TankRepository.Get(id);
    }

    public IList<Gear> GetAllForUser(int userId) {
      UserServices userServices = new UserServices(new Repository<User>());
      User systemUser = userServices.FindUser("system");

      IList<Gear> diveSites = GearRepository.GetAllSystemAndUser(userId, systemUser.Id);

      return diveSites;
    }
  }
}
