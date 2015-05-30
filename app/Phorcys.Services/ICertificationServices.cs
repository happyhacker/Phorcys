using System.Collections.Generic;
using System.Web.Mvc;
using Phorcys.Core;

namespace Phorcys.Services {
  public interface ICertificationServices {
    void Delete(Certification certification);
    Certification Get(int id);
    IList<Certification> GetAllForUser(int userId);
    IList<Certification> GetCertificationsForAgency(int agencyId, int systemId, int userId);
    void Save(Certification certification);
    IList<SelectListItem> BuildSelectListForAgency(int agencyId, int? certificationId, int userId, int systemId);
  }
}
