using System;
using System.Security.Cryptography.X509Certificates;
using Phorcys.Core;
using FluentNHibernate.Mapping;

namespace Phorcys.Data.NHibernateMaps
{
	public class ContactMap : ClassMap<Contact>
	{
		public ContactMap()
		{
			Table("Contacts");
      Id(x => x.Id, "ContactId").GeneratedBy.Identity().UnsavedValue(0);
			Map(x => x.Address1);
			Map(x => x.Address2);
			Map(x => x.Birthday);
			Map(x => x.CellPhone);
			Map(x => x.City);
			Map(x => x.Company);
			Map(x => x.Created);
			Map(x => x.Email);
			Map(x => x.FirstName);
			Map(x => x.Gender);
			Map(x => x.HomePhone);
			Map(x => x.LastModified);
			Map(x => x.LastName);
			Map(x => x.Notes);
			Map(x => x.PostalCode);
			Map(x => x.State);
			Map(x => x.WorkPhone);
			References(x => x.Country)
				.Class(typeof(Country))	
				.Column("CountryCode")
				.Insert()
				.Update();

			HasMany(x => x.DiveAgencies)
				.KeyColumn("ContactId")
				.Fetch.Select()
				.AsSet();
      /*
			HasMany(x => x.DiveLocations)
				.KeyColumn("ContactId")
				.Fetch.Select()
				.AsSet();
			HasMany(x => x.DiverCertifications)
				.KeyColumn("ContactId")
				.Fetch.Select()
				.AsSet();
       */
			HasMany(x => x.Divers)
				.KeyColumn("ContactId")
				.Fetch.Select()
				.AsSet();
        
			HasMany(x => x.DiveShops)
				.KeyColumn("ContactId")
				.Fetch.Select()
				.AsSet();

		    HasMany(x => x.Instructors)
		        .KeyColumn("ContactId")
		        .Fetch.Select()
		        .AsSet();

			HasMany(x => x.Manufacturers)
				.KeyColumn("ContactId")
				.Fetch.Select()
				.AsSet();
      /*
			HasMany(x => x.Gears)
				.KeyColumn("ContactId")
				.Fetch.Select()
				.AsSet();
			HasMany(x => x.InsurancePolicies)
				.KeyColumn("ContactId")
				.Fetch.Select()
				.AsSet();
			HasMany(x => x.SoldGears)
				.KeyColumn("ContactId")
				.Fetch.Select()
				.AsSet();*/
			References(x => x.User)
				.Class(typeof(User))
				.Not.Nullable()	
				.Column("UserId")
				.Insert()
				.Update();
      
			/*HasMany(x => x.Users)
				.KeyColumn("ContactId")
				.Fetch.Select()
				.AsSet();*/
		}
	}
}