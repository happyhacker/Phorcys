using System;
using Phorcys.Core;
using FluentNHibernate.Mapping;

namespace Phorcys.Data.NHibernateMaps
{
	public class CountryMap : ClassMap<Country>
	{
		public CountryMap()
		{
			Table("Countries");
			Schema("dbo");
      Id(x => x.CountryCode, "CountryCode").GeneratedBy.Assigned();
			Map(x => x.Name, "Name");
      //HasMany(x => x.Contacts)
      //  .KeyColumn("CountryCode")
      //  .Fetch.Select()
      //  .AsSet();
		}
	}
}