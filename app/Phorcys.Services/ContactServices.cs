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
        ContactRepository.SaveOrUpdate(contact);
        ContactRepository.DbContext.CommitChanges();
      }
      catch (Exception e) {
        log.Error("Unable to save contact " + contact.Company + ", " + contact.FirstName + ", " + contact.LastName +". ", e);
        throw e;
      }
      return contact;
    }

    public Contact Delete(Contact contact) {
      Contact retVal = new Contact();

      try {
        ContactRepository.Delete(contact);
        ContactRepository.DbContext.CommitChanges();
      }
      catch (Exception e) {
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

      IList<Contact> contact = ContactRepository.GetAllSystemAndUser(userId, systemUser.Id);

      return contact;
    }
  }
}
