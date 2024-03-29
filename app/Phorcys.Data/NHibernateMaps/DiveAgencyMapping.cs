using System;
using Phorcys.Core;
using FluentNHibernate.Mapping;

namespace Phorcys.Data.NHibernateMaps
{
	public class DiveAgencyMapping : ClassMap<DiveAgency>
	{
		public DiveAgencyMapping()
		{
			Table("DiveAgencies");
      Id(x => x.Id, "DiveAgencyId").GeneratedBy.Identity().UnsavedValue(0);
			Map(x => x.Notes);
			References(x => x.Contact)
				.Class(typeof(Contact))
				.Not.Nullable()	
				.Column("ContactId")
				.Insert()
				.Update();

      /*
			HasMany(x => x.Certifications)
				.KeyColumn("DiveAgencyId")
				.Fetch.Select()
				.AsSet();
       */
		}
	}
}