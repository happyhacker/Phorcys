using System;
using DiveLog.Core;
using FluentNHibernate.Mapping;

namespace DiveLog.Data.NHibernateMaps
{
	public class InsurancePolicyMapping : ClassMap<InsurancePolicy>
	{
		public InsurancePolicyMapping()
		{
			Table("InsurancePolicies");
			Schema("dbo");
			Id(x => x.InsurancePolicyId);
			Map(x => x.Deductible);
			Map(x => x.EndOfTerm);
			Map(x => x.Notes);
			Map(x => x.StartOfTerm);
			Map(x => x.ValueAmount);
			References(x => x.Contact)
				.Class(typeof(Contact))
				.Not.Nullable()	
				.Column("ContactId")
				.Insert()
				.Update();
			HasManyToMany(x => x.Gears)
				.ChildKeyColumn("InsurancePolicyId")
				.ParentKeyColumn("GearId")
				.Table("InsuredGear")
				.Fetch.Select()
				.AsSet();
		}
	}
}