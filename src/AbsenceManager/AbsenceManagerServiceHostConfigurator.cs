using System;
using EMG.Wcf;
using EMG.Wcf.Extensions;

namespace EMG.AbsenceManager
{
    public class AbsenceManagerServiceHostConfigurator : IServiceHostConfigurator<AbsenceManager>
    {
        private readonly WcfHostingOptions _options;

        public AbsenceManagerServiceHostConfigurator(WcfHostingOptions options)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
        }

        public void Configure(IServiceHost<AbsenceManager> host)
        {
            host.AddNamedPipeEndpoint(typeof(IAbsenceManager), "absence:absence-manager", configureBinding: binding => binding.UseDefaults());

            host.AddNetTcpEndpoint(typeof(IAbsenceManager), 10002, hostname: _options.AnnouncedHostName, configureBinding: binding => binding.UseDefaults());

            host.AddMetadata(10102);
        }
    }

    public class WcfHostingOptions
    {
        public string AnnouncedHostName { get; set; }
    }
}