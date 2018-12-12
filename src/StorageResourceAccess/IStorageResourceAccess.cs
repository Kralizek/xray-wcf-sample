using System.ServiceModel;
using System.Threading.Tasks;

namespace EMG.StorageResourceAccess
{
    /* 
        The contract to expose.
        Can have both synchronous and asynchronous methods. WCF will create support for both cases.
        Due to C# compiler limitations, your service will have to perfectly implement the interface.
        Asynchronous methods in the interface is suggested.
    */
    [ServiceContract]
    public interface IStorageResourceAccess
    {

        [OperationContract]
        string Echo(string message);

        [OperationContract]
        Task<string> UpperCaseAsync(string message);

    }
}