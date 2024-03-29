using System;
using Phorcys.Core;
using FluentNHibernate.Mapping;

namespace DiveLog.Data.NHibernateMaps {
  public class DiverMapping : ClassMap<Diver> {
    public DiverMapping() {
      Table("Divers");
      Id(x => x.Id, "DiverId").GeneratedBy.Identity().UnsavedValue(0);
      Map(x => x.Notes);
      Map(x => x.RestingSacRate);
      Map(x => x.WorkingSacRate);

      References(x => x.Contact)
        .Class(typeof(Contact))
        .Not.Nullable()
        .Column("ContactId")
        .Insert()
        .Update();

       HasMany(x => x.DiverCertifications)
         .KeyColumn("DiverId")
         .Fetch.Select()
         .AsSet();

      /*			HasMany(x => x.DiveDetails)
         .KeyColumn("DiverId")
         .Fetch.Select()
         .AsSet();
       HasMany(x => x.DiverQualifications)
         .KeyColumn("DiverId")
         .Fetch.Select()
         .AsSet();
       HasManyToMany(x => x.Dives)
         .ChildKeyColumn("DiverId")
         .ParentKeyColumn("DiveId")
         .Table("DiveTeams")
         .Fetch.Select()
         .AsSet();
       HasManyToMany(x => x.Gears)
         .ChildKeyColumn("DiverId")
         .ParentKeyColumn("GearId")
         .Table("DiverGear")
         .Fetch.Select()
         .AsSet();
        */
    }
  }
}