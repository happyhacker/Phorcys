using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
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

      public IList<Instructor> GetAll()
      {
          IList<Instructor> instructors = null;
          try {
            instructors = InstructorRepository.GetAll();
          } catch (Exception e) {
            log.Error("Error retrieving instructors", e);
          }
          return instructors;
   
      } 
        
      public IList<Instructor> GetAllForUser(int userId)
        {
            UserServices userServices = new UserServices(new Repository<User>());
            User systemUser = userServices.FindUser("system");
            IList<Instructor> instructors = null;
            try
            {
                instructors = InstructorRepository. GetAll();
            }
            catch (Exception e)
            {
                log.Error("Error retrieving instructors",e);
            }
            return instructors;
        }

        public IList<SelectListItem> BuildListItems(int? instructorId, int userId) {
          IList<SelectListItem> instructorList = new List<SelectListItem>();
          IList<Instructor> instructors = GetAllForUser(userId);
          SelectListItem instructorItem;

          instructors = instructors.OrderBy(m => m.Contact.FirstName).ToList();
          foreach (var instructor in instructors) {
            instructorItem = new SelectListItem();
            instructorItem.Text = instructor.Contact.FirstName + " " + instructor.Contact.LastName;
            instructorItem.Value = instructor.Id.ToString();
            if (instructorId != null) {
              if (instructor.Id == instructorId) {
                instructorItem.Selected = true;
              }
            }
            instructorList.Add(instructorItem);
          }

          return instructorList;
        }

    }
}
