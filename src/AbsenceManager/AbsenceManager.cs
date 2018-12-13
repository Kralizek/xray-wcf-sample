using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Threading.Tasks;
using Kralizek.XRayRecorder;
using Nybus.Logging;
using XRaySample.StorageResourceAccess;

namespace EMG.AbsenceManager
{
    [AWSXRayBehavior("XRaySample")]
    [ServiceBehavior]
    public class AbsenceManager : IAbsenceManager
    {
        private readonly IStorageResourceAccess _storage;
        private readonly ILogger _logger;

        public AbsenceManager(IStorageResourceAccess storage, ILogger logger)
        {
            _storage = storage ?? throw new ArgumentNullException(nameof(storage));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task MarkAsSick(string employee, DateTime date)
        {
            _logger.LogInformation(new {employee, date}, s => $"{s.employee} was sick on {s.date:yyyy-MM-dd}");
            await _storage.RegisterAbsenceAsync(employee, date, AbsenceReason.Sick);
        }

        public async Task MarkAsCareOfChild(string employee, DateTime date)
        {
            _logger.LogInformation(new { employee, date }, s => $"{s.employee} was taking care of their kid on {s.date:yyyy-MM-dd}");
            await _storage.RegisterAbsenceAsync(employee, date, AbsenceReason.CareOfChild);
        }
    }
}

namespace XRaySample.StorageResourceAccess
{
    public partial class StorageResourceAccessClient
    {
        static partial void ConfigureEndpoint(ServiceEndpoint serviceEndpoint, ClientCredentials clientCredentials)
        {
            serviceEndpoint.EndpointBehaviors.Add(new AWSXRayBehavior(prefix: "XRaySample"));
        }
    }
}