using System;
using System.ServiceModel;
using System.Threading.Tasks;

namespace EMG.AbsenceManager
{
    [ServiceContract]
    public interface IAbsenceManager
    {
        [OperationContract]
        Task MarkAsSick(string employee, DateTime date);

        [OperationContract]
        Task MarkAsCareOfChild(string employee, DateTime date);
    }
}