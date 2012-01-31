using FluentNHibernate.Mapping;
using Phorcys.Core.Domains;

namespace Phorcys.Data.NHibernateMaps
{
    /// <summary>
    /// The NHibernate mapping for an Enumeration.
    /// </summary>
    public class EnumerationMap : ClassMap<Enumeration>
    {
        public EnumerationMap()
        {
            Table("Enumerations");
            Id(x => x.Id, "EnumerationId").GeneratedBy.Identity().UnsavedValue(0);
            Map(x => x.Name, "Name").Not.Nullable();
            Map(x => x.DataType, "DataType").Not.Nullable();
            Map(x => x.Active, "ActiveFlag").Not.Nullable();
        }
    }
}