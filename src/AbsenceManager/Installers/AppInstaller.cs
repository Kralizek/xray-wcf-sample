using System.ServiceModel;
using Castle.Facilities.WcfIntegration;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using EMG.Common;
using EMG.Wcf.Installers;
using XRaySample.NotificationEngine;
using XRaySample.StorageResourceAccess;

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

            container.Register(Component.For<IStorageResourceAccess>().ImplementedBy<StorageResourceAccessClient>().AsWcfClient(new DefaultClientModel
            {
                Endpoint = WcfEndpoint.BoundTo(new NetTcpBinding(SecurityMode.None)).At("net.tcp://localtest.me:10001/")
            }).LifestyleTransient());

            container.Register(Component.For<INotificationEngine>().ImplementedBy<NotificationEngineClient>().AsWcfClient(new DefaultClientModel
            {
                Endpoint = WcfEndpoint.BoundTo(new NetTcpBinding(SecurityMode.None)).At("net.tcp://localtest.me:10003/")
            }).LifestyleTransient());
        }
    }
    
}