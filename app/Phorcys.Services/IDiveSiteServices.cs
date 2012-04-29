using System.Collections.Generic;
using Phorcys.Core;

namespace Phorcys.Services {
  public interface IDiveSiteServices {
    void Delete(DiveSite diveSite);
    DiveSite Get(int id);
    IList<DiveSite> GetAllForUser(int userId);
    IList<DiveSite> GetDiveSitesForLocation(int locationId, int systemId, int userId);
    void Save(DiveSite diveSite);
  }
}
