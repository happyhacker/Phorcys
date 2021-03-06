using System;
using DiveLog.Core;
using FluentNHibernate.Mapping;

namespace DiveLog.Data.NHibernateMaps
{
	public class DiveSiteUrlMapping : ClassMap<DiveSiteUrl>
	{
		public DiveSiteUrlMapping()
		{
			Table("DiveSiteUrls");
			Schema("dbo");
			Id(x => x.DiveSiteUrlId);
			Map(x => x.IsImage);
			Map(x => x.Title);
			Map(x => x.Url, "URL");
			References(x => x.DiveSite)
				.Class(typeof(DiveSite))
				.Not.Nullable()	
				.Column("DiveSiteId")
				.Insert()
				.Update();
		}
	}
}