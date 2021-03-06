using System;
using Phorcys.Core;
using FluentNHibernate.Mapping;

namespace Phorcys.Data.NHibernateMaps {
	public class DiveLocationMap : ClassMap<DiveLocation> {
		
    public DiveLocationMap() {
	  Table("DiveLocations");
      Id(x => x.Id, "DiveLocationId").GeneratedBy.Identity().UnsavedValue(0);
		  //Map(x => x.DiveLocationId);
			Map(x => x.Created);
			Map(x => x.LastModified);
			Map(x => x.Notes);
			Map(x => x.Title);
		  //Map(x => x.UserId);
		  /*References(x => x.Contact)
				.Class(typeof(Contact))	
				.Column("ContactId")
				.Insert()
				.Update();
			HasMany(x => x.DiveSites)
				.KeyColumn("DiveLocationId")
				.Fetch.Select()
				.AsSet();*/
			References(x => x.User)
				.Class(typeof(User))
				.Not.Nullable()	
				.Column("UserId")
				.Insert()
				.Update();
		}
	}
}