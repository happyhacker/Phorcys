using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Criterion;
using Phorcys.Core;
using Phorcys.Data;
using Phorcys.Services.Services;
using SharpArch.Data.NHibernate;

namespace Phorcys.Services
{
    public class DiveServices : IDiveServices
    {
        private IDiveRepository<Dive> diveRepository = new DiveRepository<Dive>();
        private IPhorcysRepository<DiveSite> phorcysRepository = new PhorcysRepository<DiveSite>();

        public void Delete(Dive diveToDelete)
        {
            diveRepository.Delete(diveToDelete);

            try
            {
                diveRepository.DbContext.CommitChanges();
            }
            catch (Exception e)
            {
                diveRepository.DbContext.RollbackTransaction();
                throw new Exception("A problem was encountered preventing the dive from being deleted. " +
                                    "Another item likely depends on this divePlan.");

            }

        }

        public Dive Get(int id)
        {
            Dive dive = diveRepository.Get(id);
            return dive;
        }

        public IList<Dive> GetAllForUser(int userId)
        {
            UserServices userServices = new UserServices(new Repository<User>());
            User systemUser = userServices.FindUser("system");

            IList<Dive> dives = diveRepository.GetAllForUser(userId, systemUser.Id);

            return dives;
        }

        public void Save(Dive dive)
        {
            Dive savedDive = diveRepository.SaveOrUpdate(dive);
            diveRepository.DbContext.CommitChanges();
        }
    }
}
