using System.Collections.Generic;
using Phorcys.Core;

namespace Phorcys.Services {
  public interface IDivePlanServices {
    void Delete(DivePlan divePlan);
    DivePlan Get(int id);
    IList<DivePlan> GetAllForUser(int userId);
    //void Save(DivePlan divePlan);
  }
}
