using System;
using System.ServiceModel;
using System.Threading.Tasks;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using Kralizek.XRayRecorder;
using Nybus.Logging;

namespace EMG.NotificationEngine
{

    /*
        The concrete implementation of your service.
        It can implement more than one service contract.
    */
    [ServiceBehavior(Name = "NotificationEngine")]
    [AWSXRayBehavior(prefix: "XRaySample")]
    public class NotificationEngine : INotificationEngine
    {
        private readonly IAmazonSimpleNotificationService _sns;
        private readonly ILogger _logger;

        private string topicArn = Environment.GetEnvironmentVariable("XRaySampleTopicArn");

        public NotificationEngine(IAmazonSimpleNotificationService sns, ILogger logger)
        {
            _sns = sns ?? throw new ArgumentNullException(nameof(sns));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task NotifyAbsence(string employee, AbsenceReason reason)
        {
            try
            {
                _logger.LogInformation(new { employee, reason }, s => $"Sending a notification: employee = {s.employee}, reason = {s.reason}");

                await _sns.PublishAsync(new PublishRequest
                {
                    TopicArn = topicArn,
                    Message = $"{employee} reported an absence due to {reason:G}"
                });

            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}