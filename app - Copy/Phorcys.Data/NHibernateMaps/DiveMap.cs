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
      Id(x => x.DiveId).GeneratedBy.Identity().UnsavedValue(0);
			Map(x => x.Created, "Created");
			Map(x => x.LastModified, "LastModified");
			Map(x => x.Notes, "Notes");
			Map(x => x.ScheduledTime, "ScheduledTime");
			Map(x => x.Title, "Title");
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