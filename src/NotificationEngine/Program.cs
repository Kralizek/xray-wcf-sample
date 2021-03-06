﻿using System;
using Amazon.XRay.Recorder.Handlers.AwsSdk;
using Castle.Facilities.WcfIntegration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using Castle.Windsor.Installer;
using EMG.Common;
using EMG.Wcf;
using EMG.Wcf.Installers;
using EMG.NotificationEngine.Installers;
using Microsoft.Extensions.Configuration;
using Nybus.Logging;
using Topshelf;
using Topshelf.CastleWindsor;

namespace EMG.NotificationEngine
{
    class Program
    {
        public static readonly string ServiceName = "XRaySample.NotificationEngine";

        static void Main(string[] args)
        {
            AWSSDKHandler.RegisterXRayForAllServices();

            using (var container = CreateContainer())
            {
                var loggerFactory = container.Resolve<ILoggerFactory>();
                var logger = loggerFactory.CreateCurrentClassLogger();

                var service = HostFactory.New(cfg =>
                {
                    cfg.UseWindsorContainer(container);

                    cfg.Service<WcfServiceHost<NotificationEngine>>(svc =>
                    {
                        svc.BeforeStartingService(sc => sc.RequestAdditionalTime(TimeSpan.FromMinutes(1)));
                        svc.BeforeStoppingService(sc => sc.RequestAdditionalTime(TimeSpan.FromMinutes(1)));

                        svc.ConstructUsingWindsorContainer();

                        svc.WhenStarted(host =>
                        {
                            logger.LogInformation($"Starting {ServiceName}");
                            host.Start();
                            logger.LogInformation($"{ServiceName} started");
                        });
                        svc.WhenStopped(host =>
                        {
                            logger.LogInformation($"Stopping {ServiceName}");
                            host.Stop();
                            logger.LogInformation($"{ServiceName} stopped");
                        });
                    });

                    cfg.SetDisplayName("XRaySample NotificationEngine");
                    cfg.SetServiceName(ServiceName);

                    cfg.EnableServiceRecovery(rc => rc.RestartService(1).RestartService(5).RestartService(10).SetResetPeriod(1));

                    cfg.RunAsLocalSystem();
                    cfg.StartAutomaticallyDelayed();
                    cfg.SetStopTimeout(TimeSpan.FromMinutes(5));
                });

                try
                {
                    service.Run();
                }
                catch (Exception ex)
                {
                    logger.LogError(ex);
                }
                
                container.Release(loggerFactory);
            }
        }

        private static IWindsorContainer CreateContainer()
        {
            var container = new WindsorContainer();

            container.AddConfiguration(
                b => b.AddJsonFile("appsettings.json", true),
                b => b.AddEnvironmentVariables()
            );

            // Check https://app.assembla.com/spaces/studentum/git-8/source/master/Configuration/CastleWindsor/README.md for usages

            container.Kernel.Resolver.AddSubResolver(new CollectionResolver(container.Kernel));

            container.Install(FromAssembly.InThisApplication());

            container.AddFacility<WcfFacility>();

            container.Install(new WcfInstaller<NotificationEngine>());

            return container;
        }
    }
}