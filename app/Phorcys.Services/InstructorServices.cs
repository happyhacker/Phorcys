using System;
using System.Collections.Generic;
using SharpArch.Data.NHibernate;
using Phorcys.Core;
using Phorcys.Data;
using log4net;


namespace Phorcys.Services
{
    public class InstructorServices : IInstructorServices
    {
        public IPhorcysRepository<Instructor> InstructorRepository = new PhorcysRepository<Instructor>();

        protected static readonly ILog log = LogManager.GetLogger(typeof(InstructorServices));

        public Instructor Save(Instructor instructor)
        {
            try
            {
                //instructor.LastModifed = System.DateTime.Now;
  
                InstructorRepository.SaveOrUpdate(instructor);
                InstructorRepository.DbContext.CommitChanges();
            }
            catch (Exception e)
            {
                log.Error("Unable to save instructor ", e);
                throw e;
            }
            return instructor;
        }

        public void Delete(Instructor instructor)
        {
 
            try
            {
                InstructorRepository.Delete(instructor);
                InstructorRepository.DbContext.CommitChanges();
            }
            catch (Exception e)
            {
                log.Error("Cound not delete instructor. Something else probably references this instructor",e);
                throw e;
            }
            return;

        }

        public Instructor GetInstructor(int id)
        {
            return InstructorRepository.Get(id);
        }

        public IList<Instructor> GetAllForUser(int userId)
        {
            UserServices userServices = new UserServices(new Repository<User>());
            User systemUser = userServices.FindUser("system");
            IList<Instructor> instructors = null;
            try
            {
                instructors = InstructorRepository.GetAllSystemAndUser(userId, systemUser.Id);
            }
            catch (Exception e)
            {
                log.Error("Error retrieving instructors",e);
            }
            return instructors;
        }
    }
}
