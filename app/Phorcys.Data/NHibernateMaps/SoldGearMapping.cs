using System;
using Phorcys.Core;
using FluentNHibernate.Mapping;

namespace Phorcys.Data.NHibernateMaps
{
	public class SoldGearMapping : ClassMap<SoldGear>
	{
		public SoldGearMapping()
		{
			Table("SoldGear");
			Schema("dbo");
            Id(x => x.GearId, "GearId");
			Map(x => x.Amount);
			Map(x => x.Notes);
			Map(x => x.SoldOn);
			References(x => x.Contact)
				.Class(typeof(Contact))	
				.Column("SoldToContactId")
				.Insert()
				.Update();
			References(x => x.Gear)
				.Unique()	
				.Column("GearId")
				.Insert()
				.Update();
		}
	}
}