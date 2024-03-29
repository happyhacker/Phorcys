using System;
using DiveLog.Core;
using FluentNHibernate.Mapping;

namespace DiveLog.Data.NHibernateMaps
{
	public class TanksOnDiveMapping : ClassMap<TanksOnDive>
	{
		public TanksOnDiveMapping()
		{
			Table("TanksOnDive");
			Schema("dbo");			CompositeId()
				.KeyProperty(x => x.GearId, "GearId")
				.KeyProperty(x => x.DiveDetailsId, "DiveDetailsId");

			Map(x => x.EndingPressure);
			Map(x => x.StartingPressure);
			References(x => x.DiveDetail)
				.Class(typeof(DiveDetail))
				.Not.Nullable()	
				.Column("DiveDetailsId")
				.Insert()
				.Update();
			HasManyToMany(x => x.GasPrices)
				.ChildKeyColumns.Add("GearId")
				.ParentKeyColumn("GasPriceId")
				.Table("GasInTanks")
				.Fetch.Select()
				.AsSet();
			References(x => x.Tank)
				.Class(typeof(Tank))
				.Not.Nullable()	
				.Column("GearId")
				.Insert()
				.Update();
		}
	}
}