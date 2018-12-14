using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Threading.Tasks;
using Kralizek.XRayRecorder;
using Nybus.Logging;
using XRaySample.NotificationEngine;
using XRaySample.StorageResourceAccess;
using AbsenceReason = XRaySample.StorageResourceAccess.AbsenceReason;

namespace EMG.AbsenceManager
{
    [AWSXRayBehavior("XRaySample")]
    [ServiceBehavior]
    public class AbsenceManager : IAbsenceManager
    {
        private readonly INotificationEngine _notification;
        private readonly IStorageResourceAccess _storage;
        private readonly ILogger _logger;

        public AbsenceManager(INotificationEngine notification, IStorageResourceAccess storage, ILogger logger)
        {
            _notification = notification ?? throw new ArgumentNullException(nameof(notification));
            _storage = storage ?? throw new ArgumentNullException(nameof(storage));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task MarkAsSick(string employee, DateTime date)
        {
            try
            {
                _logger.LogInformation(new { employee, date }, s => $"{s.employee} was sick on {s.date:yyyy-MM-dd}");
                await _storage.RegisterAbsenceAsync(employee, date, AbsenceReason.Sick);
                await _notification.NotifyAbsenceAsync(employee, XRaySample.NotificationEngine.AbsenceReason.Sick);

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task MarkAsCareOfChild(string employee, DateTime date)
        {
            try
            {
                _logger.LogInformation(new { employee, date }, s => $"{s.employee} was taking care of their kid on {s.date:yyyy-MM-dd}");
                await _storage.RegisterAbsenceAsync(employee, date, AbsenceReason.CareOfChild);
                await _notification.NotifyAbsenceAsync(employee, XRaySample.NotificationEngine.AbsenceReason.CareOfChild);

            }
            catch (Exception ex)
            {

                throw;
            }
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

namespace XRaySample.NotificationEngine
{
    public partial class NotificationEngineClient
    {
        static partial void ConfigureEndpoint(ServiceEndpoint serviceEndpoint, ClientCredentials clientCredentials)
        {
            serviceEndpoint.EndpointBehaviors.Add(new AWSXRayBehavior(prefix: "XRaySample"));
        }
    }
}