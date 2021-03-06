using System;
using Phorcys.Core;
using FluentNHibernate.Mapping;

namespace Phorcys.Data.NHibernateMaps
{
	public class DivePlanMapping : ClassMap<DivePlan>
	{
		public DivePlanMapping()
		{
			Table("DivePlans");
      Id(x => x.Id,"DivePlanId").GeneratedBy.Identity().UnsavedValue(0);
			Map(x => x.Created, "Created");
			Map(x => x.LastModified, "LastModified");
			Map(x => x.Notes, "Notes");
		  Map(x => x.MaxDepth, "MaxDepth");
			Map(x => x.ScheduledTime, "ScheduledTime");
			Map(x => x.Title, "Title");

      HasMany(x => x.Dives)
        .KeyColumn("DiveId")
        .Fetch.Select()
        .AsSet();
      HasManyToMany(x => x.Divers)
        .ChildKeyColumn("DivePlanId")
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
        .ChildKeyColumn("DivePlanId")
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