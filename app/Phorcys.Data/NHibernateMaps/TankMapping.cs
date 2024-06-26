using System;
using Phorcys.Core;
using FluentNHibernate.Mapping;

namespace Phorcys.Data.NHibernateMaps
{

    public class TankMapping : ClassMap<Tank>
    {
        public TankMapping()
        {
            Table("Tanks");
            Schema("dbo");
            Id(x => x.Id, "GearId").GeneratedBy.Assigned();
            //Map(x => x.GearId);
            Map(x => x.Volume);
            Map(x => x.WorkingPressure);
            Map(x => x.ManufacturedMonth);
            Map(x => x.ManufacturedYear);
        }
    }
} 