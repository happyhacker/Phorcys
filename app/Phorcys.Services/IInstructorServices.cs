using System.Collections.Generic;
using System.Web.Mvc;
using Phorcys.Core;

namespace Phorcys.Services {
  interface IInstructorServices
  {
    Instructor Save(Instructor instructor);
    void Delete(Instructor instructor);
    Instructor GetInstructor(int id);
    IList<Instructor> GetAllForUser(int userId);
    IList<SelectListItem> BuildListItems(int? instructorId, int userId);
  }
}
