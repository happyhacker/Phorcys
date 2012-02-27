using System;
using FluentNHibernate.Automapping;
using FluentNHibernate.Conventions;
using Phorcys.Core;
using Phorcys.Data.NHibernateMaps.Conventions;
using SharpArch.Core.DomainModel;
using SharpArch.Data.NHibernate.FluentNHibernate;

namespace Phorcys.Data.NHibernateMaps
{

    public class AutoPersistenceModelGenerator : IAutoPersistenceModelGenerator
    {

        #region IAutoPersistenceModelGenerator Members

        public AutoPersistenceModel Generate()
        {
            return AutoMap.AssemblyOf<Class1>(new AutomappingConfiguration())
                .Conventions.Setup(GetConventions())
                .IgnoreBase<Entity>()
                .IgnoreBase(typeof(EntityWithTypedId<>))
                .UseOverridesFromAssemblyOf<AutoPersistenceModelGenerator>();
        }

        #endregion

        private Action<IConventionFinder> GetConventions()
        {
            return c =>
            {
                c.Add<Conventions.ForeignKeyConvention>();
                c.Add<HasManyConvention>();
                c.Add<HasManyToManyConvention>();
                c.Add<Conventions.ManyToManyTableNameConvention>();
                c.Add<PrimaryKeyConvention>();
                c.Add<ReferenceConvention>();
                c.Add<TableNameConvention>();
            };
        }
    }
}
