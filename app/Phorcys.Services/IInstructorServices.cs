using System.Collections.Generic;
using Phorcys.Core;

namespace Phorcys.Services {
  interface IInstructorServices
  {
    Instructor Save(Instructor instructor);
    void Delete(Instructor instructor);
    Instructor GetInstructor(int id);
    IList<Instructor> GetAllForUser(int userId);
  }
}
