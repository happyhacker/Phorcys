using System.Collections.Generic;
using Phorcys.Core;

namespace Phorcys.Services
{
  public interface IDiveSiteServices
  {
    IList<DiveSite> GetDiveSitesForLocation(int locationId, int systemId, int userId);
    IList<DiveSite> GetAllForUser(int userId);
  }
}