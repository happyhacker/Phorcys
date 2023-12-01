using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using NHibernate.Criterion;
using Phorcys.Core;
using Phorcys.Data;
using Phorcys.Services.Services;
using SharpArch.Data.NHibernate;

namespace Phorcys.Services
{
    public class DiveSiteServices : IDiveSiteServices
    {
        private IDiveSiteRepository<DiveSite> diveSiteRepository = new DiveSiteRepository<DiveSite>();
        private IPhorcysRepository<DiveSite> phorcysRepository = new PhorcysRepository<DiveSite>();
        private static readonly ILog log = LogManager.GetLogger(typeof(DiveSiteServices));
        public void Delete(DiveSite diveSiteToDelete)
        {
            diveSiteRepository.Delete(diveSiteToDelete);

            try
            {
                diveSiteRepository.DbContext.CommitChanges();
            }
            catch (Exception e)
            {
                log.Error(e.Message + " Inner Exception: " + e.InnerException.Message); 
                diveSiteRepository.DbContext.RollbackTransaction();
                throw new Exception("A problem was encountered preventing the diveSite from being deleted. " +
                                    "Another item likely depends on this diveSite.");

            }

        }

        public DiveSite Get(int id)
        {
            DiveSite diveSite = diveSiteRepository.Get(id);
            return diveSite;
        }

        public IList<DiveSite> GetDiveSitesForLocation(int locationId, int systemId, int userId)
        {
            IList<DiveSite> diveSites;
            DetachedCriteria query = DetachedCriteria.For(typeof(DiveSite));

            query.Add(Expression.Or(Expression.Eq("User.Id", userId), Expression.Eq("User.Id", systemId)));
            query.Add(Expression.Eq("DiveLocation.Id", locationId));
            diveSites = phorcysRepository.GetByCriteria(query);

            return diveSites;
        }

        public IList<DiveSite> GetAllForUser(int userId)
        {
            UserServices userServices = new UserServices(new Repository<User>());
            User systemUser = userServices.FindUser("system");

            IList<DiveSite> diveSites = diveSiteRepository.GetAllForUser(userId, systemUser.Id);

            return diveSites;
        }

        public void Save(DiveSite diveSite)
        {
            DiveSite savedDiveSite = diveSiteRepository.SaveOrUpdate(diveSite);
            diveSiteRepository.DbContext.CommitChanges();
        }
    }
}
