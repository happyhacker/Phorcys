using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Phorcys.Core;

namespace Phorcys.Data.NHibernateMaps {

  public class UserMap : ClassMap<User> {

    public UserMap() {
      //Table("Users");
      ////References(x => x.DiveLocationId).Column("DiveLocationId");
      //Id(x => x.Id, "UserId").GeneratedBy.Identity().UnsavedValue(0);

      ////References(x => x.Dives).Column("Dives");
      
      //Map(x => x.LoginId, "LoginId").Length(30).Not.Nullable();
      //Map(x => x.Password, "Password").Length(20).Not.Nullable();
      //Map(x => x.LoginCount, "LoginCount");
      ////Map(x => x.ContactId, "ContactId");

      Table("Users");
      Id(x => x.Id, "UserId").GeneratedBy.Identity().UnsavedValue(0);
      Map(x => x.Created, "Created");
      Map(x => x.LastModified, "LastModified");
      Map(x => x.LoginCount, "LoginCount");
      Map(x => x.LoginId,"LoginId");
      Map(x => x.Password, "Password");
      HasMany(x => x.Attributes)
        .KeyColumn("UserId")
        .Fetch.Select()
        .AsSet();
      HasMany(x => x.Certifications)
        .KeyColumn("UserId")
        .Fetch.Select()
        .AsSet();
      References(x => x.Contact)
        .Class(typeof(Contact))
        .Column("ContactId")
        .Insert()
        .Update();
      HasMany(x => x.Contacts)
        .KeyColumn("UserId")
        .Fetch.Select()
        .AsSet();
      HasMany(x => x.DiveLocations)
        .KeyColumn("UserId")
        .Fetch.Select()
        .AsSet();
      HasMany(x => x.Dives)
        .KeyColumn("UserId")
        .Fetch.Select()
        .AsSet();
      HasMany(x => x.DiveSites)
        .KeyColumn("UserId")
        .Fetch.Select()
        .AsSet();
      HasMany(x => x.DiveTypes)
        .KeyColumn("UserId")
        .Fetch.Select()
        .AsSet();
      HasMany(x => x.Friends)
        .KeyColumn("UserId")
        .Fetch.Select()
        .AsSet();
      HasMany(x => x.Friends1)
        .KeyColumn("UserId")
        .Fetch.Select()
        .AsSet();
      HasMany(x => x.Gears)
        .KeyColumn("UserId")
        .Fetch.Select()
        .AsSet();
      HasMany(x => x.Qualifications)
        .KeyColumn("UserId")
        .Fetch.Select()
        .AsSet();
      HasMany(x => x.Roles)
        .KeyColumn("UserId")
        .Fetch.Select()
        .AsSet();
    }
  }
}
