using System;
using DiveLog.Core;
using FluentNHibernate.Mapping;

namespace DiveLog.Data.NHibernateMaps
{
	public class DiverCertificationMapping : ClassMap<DiverCertification>
	{
		public DiverCertificationMapping()
		{
			Table("DiverCertifications");
			Schema("dbo");			CompositeId()
				.KeyProperty(x => x.DiverId, "DiverId")
				.KeyProperty(x => x.CertificationId, "CertificationId");

			Map(x => x.CertificationNum);
			Map(x => x.Certified);
			Map(x => x.Created);
			Map(x => x.LastModified);
			Map(x => x.Notes);
			References(x => x.Certification)
				.Class(typeof(Certification))
				.Not.Nullable()	
				.Column("CertificationId")
				.Insert()
				.Update();
			References(x => x.Contact)
				.Class(typeof(Contact))	
				.Column("Instructor")
				.Insert()
				.Update();
			References(x => x.Diver)
				.Class(typeof(Diver))
				.Not.Nullable()	
				.Column("DiverId")
				.Insert()
				.Update();
		}
	}
}