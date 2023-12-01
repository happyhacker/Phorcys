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
    public class DivePlanServices : IDivePlanServices
    {
        private IDivePlanRepository<DivePlan> divePlanRepository = new DivePlanRepository<DivePlan>();
        private IPhorcysRepository<DiveSite> phorcysRepository = new PhorcysRepository<DiveSite>();
        private static readonly ILog log = LogManager.GetLogger(typeof(DivePlanServices));

        public void Delete(DivePlan divePlanToDelete)
        {
            divePlanRepository.Delete(divePlanToDelete);

            try
            {
                divePlanRepository.DbContext.CommitChanges();
            }
            catch (Exception e)
            {
                log.Error(e.Message + " Inner Exception: " + e.InnerException.Message);
                divePlanRepository.DbContext.RollbackTransaction();
                throw new Exception("A problem was encountered preventing the divePlan from being deleted. " +
                                    "Another item likely depends on this divePlan.");

            }

        }

        public DivePlan Get(int id)
        {
            DivePlan divePlan = divePlanRepository.Get(id);
            return divePlan;
        }

        public IList<DivePlan> GetAllForUser(int userId)
        {
            UserServices userServices = new UserServices(new Repository<User>());
            User systemUser = userServices.FindUser("system");

            IList<DivePlan> divePlans = divePlanRepository.GetAllForUser(userId, systemUser.Id);

            return divePlans;
        }

        public void Save(DivePlan divePlan)
        {
            DivePlan savedDivePlan = divePlanRepository.SaveOrUpdate(divePlan);
            divePlanRepository.DbContext.CommitChanges();
        }
    }
}
