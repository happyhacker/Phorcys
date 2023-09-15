using System;
using System.Collections.Generic;
using SharpArch.Data.NHibernate;
using Phorcys.Core;
using Phorcys.Data;
using log4net;


namespace Phorcys.Services {
  public class DiveShopServices : IDiveShopServices {
    public IPhorcysRepository<DiveShop> DiveShopRepository = new PhorcysRepository<DiveShop>();

    protected static readonly ILog log = LogManager.GetLogger(typeof(DiveShopServices));

    public DiveShop Save(DiveShop diveShop) {
      try {
        //DiveShop.LastModifed = System.DateTime.Now;

        DiveShopRepository.SaveOrUpdate(diveShop);
        DiveShopRepository.DbContext.CommitChanges();
      } catch (Exception e) {
        log.Error("Unable to save dive shop ", e);
        throw e;
      }
      return diveShop;
    }

    public void Delete(DiveShop diveShop) {

      try {
        DiveShopRepository.Delete(diveShop);
        DiveShopRepository.DbContext.CommitChanges();
      } catch (Exception e) {
        log.Error("Cound not delete dive shop. Something else probably references this dive shop", e);
        throw e;
      }
      return;

    }

    public DiveShop GetDiveShop(int id) {
      return DiveShopRepository.Get(id);
    }

    public IList<DiveShop> GetAllForUser(int userId) {
      UserServices userServices = new UserServices(new Repository<User>());
      User systemUser = userServices.FindUser("system");
      IList<DiveShop> diveShops = null;
      try {
        diveShops = DiveShopRepository.GetAllSystemAndUser(userId, systemUser.Id);
      } catch (Exception e) {
        log.Error("Error retrieving dive shops", e);
      }
      return diveShops;
    }
  }
}
