using System.Collections.Generic;
using Phorcys.Core;

namespace Phorcys.Services {
  interface IDiverServices
  {
    Diver Save(Diver contact);
    Diver Delete(Diver contact);
    Diver GetDiver(int id);
    IList<Diver> GetAllForUser(int userId);
  }
}
