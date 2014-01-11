using System;
using System.Collections.Generic;
using SharpArch.Data.NHibernate;
using Phorcys.Core;
using Phorcys.Data;
using log4net;


namespace Phorcys.Services {
  public class DiveAgencyServices : IDiveAgencyServices {
    public IPhorcysRepository<DiveAgency> DiveAgencyRepository = new PhorcysRepository<DiveAgency>();

    protected static readonly ILog log = LogManager.GetLogger(typeof(InstructorServices));

    public DiveAgency Save(DiveAgency agency) {
      try {
        //instructor.LastModifed = System.DateTime.Now;

        DiveAgencyRepository.SaveOrUpdate(agency);
        DiveAgencyRepository.DbContext.CommitChanges();
      } catch (Exception e) {
        log.Error("Unable to save dive agency ", e);
        throw e;
      }
      return agency;
    }

    public void Delete(DiveAgency agency) {

      try {
        DiveAgencyRepository.Delete(agency);
        DiveAgencyRepository.DbContext.CommitChanges();
      } catch (Exception e) {
        log.Error("Cound not delete dive agency. Something else probably references this agency", e);
        throw e;
      }
      return;

    }

    public DiveAgency GetDiveAgency(int id) {
      return DiveAgencyRepository.Get(id);
    }

    public IList<DiveAgency> GetAllForUser(int userId) {
      UserServices userServices = new UserServices(new Repository<User>());
      User systemUser = userServices.FindUser("system");
      IList<DiveAgency> agencies = null;
      try {
        agencies = DiveAgencyRepository.GetAllSystemAndUser(userId, systemUser.Id);
      } catch (Exception e) {
        log.Error("Error retrieving dive agencies", e);
      }
      return agencies;
    }
  }
}
