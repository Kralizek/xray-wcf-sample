﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     //
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace XRaySample.StorageResourceAccess
{
    using System.Runtime.Serialization;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.Runtime.Serialization.DataContractAttribute(Name="AbsenceReason", Namespace="http://schemas.datacontract.org/2004/07/XRaySample.StorageResourceAccess")]
    public enum AbsenceReason : int
    {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Sick = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        CareOfChild = 2,
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Absence", Namespace="http://schemas.datacontract.org/2004/07/XRaySample.StorageResourceAccess")]
    public partial class Absence : object
    {
        
        private System.DateTime DateField;
        
        private string EmployeeField;
        
        private System.DateTime InsertedOnField;
        
        private XRaySample.StorageResourceAccess.AbsenceReason ReasonField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime Date
        {
            get
            {
                return this.DateField;
            }
            set
            {
                this.DateField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Employee
        {
            get
            {
                return this.EmployeeField;
            }
            set
            {
                this.EmployeeField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime InsertedOn
        {
            get
            {
                return this.InsertedOnField;
            }
            set
            {
                this.InsertedOnField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public XRaySample.StorageResourceAccess.AbsenceReason Reason
        {
            get
            {
                return this.ReasonField;
            }
            set
            {
                this.ReasonField = value;
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="XRaySample.StorageResourceAccess.IStorageResourceAccess")]
    public interface IStorageResourceAccess
    {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStorageResourceAccess/RegisterAbsence", ReplyAction="http://tempuri.org/IStorageResourceAccess/RegisterAbsenceResponse")]
        System.Threading.Tasks.Task RegisterAbsenceAsync(string employee, System.DateTime date, XRaySample.StorageResourceAccess.AbsenceReason reason);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStorageResourceAccess/GetUserAbsenceList", ReplyAction="http://tempuri.org/IStorageResourceAccess/GetUserAbsenceListResponse")]
        System.Threading.Tasks.Task<XRaySample.StorageResourceAccess.Absence[]> GetUserAbsenceListAsync(string employee);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    public interface IStorageResourceAccessChannel : XRaySample.StorageResourceAccess.IStorageResourceAccess, System.ServiceModel.IClientChannel
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    public partial class StorageResourceAccessClient : System.ServiceModel.ClientBase<XRaySample.StorageResourceAccess.IStorageResourceAccess>, XRaySample.StorageResourceAccess.IStorageResourceAccess
    {
        
    /// <summary>
    /// Implement this partial method to configure the service endpoint.
    /// </summary>
    /// <param name="serviceEndpoint">The endpoint to configure</param>
    /// <param name="clientCredentials">The client credentials</param>
    static partial void ConfigureEndpoint(System.ServiceModel.Description.ServiceEndpoint serviceEndpoint, System.ServiceModel.Description.ClientCredentials clientCredentials);
        
        public StorageResourceAccessClient() : 
                base(StorageResourceAccessClient.GetDefaultBinding(), StorageResourceAccessClient.GetDefaultEndpointAddress())
        {
            this.Endpoint.Name = EndpointConfiguration.NetTcpBinding_IStorageResourceAccess.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public StorageResourceAccessClient(EndpointConfiguration endpointConfiguration) : 
                base(StorageResourceAccessClient.GetBindingForEndpoint(endpointConfiguration), StorageResourceAccessClient.GetEndpointAddress(endpointConfiguration))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public StorageResourceAccessClient(EndpointConfiguration endpointConfiguration, string remoteAddress) : 
                base(StorageResourceAccessClient.GetBindingForEndpoint(endpointConfiguration), new System.ServiceModel.EndpointAddress(remoteAddress))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public StorageResourceAccessClient(EndpointConfiguration endpointConfiguration, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(StorageResourceAccessClient.GetBindingForEndpoint(endpointConfiguration), remoteAddress)
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public StorageResourceAccessClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress)
        {
        }
        
        public System.Threading.Tasks.Task RegisterAbsenceAsync(string employee, System.DateTime date, XRaySample.StorageResourceAccess.AbsenceReason reason)
        {
            return base.Channel.RegisterAbsenceAsync(employee, date, reason);
        }
        
        public System.Threading.Tasks.Task<XRaySample.StorageResourceAccess.Absence[]> GetUserAbsenceListAsync(string employee)
        {
            return base.Channel.GetUserAbsenceListAsync(employee);
        }
        
        public virtual System.Threading.Tasks.Task OpenAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndOpen));
        }
        
        public virtual System.Threading.Tasks.Task CloseAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginClose(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndClose));
        }
        
        private static System.ServiceModel.Channels.Binding GetBindingForEndpoint(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.NetTcpBinding_IStorageResourceAccess))
            {
                System.ServiceModel.NetTcpBinding result = new System.ServiceModel.NetTcpBinding();
                result.MaxBufferSize = int.MaxValue;
                result.ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max;
                result.MaxReceivedMessageSize = int.MaxValue;
                result.Security.Mode = System.ServiceModel.SecurityMode.None;
                return result;
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        private static System.ServiceModel.EndpointAddress GetEndpointAddress(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.NetTcpBinding_IStorageResourceAccess))
            {
                return new System.ServiceModel.EndpointAddress("net.tcp://localtest.me:10001/");
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        private static System.ServiceModel.Channels.Binding GetDefaultBinding()
        {
            return StorageResourceAccessClient.GetBindingForEndpoint(EndpointConfiguration.NetTcpBinding_IStorageResourceAccess);
        }
        
        private static System.ServiceModel.EndpointAddress GetDefaultEndpointAddress()
        {
            return StorageResourceAccessClient.GetEndpointAddress(EndpointConfiguration.NetTcpBinding_IStorageResourceAccess);
        }
        
        public enum EndpointConfiguration
        {
            
            NetTcpBinding_IStorageResourceAccess,
        }
    }
}
