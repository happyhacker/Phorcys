using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Phorcys.Core;

namespace Phorcys.UI.Web.Models {
  public class ContactsIndexModel {
    public int ContactId { get; set; }
    public string Company { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }   
    public string Email { get; set; }
    public string EmailLink {
      get {
        return (Email != null ? "<A href=mailto:" + Email + ">" + Email + "</a>" : ""); 
      }
    }
    public string User { get; set; }
    public string tags { get; set;}
  }
}