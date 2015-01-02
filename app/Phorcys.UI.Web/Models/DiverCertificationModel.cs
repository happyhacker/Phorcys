using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Phorcys.UI.Web.Models {
  public class DiverCertificationModel {
    public int Id { get; set; }

    public int DiverId { get; set; }
    public int CertificationId { get; set; }
    public DateTime? Certified { get; set; }
    public string CertificationNum { get; set; }
    public string Notes { get; set; }
    public DateTime Created { get; set; }
    public DateTime LastModified { get; set; }
    public int InstructorId { get; set; }

    public IList<SelectListItem> DiveAgencyListItems { get; set; }
    public IList<SelectListItem> CertificationListItems { get; set; }
    public IList<SelectListItem> InstructorListItems { get; set; }
  }
}