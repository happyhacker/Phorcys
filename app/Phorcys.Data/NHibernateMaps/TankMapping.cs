using System;
using Phorcys.Core;
using FluentNHibernate.Mapping;

namespace Phorcys.Data.NHibernateMaps
{
  public sealed class TankMapping : SubclassMap<Tank>
  {

    public TankMapping()
    {
      Extends<Gear>();
      Table("Tanks");
      KeyColumn("GearId");
      Map(x => x.Volume);
      Map(x => x.WorkingPressure);
      Map(x => x.ManufacturedMonth);
      Map(x => x.ManufacturedYear);
    }
  }
}

/*public class TankMapping : ClassMap<Tank>
	{
		public TankMapping()
		{
			Table("Tanks");
			Schema("dbo");
      Id(x => x.GearId, "GearId");
			Map(x => x.Volume);
			Map(x => x.WorkingPressure);
			References(x => x.Gear)
				.Unique()	
				.Column("GearId")
				.Insert()
				.Update();*/
			/*HasMany(x => x.TanksOnDives)
				.KeyColumn("GearId")
				.Fetch.Select()
				.AsSet();
		}
	}
}*/