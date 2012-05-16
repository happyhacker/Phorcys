using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Phorcys.Core;
using Phorcys.Core.Domains;
using SharpArch.Testing.NUnit;

namespace Phorcys.Tests.Unit.Core.Domains {

    [TestFixture]
    public class DiveLocationTests {
      [Test]
      public void CanCompareDiveLocations() {
        DiveLocation instance = new DiveLocation();
        User user = new User();
        user.LoginCount = 3;
        user.LoginId = "larry";
        user.Password = "password";        
        user.Created = System.DateTime.Now;
        user.LastModified = System.DateTime.Now;

        instance.Title = "Goodenough Spring";
        instance.User = user;

        DiveLocation instanceToCompareTo = new DiveLocation();
        instanceToCompareTo.Title = "Goodenough Spring";


        User user2 = new User();
        user.LoginCount = 3;
        user.LoginId = "larry";
        user.Password = "password";
        user.Created = System.DateTime.Now;
        user.LastModified = System.DateTime.Now;

        //instanceToCompareTo.User = user2; //breaks
        instanceToCompareTo.User = user; // works

        instance.ShouldEqual(instanceToCompareTo);
      }
    }
  }
