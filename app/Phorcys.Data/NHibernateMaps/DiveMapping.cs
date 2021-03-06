using System;
using Phorcys.Core;
using FluentNHibernate.Mapping;

namespace DiveLog.Data.NHibernateMaps
{
	public class DiveMapping : ClassMap<Dive>
	{
		public DiveMapping()
		{
			Table("Dives");
			Schema("dbo");
			Id(x => x.DiveId);
			Map(x => x.AdditionalWeight);
			Map(x => x.AvgDepth);
			Map(x => x.Created);
			Map(x => x.DescentTime);
			Map(x => x.DiveNumber);
			Map(x => x.LastModified);
			Map(x => x.MaxDepth);
			Map(x => x.Minutes);
			Map(x => x.Notes);
			Map(x => x.Temperature);
			HasMany(x => x.DiveUrls)
				.KeyColumn("DiveUrlId")
				.Fetch.Select()
				.AsSet();
			References(x => x.Diver)
				.Class(typeof(Diver))	
				.Column("DiverId")
				.Insert()
				.Update();
			HasManyToMany(x => x.Gears)
				.ChildKeyColumn("DiveDetailsId")
				.ParentKeyColumn("GearId")
				.Table("GearOnDive")
				.Fetch.Select()
				.AsSet();
			References(x => x.Role)
				.Class(typeof(Role))	
				.Column("RoleId")
				.Insert()
				.Update();
			HasMany(x => x.TanksOnDives)
				.KeyColumn("DiveDetailsId")
				.Fetch.Select()
				.AsSet();
		}
	}
}