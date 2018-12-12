using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using EMG.Common;
using EMG.Wcf.Installers;

namespace EMG.AbsenceManager.Installers
{
    public class AppInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.RegisterWcfService<AbsenceManager, AbsenceManagerServiceHostConfigurator>();

            /* EQUIVALENT TO
            container.Register(Component.For<AbsenceManager>().LifestyleTransient());

            container.Register(Component.For<IServiceHostConfigurator<AbsenceManager>, AbsenceManagerWithDiscoveryServiceHostConfigurator>());
            */

            container.Register(Component.For<WcfHostingOptions>().FromConfiguration(c => c.GetSection("WCF")));
        }
    }
    
}