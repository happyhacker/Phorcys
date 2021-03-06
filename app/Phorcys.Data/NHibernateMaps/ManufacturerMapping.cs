using System;
using Phorcys.Core;
using FluentNHibernate.Mapping;

namespace Phorcys.Data.NHibernateMaps
{
	public class ManufacturerMapping : ClassMap<Manufacturer>
	{
		public ManufacturerMapping()
		{
			Table("Manufacturers");
			Schema("dbo");
      Id(x => x.Id, "ManufacturerId").GeneratedBy.Identity().UnsavedValue(0);
			References(x => x.Contact)
				.Class(typeof(Contact))
				.Not.Nullable()	
				.Column("ContactId")
				.Insert()
				.Update();

			/*HasMany(x => x.Gears)
				.KeyColumn("ManufacturerId")
				.Fetch.Select()
				.AsSet();
       * */
		}
	}
}