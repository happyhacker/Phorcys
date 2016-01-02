using System;
using Phorcys.Core;
using FluentNHibernate.Mapping;
using Phorcys.Core;

namespace Phorcys.Data.NHibernateMaps
{
	public class DiveTypeMapping : ClassMap<DiveType>
	{
		public DiveTypeMapping()
		{
			Table("DiveTypes");
			Schema("dbo");
      Id(x => x.DiveTypeId, "DiveTypeId").GeneratedBy.Identity().UnsavedValue(0);
			Map(x => x.Created);
			Map(x => x.LastModified);
			Map(x => x.Notes);
			Map(x => x.Title);
			HasManyToMany(x => x.Dives)
				.ChildKeyColumn("DiveTypeId")
				.ParentKeyColumn("DiveId")
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