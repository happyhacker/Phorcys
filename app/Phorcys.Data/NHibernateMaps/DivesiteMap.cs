using FluentNHibernate.Mapping;
using Phorcys.Core;

namespace Phorcys.Data.NHibernateMaps {
  public class DivesiteMap : ClassMap<DiveSite> {

    public DivesiteMap() {
      Table("DiveSites");
      Id(x => x.Id, "DiveSiteId").GeneratedBy.Identity().UnsavedValue(0);
      //References(x => x.Dives).Column("Dives");
      Map(x => x.GeoCode, "GeoCode").Length(20);
      Map(x => x.IsFreshWater, "IsFreshWater").Not.Nullable();
      Map(x => x.Notes, "Notes");
      Map(x => x.Title, "Title").Length(40).Not.Nullable();
      Map(x => x.MaxDepth, "MaxDepth");
      //Map(x => x.UserId, "UserId").Not.Nullable();
      //Map(x => x.DiveLocationId, "DiveLocationId").Nullable();
      Map(x => x.Created).ReadOnly();
      Map(x => x.LastModified).ReadOnly();
      References(x => x.DiveLocation).Class(typeof(DiveLocation)).Column("DiveLocationId").Insert().Update().Nullable();
      References(x => x.User)
        .Class(typeof (User))
        .Not.Nullable()
        .Column("UserId")
        .Insert()
        .Update();
    }
  }
}
