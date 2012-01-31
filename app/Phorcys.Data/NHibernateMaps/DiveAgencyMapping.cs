using System;
using DiveLog.Core;
using FluentNHibernate.Mapping;

namespace DiveLog.Data.NHibernateMaps
{
	public class DiveAgencyMapping : ClassMap<DiveAgency>
	{
		public DiveAgencyMapping()
		{
			Table("DiveAgencies");
			Schema("dbo");
			Id(x => x.DiveAgencyId);
			Map(x => x.Notes);
			HasMany(x => x.Certifications)
				.KeyColumn("DiveAgencyId")
				.Fetch.Select()
				.AsSet();
			References(x => x.Contact)
				.Class(typeof(Contact))
				.Not.Nullable()	
				.Column("ContactId")
				.Insert()
				.Update();
		}
	}
}