using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phorcys.Core;
using SharpArch.Core.PersistenceSupport;
using SharpArch.Data.NHibernate;
using log4net;

namespace Phorcys.Services {
  public class UserServices {
    protected static readonly ILog log = LogManager.GetLogger(typeof(ContactServices));
    private readonly IRepository<User> userRepository;

    public UserServices() {
      userRepository = new Repository<User>();
    }

    public UserServices(IRepository<User> userRepository) {
      this.userRepository = userRepository;
    }

    public User FindUser(string userId) {
      User user = new User();
      Dictionary<string, object> dictionary = new Dictionary<string, object>();

      dictionary.Add("LoginId", userId);
      //dictionary.Add("Password", password);
      user = userRepository.FindOne(dictionary);
      return user;
    }

    public User Save(User user)
    {
        try
        {
            userRepository.SaveOrUpdate(user);
            userRepository.DbContext.CommitChanges();
        }
        catch (Exception e)
        {
            log.Error("Unable to save user " + user.LoginId, e);
            throw e;
        }
        return user;

    }
  }
}
