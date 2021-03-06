using System;
using DiveLog.Core;
using FluentNHibernate.Mapping;

namespace DiveLog.Data.NHibernateMaps
{
	public class GearServiceEventMapping : ClassMap<GearServiceEvent>
	{
		public GearServiceEventMapping()
		{
			Table("GearServiceEvents");
			Schema("dbo");
			Id(x => x.GearServiceEventsId);
			Map(x => x.Cost);
			Map(x => x.Notes);
			Map(x => x.ServicedDate);
			Map(x => x.Title);
			References(x => x.Gear)
				.Class(typeof(Gear))
				.Not.Nullable()	
				.Column("GearId")
				.Insert()
				.Update();
		}
	}
}