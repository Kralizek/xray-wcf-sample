using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using EMG.Common;
using EMG.Wcf.Installers;

namespace EMG.StorageResourceAccess.Installers
{
    public class AppInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.RegisterWcfService<StorageResourceAccess, StorageResourceAccessServiceHostConfigurator>();

            /* EQUIVALENT TO
            container.Register(Component.For<StorageResourceAccess>().LifestyleTransient());

            container.Register(Component.For<IServiceHostConfigurator<StorageResourceAccess>, StorageResourceAccessWithDiscoveryServiceHostConfigurator>());
            */

            container.Register(Component.For<WcfHostingOptions>().FromConfiguration(c => c.GetSection("WCF")));
        }
    }
    
}