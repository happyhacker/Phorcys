using System;
using Phorcys.Core;
using FluentNHibernate.Mapping;

namespace Phorcys.Data.NHibernateMaps
{
	public class GearMapping : ClassMap<Gear>
	{
		public GearMapping()
		{
			Table("Gear");
            Id(x => x.Id, "GearId").GeneratedBy.Identity().UnsavedValue(0);
			Map(x => x.Acquired);
			Map(x => x.NoLongerUse);
			Map(x => x.Notes);
			Map(x => x.Paid);
			Map(x => x.RetailPrice);
			Map(x => x.Sn, "SN");
			Map(x => x.Title);
			Map(x => x.Weight);
		  Map(x => x.Created);
		  Map(x => x.LastModified);
			/*References(x => x.Contact)
				.Class(typeof(Contact))	
				.Column("PurchasedFromContactId")
				.Insert()
				.Update();
			HasManyToMany(x => x.DiveDetails)
				.ChildKeyColumn("GearId")
				.ParentKeyColumn("DiveDetailsId")
				.Table("GearOnDive")
				.Fetch.Select()
				.AsSet();
			HasManyToMany(x => x.Divers)
				.ChildKeyColumn("GearId")
				.ParentKeyColumn("DiverId")
				.Table("DiverGear")
				.Fetch.Select()
				.AsSet();
			HasMany(x => x.GearServiceEvents)
				.KeyColumn("GearId")
				.Fetch.Select()
				.AsSet();
			HasManyToMany(x => x.InsurancePolicies)
				.ChildKeyColumn("GearId")
				.ParentKeyColumn("InsurancePolicyId")
				.Table("InsuredGear")
				.Fetch.Select()
				.AsSet();
			References(x => x.Manufacturer)
				.Class(typeof(Manufacturer))	
				.Column("ManufacturerId")
				.Insert()
				.Update();
			HasMany(x => x.ServiceSchedules)
				.KeyColumn("GearId")
				.Fetch.Select()
				.AsSet();
			HasOne(x => x.SoldGear)
				.Class(typeof(SoldGear))
				.PropertyRef(x => x.Gear)
				.Cascade
				.All();
*/

          References(x => x.Tank)
              //.Class(typeof (Tank))
            .Column("GearId")
            .Nullable()
            .Cascade.None()
            //.Not.LazyLoad()
            .ReadOnly();
      
			References(x => x.User)
				.Class(typeof(User))
				.Not.Nullable()	
				.Column("UserId")
				.Insert()
				.Update();
		}
	}
}