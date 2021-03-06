using System;
using DiveLog.Core;
using FluentNHibernate.Mapping;

namespace DiveLog.Data.NHibernateMaps
{
	public class RoleMapping : ClassMap<Role>
	{
		public RoleMapping()
		{
			Table("Roles");
			Schema("dbo");
			Id(x => x.RoleId);
			Map(x => x.Notes);
			Map(x => x.Title);
			HasMany(x => x.DiveDetails)
				.KeyColumn("RoleId")
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