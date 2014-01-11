using System;
using System.Collections.Generic;
using SharpArch.Data.NHibernate;
using Phorcys.Core;
using Phorcys.Data;
using log4net;


namespace Phorcys.Services {
  public class ContactServices : IContactServices {
    public IPhorcysRepository<Contact> ContactRepository = new PhorcysRepository<Contact>();

    protected static readonly ILog log = LogManager.GetLogger(typeof(ContactServices));

    public Contact Save(Contact contact) {
      try {
        contact.LastModified = System.DateTime.Now;
        if (contact.Address1 == null) contact.Address1 = "";
        if (contact.Address2 == null) contact.Address2 = "";
        if (contact.CellPhone == null) contact.CellPhone = "";
        if (contact.City == null) contact.City = "";
        if (contact.Company == null) contact.Company = "";
        if (contact.Email == null) contact.Email = "";
        if (contact.FirstName == null) contact.FirstName = "";
        if (contact.Gender == null) contact.Gender = "";
        if (contact.HomePhone == null) contact.HomePhone = "";
        if (contact.LastName == null) contact.LastName = "";
        if (contact.PostalCode == null) contact.PostalCode = "";
        if (contact.State == null) contact.State = "";
        if (contact.WorkPhone == null) contact.WorkPhone = "";

        ContactRepository.SaveOrUpdate(contact);
        ContactRepository.DbContext.CommitChanges();
      } catch (Exception e) {
        log.Error("Unable to save contact " + contact.Company + ", " + contact.FirstName + ", " + contact.LastName + ". ", e);
        throw e;
      }
      return contact;
    }

    public Contact Delete(Contact contact) {
      Contact retVal = new Contact();

      try {
        ContactRepository.Delete(contact);
        ContactRepository.DbContext.CommitChanges();
      } catch (Exception e) {
        log.Error("Cound not delete Contact" + contact.Company + ", " + contact.FirstName + " " + ". Something else probably references this contact");
        throw e;
      }
      return retVal;

    }

    public Contact GetContact(int id) {
      return ContactRepository.Get(id);
    }

    public IList<Contact> GetAllForUser(int userId) {
      UserServices userServices = new UserServices(new Repository<User>());
      User systemUser = userServices.FindUser("system");
      IList<Contact> contact = null;
      try {
        contact = ContactRepository.GetAllSystemAndUser(userId, systemUser.Id);
      } catch (Exception e) {
        log.Error("Error retrieving contacts");
      }
      return contact;
    }
  }
}
