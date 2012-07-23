using System.Collections.Generic;
using Phorcys.Core;

namespace Phorcys.Services {
  interface IGearServices
  {
    Gear Create(Gear gear);
    Gear Delete(Gear gear);
    Gear Get(int id);
    IList<Gear> GetAllSystemAndUser(int systemId, int userId);
    Gear Save(Gear gear);
  }
}
