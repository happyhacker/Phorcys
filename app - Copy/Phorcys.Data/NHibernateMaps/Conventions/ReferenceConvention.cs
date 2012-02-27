using FluentNHibernate.Conventions;

namespace Phorcys.Data.NHibernateMaps.Conventions
{
    public class ReferenceConvention : IReferenceConvention
    {
        public void Apply(FluentNHibernate.Conventions.Instances.IManyToOneInstance instance)
        {
            instance.Column(instance.Property.Name + "Fk");
        }
    }
}
