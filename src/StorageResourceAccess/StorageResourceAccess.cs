using System;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Kralizek.XRayRecorder;
using Nybus.Logging;

namespace XRaySample.StorageResourceAccess
{
    [AWSXRayBehavior("XRaySample")]
    [ServiceBehavior(Name = "StorageResourceAccess")]
    public class StorageResourceAccess : IStorageResourceAccess
    {
        private readonly ILogger _logger;
        private readonly DynamoDBContext _context;

        public StorageResourceAccess(IAmazonDynamoDB dynamoDb, ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            _context = new DynamoDBContext(dynamoDb ?? throw new ArgumentNullException(nameof(dynamoDb)));
        }

        public async Task RegisterAbsenceAsync(string employee, DateTime date, AbsenceReason reason)
        {
            var newAbsence = new Absence
            {
                Employee = employee,
                Date = date.Date,
                InsertedOn = DateTime.Now,
                Reason = reason
            };

            try
            {
                _logger.LogInformation(newAbsence, o => $"Persisting new absence: {o.Employee} reported {o.Reason:G} on {o.Date:yyyy-MM-dd} at {o.InsertedOn:s}");
                await _context.SaveAsync(newAbsence);
            }
            catch (Exception ex)
            {
                _logger.LogError(newAbsence, ex, (obj, e) => $"{e.Message} occurred when persisting {obj.Employee}'s absence");
                throw;
            }
        }

        public async Task<Absence[]> GetUserAbsenceListAsync(string employee)
        {
            if (employee == null)
            {
                throw new ArgumentNullException(nameof(employee));
            }

            try
            {
                var items = _context.Query<Absence>(employee);

                return items.ToArray();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}