using System.Collections.Generic;
using Phorcys.Core;

namespace Phorcys.Services {
  interface IContactServices
  {
    Contact Save(Contact contact);
    Contact Delete(Contact contact);
    Contact GetContact(int id);
    IList<Contact> GetAllForUser(int userId);
  }
}
