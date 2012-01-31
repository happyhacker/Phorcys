using System;
using DiveLog.Core;
using FluentNHibernate.Mapping;

namespace DiveLog.Data.NHibernateMaps
{
	public class CertificationMapping : ClassMap<Certification>
	{
		public CertificationMapping()
		{
			Table("Certifications");
			Schema("dbo");
			Id(x => x.CertificationId);
			Map(x => x.Created);
			Map(x => x.LastModified);
			Map(x => x.Notes);
			Map(x => x.Title);
			References(x => x.DiveAgency)
				.Class(typeof(DiveAgency))	
				.Column("DiveAgencyId")
				.Insert()
				.Update();
			HasMany(x => x.DiverCertifications)
				.KeyColumn("CertificationId")
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