using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Phorcys.UI.Web.Models {
  public class CertificationModel {
    public int Id { get; set; }
    public string Notes { get; set; }

    [Required]
    public string Title { get; set; }

    public int DiveAgencyId { get; set; }
    public IList<SelectListItem> DiveAgencyListItems { get; set; }

  }
}