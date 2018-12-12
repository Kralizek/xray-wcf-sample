using System;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;

namespace XRaySample.StorageResourceAccess
{
    [ServiceContract]
    public interface IStorageResourceAccess
    {

        [OperationContract]
        Task RegisterAbsenceAsync(string employee, DateTime date, AbsenceReason reason);

        [OperationContract]
        Task<Absence[]> GetUserAbsenceListAsync(string employee);
    }

    [DataContract]
    [DynamoDBTable("absences", lowerCamelCaseProperties: true)]
    public class Absence
    {
        [DataMember]
        [DynamoDBHashKey("employee")]
        public string Employee { get; set; }

        [DataMember]
        [DynamoDBRangeKey("date", typeof(DateConverter))]
        public DateTime Date { get; set; }

        [DataMember]
        [DynamoDBProperty("inserted-on")]
        public DateTime InsertedOn { get; set; }

        [DataMember]
        [DynamoDBProperty("reason")]
        public AbsenceReason Reason { get; set; }
    }


    [DataContract]
    public enum AbsenceReason
    {
        [EnumMember] Sick = 1,
        [EnumMember] CareOfChild = 2
    }

    public class DateConverter : Amazon.DynamoDBv2.DataModel.IPropertyConverter
    {
        public DynamoDBEntry ToEntry(object value)
        {
            if (value is DateTime dt)
            {
                return new Primitive(dt.Date.ToString("yyyy-MM-dd"));
            }

            return new DynamoDBNull();
        }

        public object FromEntry(DynamoDBEntry entry)
        {
            var value = entry.AsString();

            if (DateTime.TryParse(value, out var date))
            {
                return date;
            }

            return default(DateTime);
        }
    }
}