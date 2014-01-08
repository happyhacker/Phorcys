using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Phorcys.Core;

namespace Phorcys.UI.Web.Models {
  public class ContactModel {

    public int ContactId { get; set; }
    public string Company { get; set; }
    [DisplayName("First Name")]
    public string FirstName { get; set; }
    [DisplayName("Last Name")]
    public string LastName { get; set; }    
    public string Address1 { get; set; }
    [DisplayName("Address 1")]
    public string Address2 { get; set; }
    [DisplayName("Address 2")]
    public string City { get; set; }
    public string State { get; set; }
    public string PostalCode { get; set; }
    public Country Country { get; set; }
    public IList<SelectListItem> Countries { get; set; }
    public DateTime? Birthday { get; set; }
 
    public string HomePhone { get; set; }
    [DisplayName("Home Phone")]
    public string CellPhone { get; set; }
    [DisplayName("Cell Phone")]
    public string WorkPhone { get; set; }
    [DisplayName("Work Phone")]
    public string Email { get; set; }

    public string Gender { get; set; }

    public DateTime Created { get; set; }
    public DateTime LastModified { get; set; }

    public string Notes { get; set; }

    public bool isDiver { get; set; }
    public bool isDiveShop { get; set; }
    public bool isInstructor { get; set; }
    public bool isAgency { get; set; }
    public bool isManufacturer { get; set; }
  }
}
