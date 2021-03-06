using System;
using DiveLog.Core;
using FluentNHibernate.Mapping;

namespace DiveLog.Data.NHibernateMaps
{
	public class QualificationMapping : ClassMap<Qualification>
	{
		public QualificationMapping()
		{
			Table("Qualifications");
			Schema("dbo");
			Id(x => x.QualificationId);
			Map(x => x.Created);
			Map(x => x.LastModified);
			Map(x => x.Notes);
			Map(x => x.Title);
			HasMany(x => x.DiverQualifications)
				.KeyColumn("QualificationId")
				.Fetch.Select()
				.AsSet();
			References(x => x.User)
				.Class(typeof(User))
				.Not.Nullable()	
				.Column("UserId")
				.Insert()
				.Update();
		}
	}
}