using System.Collections.Generic;
using Phorcys.Core;

namespace Phorcys.Services {
  interface IGearServices
  {
    Gear Save(Gear gear);
    //Tank Save(Tank tank);
    Gear Delete(Gear gear);
    Gear GetGear(int id);
    IList<Gear> GetAllForUser(int userId);
  }
}
