using System;
using System.Collections.Generic;
using SharpArch.Data.NHibernate;
using Phorcys.Core;
using Phorcys.Data;
using log4net;


namespace Phorcys.Services
{
    public class ManufacturerServices : IManufacturerServices
    {
        public IPhorcysRepository<Manufacturer> ManufacturerRepository = new PhorcysRepository<Manufacturer>();

        protected static readonly ILog log = LogManager.GetLogger(typeof(ManufacturerServices));

        public Manufacturer Save(Manufacturer manufacturer)
        {
            try
            {
                //manufacturer.LastModifed = System.DateTime.Now;
  
                ManufacturerRepository.SaveOrUpdate(manufacturer);
                ManufacturerRepository.DbContext.CommitChanges();
            }
            catch (Exception e)
            {
                log.Error("Unable to save manufacturer ", e);
                throw e;
            }
            return manufacturer;
        }

        public void Delete(Manufacturer manufacturer)
        {
 
            try
            {
                ManufacturerRepository.Delete(manufacturer);
                ManufacturerRepository.DbContext.CommitChanges();
            }
            catch (Exception e)
            {
                log.Error("Cound not delete manufacturer. Something else probably references this manufacturer",e);
                throw e;
            }
            return;

        }

        public Manufacturer GetManufacturer(int id)
        {
            return ManufacturerRepository.Get(id);
        }

        public IList<Manufacturer> GetAllForUser(int userId)
        {
            UserServices userServices = new UserServices(new Repository<User>());
            User systemUser = userServices.FindUser("system");
            IList<Manufacturer> manufacturers = null;
            try
            {
                manufacturers = ManufacturerRepository.GetAllSystemAndUser(userId, systemUser.Id);
            }
            catch (Exception e)
            {
                log.Error("Error retrieving manufacturers",e);
            }
            return manufacturers;
        }
    }
}
