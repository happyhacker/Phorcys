using System;
using System.Collections.Generic;
using SharpArch.Data.NHibernate;
using Phorcys.Core;
using Phorcys.Data;


namespace Phorcys.Services {
  public class ContactServices : IContactServices {
    public IPhorcysRepository<Contact> ContactRepository = new PhorcysRepository<Contact>();

    public Contact Save(Contact contact) {
      return new Contact();
    }

    public Contact Delete(Contact contact) {
      return null;
    }

    public Contact GetContact(int id) {
      return new Contact();
    }

    public IList<Contact> GetAllForUser(int userId) {
      UserServices userServices = new UserServices(new Repository<User>());
      User systemUser = userServices.FindUser("system");

      IList<Contact> contact = ContactRepository.GetAllSystemAndUser(userId, systemUser.Id);

      return contact;
    }
  }
}
