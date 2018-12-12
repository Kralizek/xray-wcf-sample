using System;
using EMG.Wcf;
using EMG.Wcf.Extensions;

namespace XRaySample.StorageResourceAccess
{
    /*
    This class is used to configure the service.
    Here you can register the endpoints you want your service to expose.
    Some notes: 
    * BasicHttp and WSHttp can share the same port, granted they have different suffix
    * NetTcp doesn't need a suffix but it requires a specific port
    * NamedPipe doesn't use any port but it requires a suffix
    * Each endpoint can be configured indipendently.
    * Each binding can be used more than once, granted they have unique address
    * If the service implements more than one Service Contract, each contract should be added indipendently.
    * Each binding can be customized through a Action<TBinding>
    * Default binding setting: all timeouts are set to 70 seconds, file size and object graph restrictions are set to Int32.MaxValue, SecurityMode: None.
    */
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

            host.AddMetadata(10002); // the port number must differ
        }
    }

    public class WcfHostingOptions
    {
        public string AnnouncedHostName { get; set; }
    }
}