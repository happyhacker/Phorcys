using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Phorcys.Core;

namespace Phorcys.UI.Web.Models {
  public class ContactsModel {
    public int ContactId { get; set; }
    public string Address1 { get; set; }
    public string Address2 { get; set; }
    public string Date { get; set; }
    public string CellPhone { get; set; }
    public string City { get; set; }
    public string Company { get; set; }
    public DateTime Created { get; set; }
    public string Email { get; set; }
    [Required(ErrorMessage = "*")]
    public string FirstName { get; set; }
    public string Gender { get; set; }
    public string HomePhone { get; set; }
    public DateTime LastModified { get; set; }
    [Required(ErrorMessage = "*")]
    public string LastName { get; set; }
    public string Notes { get; set; }
    public string PostalCode { get; set; }
    public string State { get; set; }
    public string WorkPhone { get; set; }
    //public Country Country { get; set; }
  }
}