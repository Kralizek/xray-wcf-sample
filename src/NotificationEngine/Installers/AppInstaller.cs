using Amazon;
using Amazon.SimpleNotificationService;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using EMG.Common;
using EMG.Wcf.Installers;

namespace EMG.NotificationEngine.Installers
{
    public class AppInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.RegisterWcfService<NotificationEngine, NotificationEngineServiceHostConfigurator>();

            container.Register(Component.For<WcfHostingOptions>().FromConfiguration(c => c.GetSection("WCF")));

            container.Register(Component.For<IAmazonSimpleNotificationService>().UsingFactoryMethod(() => new AmazonSimpleNotificationServiceClient(RegionEndpoint.EUWest1)));
        }
    }
    
}