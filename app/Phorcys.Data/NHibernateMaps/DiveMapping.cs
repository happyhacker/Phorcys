using System;
using DiveLog.Core;
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
			Map(x => x.Created);
			Map(x => x.LastModified);
			Map(x => x.Notes);
			Map(x => x.ScheduledTime);
			Map(x => x.Title);
			HasMany(x => x.DiveDetails)
				.KeyColumn("DiveId")
				.Fetch.Select()
				.AsSet();
			HasManyToMany(x => x.Divers)
				.ChildKeyColumn("DiveId")
				.ParentKeyColumn("DiverId")
				.Table("DiveTeams")
				.Fetch.Select()
				.AsSet();
			References(x => x.DiveSite)
				.Class(typeof(DiveSite))	
				.Column("DiveSiteId")
				.Insert()
				.Update();
			HasManyToMany(x => x.DiveTypes)
				.ChildKeyColumn("DiveId")
				.ParentKeyColumn("DiveTypeId")
				.Table("DiveTypesXref")
				.Fetch.Select()
				.AsSet();
			References(x => x.User)
				.Class(typeof(User))
				.Not.Nullable()	
				.Column("UserId")
				.Insert()
				.Update();
		}
	}
}