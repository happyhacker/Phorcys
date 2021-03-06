using System;
using DiveLog.Core;
using FluentNHibernate.Mapping;

namespace DiveLog.Data.NHibernateMaps
{
	public class GasMapping : ClassMap<Gas>
	{
		public GasMapping()
		{
			Table("Gases");
			Schema("dbo");
			Id(x => x.GasId);
			Map(x => x.Name);
			HasMany(x => x.GasPrices)
				.KeyColumn("GasId")
				.Fetch.Select()
				.AsSet();
		}
	}
}