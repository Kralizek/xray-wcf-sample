using System;
using EMG.Wcf;
using EMG.Wcf.Extensions;

namespace XRaySample.StorageResourceAccess
{
    public class StorageResourceAccessServiceHostConfigurator : IServiceHostConfigurator<StorageResourceAccess>
    {
        private readonly WcfHostingOptions _options;

        public StorageResourceAccessServiceHostConfigurator(WcfHostingOptions options)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
        }

        public void Configure(IServiceHost<StorageResourceAccess> host)
        {
            host.AddNamedPipeEndpoint(typeof(IStorageResourceAccess), "absence:storage", configureBinding: binding => binding.UseDefaults());

            host.AddNetTcpEndpoint(typeof(IStorageResourceAccess), 10001, hostname: _options.AnnouncedHostName, configureBinding: binding => binding.UseDefaults());

            host.AddMetadata(10101);
        }
    }

    public class WcfHostingOptions
    {
        public string AnnouncedHostName { get; set; }
    }
}