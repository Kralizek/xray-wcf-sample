using System;
using System.Threading.Tasks;
using Nybus.Logging;

namespace EMG.NotificationEngine
{

    /*
        The concrete implementation of your service.
        It can implement more than one service contract.
    */
    public class NotificationEngine : INotificationEngine
    {
        private readonly ILogger _logger;

        public NotificationEngine(ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public string Echo(string message)
        {
            return message;
        }

        public Task<string> UpperCaseAsync(string message)
        {
            return Task.FromResult(message?.ToUpper());
        }
    }
}