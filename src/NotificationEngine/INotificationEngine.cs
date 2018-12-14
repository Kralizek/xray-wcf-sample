using System.Runtime.Serialization;
using System.ServiceModel;
using System.Threading.Tasks;

namespace EMG.NotificationEngine
{
    [ServiceContract]
    public interface INotificationEngine
    {
        [OperationContract]
        Task NotifyAbsence(string employee, AbsenceReason reason);
    }

    [DataContract]
    public enum AbsenceReason
    {
        [EnumMember] Sick = 1,
        [EnumMember] CareOfChild = 2
    }
}