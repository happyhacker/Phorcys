using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using NHibernate.Criterion;
using SharpArch.Data.NHibernate;
using Phorcys.Core;
using Phorcys.Data;
using log4net;


namespace Phorcys.Services
{
    public class DiverServices : IDiverServices
    {
        public IPhorcysRepository<Diver> DiverRepository = new PhorcysRepository<Diver>();

        protected static readonly ILog log = LogManager.GetLogger(typeof(DiverServices));

        public Diver Save(Diver diver)
        {
            try
            {
                diver.LastModifed = System.DateTime.Now;
  
                DiverRepository.SaveOrUpdate(diver);
                DiverRepository.DbContext.CommitChanges();
            }
            catch (Exception e)
            {
                log.Error("Unable to save diver ", e);
                throw e;
            }
            return diver;
        }

        public Diver Delete(Diver diver)
        {
            Diver retVal = new Diver();

            try
            {
                DiverRepository.Delete(diver);
                DiverRepository.DbContext.CommitChanges();
            }
            catch (Exception e)
            {
                log.Error("Cound not delete diver. Something else probably references this contact",e);
                throw e;
            }
            return retVal;

        }

        public Diver GetDiver(int id)
        {
            return DiverRepository.Get(id);
        }

      public Diver GetDiverByContact(int contactId)
      {
         DetachedCriteria criteria = DetachedCriteria.For(typeof(Diver));
        criteria.Add(Expression.Eq("Contact.Id", contactId));
        IList<Diver> diverList = DiverRepository.GetByCriteria(criteria);
        return diverList[0];
      }

        public IList<Diver> GetAllForUser(int userId)
        {
            UserServices userServices = new UserServices(new Repository<User>());
            User systemUser = userServices.FindUser("system");
            IList<Diver> diver = null;
            try
            {
                diver = DiverRepository.GetAllSystemAndUser(userId, systemUser.Id);
            }
            catch (Exception e)
            {
                log.Error("Error retrieving contacts",e);
            }
            return diver;
        }
    }
}
