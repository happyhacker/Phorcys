﻿using System.Collections.Generic;
using System.Web.Mvc;
using Phorcys.Core;

namespace Phorcys.Services {
  public interface IDiveAgencyServices {
    DiveAgency Save(DiveAgency agency);
    void Delete(DiveAgency agency);
    DiveAgency GetDiveAgency(int id);
    IList<DiveAgency> GetAllForUser(int userId);
    IList<SelectListItem> BuildList(int? DiveAgencyId);
  }
}
