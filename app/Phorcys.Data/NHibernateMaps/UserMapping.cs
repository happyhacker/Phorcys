using System;
using DiveLog.Core;
using FluentNHibernate.Mapping;

namespace DiveLog.Data.NHibernateMaps
{
	public class UserMapping : ClassMap<User>
	{
		public UserMapping()
		{
			Table("Users");
			Id(x => x.UserId);
			Map(x => x.Created);
			Map(x => x.LastModified);
			Map(x => x.LoginCount);
			Map(x => x.LoginId);
			Map(x => x.Password);
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