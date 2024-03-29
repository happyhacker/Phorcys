using System;
using DiveLog.Core;
using FluentNHibernate.Mapping;

namespace DiveLog.Data.NHibernateMaps
{
	public class GasPriceMapping : ClassMap<GasPrice>
	{
		public GasPriceMapping()
		{
			Table("GasPrices");
			Schema("dbo");
			Id(x => x.GasPriceId);
			Map(x => x.FillDate);
			Map(x => x.UnitPrice);
			Map(x => x.VolumeAdded);
			References(x => x.Gase)
				.Class(typeof(Gas))	
				.Column("GasId")
				.Insert()
				.Update();
			HasManyToMany(x => x.TanksOnDives)
				.ChildKeyColumn("GasPriceId")
				.ParentKeyColumns.Add("GearId")
				.Table("GasInTanks")
				.Fetch.Select()
				.AsSet();
		}
	}
}