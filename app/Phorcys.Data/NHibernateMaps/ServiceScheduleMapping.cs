using System;
using DiveLog.Core;
using FluentNHibernate.Mapping;

namespace DiveLog.Data.NHibernateMaps
{
	public class ServiceScheduleMapping : ClassMap<ServiceSchedule>
	{
		public ServiceScheduleMapping()
		{
			Table("ServiceSchedules");
			Schema("dbo");
			Id(x => x.ServiceScheduleId);
			Map(x => x.NextServiceDate);
			Map(x => x.Notes);
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