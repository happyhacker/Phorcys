using System;
using DiveLog.Core;
using FluentNHibernate.Mapping;

namespace DiveLog.Data.NHibernateMaps
{
	public class DiveDetailMapping : ClassMap<DiveDetail>
	{
		public DiveDetailMapping()
		{
			Table("DiveDetails");
			Schema("dbo");
			Id(x => x.DiveDetailsId);
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
			References(x => x.Dive)
				.Class(typeof(Dive))	
				.Column("DiveId")
				.Insert()
				.Update();
			HasMany(x => x.DiveDetailUrls)
				.KeyColumn("DiveDetailsId")
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