using System.Collections.Generic;
using Phorcys.Core;
using Phorcys.Data;

namespace Phorcys.Services {
  public interface ILocationServices {
    DiveLocation Get(int id);
    IList<DiveLocation> GetAllSystemAndUser(int systemId, int userId);
  }
}