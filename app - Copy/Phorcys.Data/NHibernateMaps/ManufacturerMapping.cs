using System;
using DiveLog.Core;
using FluentNHibernate.Mapping;

namespace DiveLog.Data.NHibernateMaps
{
	public class ManufacturerMapping : ClassMap<Manufacturer>
	{
		public ManufacturerMapping()
		{
			Table("Manufacturers");
			Schema("dbo");
			Id(x => x.ManufacturerId);
			References(x => x.Contact)
				.Class(typeof(Contact))
				.Not.Nullable()	
				.Column("ContactId")
				.Insert()
				.Update();
			HasMany(x => x.Gears)
				.KeyColumn("ManufacturerId")
				.Fetch.Select()
				.AsSet();
		}
	}
}