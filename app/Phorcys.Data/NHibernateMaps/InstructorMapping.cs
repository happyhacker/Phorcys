using System;
using Phorcys.Core;
using FluentNHibernate.Mapping;

namespace DiveLog.Data.NHibernateMaps {
  public class InstructorMapping : ClassMap<Instructor> {
    public InstructorMapping() {
      Table("Instructors");
      Id(x => x.Id, "InstructorId").GeneratedBy.Identity().UnsavedValue(0);
      Map(x => x.Notes);

      References(x => x.Contact)
        .Class(typeof(Contact))
        .Not.Nullable()
        .Column("ContactId")
        .Insert()
        .Update();
    }
  }
}