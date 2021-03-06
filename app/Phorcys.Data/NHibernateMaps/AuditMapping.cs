using System;
using DiveLog.Core;
using FluentNHibernate.Mapping;

namespace DiveLog.Data.NHibernateMaps
{
	public class AuditMapping : ClassMap<Audit>
	{
		public AuditMapping()
		{
			Table("Audits");
			Schema("dbo");
			Id(x => x.AuditsId);
			Map(x => x.ActionPerformed);
			Map(x => x.Created);
			Map(x => x.Notes);
			Map(x => x.RowId);
			Map(x => x.TableName);
			Map(x => x.UserName);
		}
	}
}