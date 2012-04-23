using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Criterion;
using Phorcys.Core;
using Phorcys.Data;
using Phorcys.Services.Services;
using SharpArch.Core.PersistenceSupport;
using SharpArch.Data.NHibernate;

namespace Phorcys.Services {
  public class DiveSiteServices {
    private IDiveSiteRepository<DiveSite> diveSiteRepository = new DiveSiteRepository<DiveSite>();

    public IList<DiveSite> GetAllForUser(int userId) {
      UserServices userServices = new UserServices(new Repository<User>());
      User systemUser = userServices.FindUser("system");

      IList<DiveSite> diveSites = diveSiteRepository.GetAllForUser(userId, systemUser.Id);

      return diveSites;
    }
  }
}
