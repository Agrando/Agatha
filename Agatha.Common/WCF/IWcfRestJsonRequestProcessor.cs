using System.ServiceModel;
using System.ServiceModel.Web;
using System.Threading.Tasks;

namespace Agatha.Common.WCF
{
    [ServiceContract]
    public interface IWcfRestJsonRequestProcessor
    {
        [OperationContract(Name = "ProcessJsonRequestsGet")]
        [ServiceKnownType("GetKnownTypes", typeof(KnownTypeProvider))]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        [WebGet(UriTemplate = "/", BodyStyle = WebMessageBodyStyle.WrappedResponse, ResponseFormat = WebMessageFormat.Json)]
        Task<Response[]> Process();

        [OperationContract(Name = "ProcessJsonRequestsPost")]
        [ServiceKnownType("GetKnownTypes", typeof(KnownTypeProvider))]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        [WebInvoke(UriTemplate = "/post", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        Task<Response[]> Process(Request[] requests);

        [OperationContract(Name = "ProcessOneWayJsonRequestsPost", IsOneWay = true)]
        [ServiceKnownType("GetKnownTypes", typeof(KnownTypeProvider))]
        [WebInvoke(UriTemplate = "/post/oneway", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        Task ProcessOneWayRequests(OneWayRequest[] requests);
    }
}
