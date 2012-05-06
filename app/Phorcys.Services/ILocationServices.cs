using System.Collections.Generic;
using Phorcys.Core;
using Phorcys.Data;

namespace Phorcys.Services {
  public interface ILocationServices {
    DiveLocation Create(DiveLocation location);
    DiveLocation Delete(DiveLocation location);
    DiveLocation Get(int id);
    IList<DiveLocation> GetAllSystemAndUser(int systemId, int userId);
    DiveLocation Save(DiveLocation location);
  }
}