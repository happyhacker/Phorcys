using Castle.Windsor;
using Phorcys.Tests.Integration.Phorcys.Data.TestDoubles;
using SharpArch.Core.CommonValidator;
using CommonServiceLocator.WindsorAdapter;
using Microsoft.Practices.ServiceLocation;
using SharpArch.Core.PersistenceSupport;
using Castle.MicroKernel.Registration;
using System.ComponentModel.DataAnnotations;

namespace Phorcys.Tests.Integration
{
    public class ServiceLocatorInitializer
    {
        public static void Init()
        {
            IWindsorContainer container = new WindsorContainer();

            container.Register(
                     Component
                         .For(typeof(IValidator))
                         .ImplementedBy(typeof(Validator))
                         .Named("validator"));

            container.Register(
                    Component
                        .For(typeof(IEntityDuplicateChecker))
                        .ImplementedBy(typeof(EntityDuplicateCheckerStub))
                        .Named("entityDuplicateChecker"));

            ServiceLocator.SetLocatorProvider(() => new WindsorServiceLocator(container));
        }
    }
}
