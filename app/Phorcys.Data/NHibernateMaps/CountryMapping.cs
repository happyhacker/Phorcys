using System;
using DiveLog.Core;
using FluentNHibernate.Mapping;

namespace DiveLog.Data.NHibernateMaps
{
	public class CountryMapping : ClassMap<Country>
	{
		public CountryMapping()
		{
			Table("Countries");
			Schema("dbo");
			Id(x => x.CountryCode);
			Map(x => x.Name);
			HasMany(x => x.Contacts)
				.KeyColumn("CountryCode")
				.Fetch.Select()
				.AsSet();
		}
	}
}