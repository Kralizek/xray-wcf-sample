using System;
using EMG.Wcf;
using EMG.Wcf.Extensions;

namespace EMG.NotificationEngine
{
    public class NotificationEngineServiceHostConfigurator : IServiceHostConfigurator<NotificationEngine>
    {
        private readonly WcfHostingOptions _options;

        public NotificationEngineServiceHostConfigurator(WcfHostingOptions options)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
        }

        public void Configure(IServiceHost<NotificationEngine> host)
        {
            host.AddNamedPipeEndpoint(typeof(INotificationEngine), "absence:notification", configureBinding: binding => binding.UseDefaults());

            host.AddNetTcpEndpoint(typeof(INotificationEngine), 10003, hostname: _options.AnnouncedHostName, configureBinding: binding => binding.UseDefaults());
            
            host.AddMetadata(10103);
        }
    }

    public class WcfHostingOptions
    {
        public string AnnouncedHostName { get; set; }
    }
}