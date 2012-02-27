using System.ComponentModel.DataAnnotations;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Gcc.Architecture.Data.Interfaces;
using Gcc.Architecture.Data.Persistence;
using NHibernate.Validator.Engine;
using Phorcys.Data.Repositories;
using Phorcys.Data.RepositoryInterfaces;
using SharpArch.Core.PersistenceSupport;
using SharpArch.Core.PersistenceSupport.NHibernate;
using SharpArch.Data.NHibernate;
using SharpArch.Web.Castle;

namespace Phorcys.UI.Web.CastleWindsor
{
    public class ComponentRegistrar
    {
        #region Public Methods

        public static void AddComponentsTo(IWindsorContainer container)
        {
            AddGenericRepositoriesTo(container);
            AddCustomRepositoriesTo(container);
            AddNonCustomeRepositoriesTo(container);
            AddApplicationServicesTo(container);
            AddApplicationServicesTo2(container);
            
            container.Register(
                    Component.For(typeof(IValidator))
                        .ImplementedBy(typeof(Validator))
                        .Named("validator"));
        }

        #endregion

        #region Methods

        private static void AddApplicationServicesTo2(IWindsorContainer container)
        {
            container.Register(
                AllTypes
                    .FromAssemblyNamed("Phorcys.Data")
                    .Pick()
                    .WithService.FirstNonGenericCoreInterface("Phorcys.Data.RepositoryInterfaces"));
        }

        private static void AddApplicationServicesTo(IWindsorContainer container)
        {
            container.Register(
                AllTypes
                    .FromAssemblyNamed("Phorcys.Services")
                    .Pick()
                    .WithService.FirstNonGenericCoreInterface("Phorcys.Services.Contracts"));
        }

        private static void AddCustomRepositoriesTo(IWindsorContainer container)
        {
            container.Register(
                AllTypes
                    .FromAssemblyNamed("Phorcys.Services")
                    .Pick()
                    .WithService.FirstInterface());
        }

        private static void AddNonCustomeRepositoriesTo(IWindsorContainer container)
        {
            container.Register(
                AllTypes
                    .FromAssemblyNamed("Phorcys.Services.Contracts")
                    .Pick()
                    .WithService.FirstInterface());
        }

        private static void AddGenericRepositoriesTo(IWindsorContainer container)
        {
            container.Register(
                Component.For(typeof(IEntityDuplicateChecker))
                    .ImplementedBy(typeof(EntityDuplicateChecker))
                    .Named("entityDuplicateChecker"));

            container.Register(
                Component.For(typeof(IRepository<>))
                    .ImplementedBy(typeof(Repository<>))
                    .Named("repositoryType"));

            container.Register(
                Component.For(typeof(INHibernateRepository<>))
                    .ImplementedBy(typeof(NHibernateRepository<>))
                    .Named("nhibernateRepositoryType"));

            container.Register(
                Component.For(typeof(IRepositoryWithTypedId<,>))
                    .ImplementedBy(typeof(RepositoryWithTypedId<,>))
                    .Named("repositoryWithTypedId"));

            container.Register(
                Component.For(typeof(INHibernateRepositoryWithTypedId<,>))
                    .ImplementedBy(typeof(NHibernateRepositoryWithTypedId<,>))
                    .Named("nhibernateRepositoryWithTypedId"));

            container.Register(
                    Component.For(typeof(ISessionFactoryKeyProvider))
                        .ImplementedBy(typeof(DefaultSessionFactoryKeyProvider))
                        .Named("sessionFactoryKeyProvider"));

            container.Register(
                    Component.For(typeof(IGccRepository<>))
                        .ImplementedBy(typeof(GccRepository<>))
                        .Named("gccRepositoryType"));

            container.Register(
                    Component.For(typeof(IDtoRepository<,>))
                        .ImplementedBy(typeof(DtoRepository<,>))
                        .Named("dtoRepository"));
        }

        #endregion
    }
}