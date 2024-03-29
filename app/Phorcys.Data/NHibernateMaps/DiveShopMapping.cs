using System;
using Phorcys.Core;
using FluentNHibernate.Mapping;

namespace Phorcys.Data.NHibernateMaps
{
	public class DiveShopMapping : ClassMap<DiveShop>
	{
		public DiveShopMapping()
		{
			Table("DiveShops");
      Id(x => x.Id, "DiveShopId").GeneratedBy.Identity().UnsavedValue(0);
			Map(x => x.Notes);

      
			References(x => x.Contact)
				.Class(typeof(Contact))
				.Not.Nullable()	
				.Column("ContactId")
				.Insert()
				.Update();

      /*
			HasManyToMany(x => x.Services)
				.ChildKeyColumn("DiveShopId")
				.ParentKeyColumn("ServiceId")
				.Table("DiveShopServices")
				.Fetch.Select()
				.AsSet();
       */
       
		}
	}
}