using Amazon;
using Amazon.DynamoDBv2;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using EMG.Common;
using EMG.Wcf.Installers;

namespace XRaySample.StorageResourceAccess.Installers
{
    public class AppInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.RegisterWcfService<StorageResourceAccess, StorageResourceAccessServiceHostConfigurator>();

            container.Register(Component.For<IAmazonDynamoDB, AmazonDynamoDBClient>().UsingFactoryMethod(() => new AmazonDynamoDBClient(RegionEndpoint.EUWest1)).LifeStyle.Transient);

            container.Register(Component.For<WcfHostingOptions>().FromConfiguration(c => c.GetSection("WCF")));
        }
    }
    
}