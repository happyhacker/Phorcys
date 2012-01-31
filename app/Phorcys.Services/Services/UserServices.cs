﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phorcys.Core;
using SharpArch.Core.PersistenceSupport;

namespace Phorcys.Services.Services {
  public class UserServices {
    public UserServices(IRepository<User> userRepository) {
      this.userRepository = userRepository;
    }
  private readonly IRepository<User> userRepository;  

    public User FindUser(string userId) {
      User user = new User();
      Dictionary<string, object> dictionary = new Dictionary<string, object>();

      dictionary.Add("LoginId", userId);
      //dictionary.Add("Password", password);
      user = userRepository.FindOne(dictionary);
      return user;
    }
  }
}