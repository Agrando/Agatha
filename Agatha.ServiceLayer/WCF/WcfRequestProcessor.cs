using Agatha.Common;
using Agatha.Common.InversionOfControl;
using Agatha.Common.WCF;
using Agatha.ServiceLayer.WCF.Rest;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Threading.Tasks;

namespace Agatha.ServiceLayer.WCF
{
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
	[ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
	[AddMessageInspectorBehavior]
	[AddErrorLoggingBehavior]
	public class WcfRequestProcessor : IWcfRequestProcessor, IWcfRestJsonRequestProcessor, IWcfRestXmlRequestProcessor
	{
		[TransactionFlow(TransactionFlowOption.Allowed)]
		public Task<Response[]> Process(params Request[] requests)
		{
			using (var processor = IoC.Container.Resolve<IRequestProcessor>())
			{
				try
				{
					return processor.Process(requests);
				}
				finally
				{
					// IRequestProcessor is a transient component so we must release it
					IoC.Container.Release(processor);
				}
			}
		}

		public Task ProcessOneWayRequests(params OneWayRequest[] requests)
		{
			using (var processor = IoC.Container.Resolve<IRequestProcessor>())
			{
				try
				{
					return processor.ProcessOneWayRequests(requests);
				}
				finally
				{
					// IRequestProcessor is a transient component so we must release it
					IoC.Container.Release(processor);
				}
			}
		}

        public Task<Response[]> Process()
        {
            var collection = WebOperationContext.Current.IncomingRequest.UriTemplateMatch.QueryParameters;

            var builder = new RestRequestBuilder();

            var requests = builder.GetRequests(collection);

            return Process(requests);
        }

        public Task ProcessOneWayRequests()
        {
            var collection = WebOperationContext.Current.IncomingRequest.UriTemplateMatch.QueryParameters;

            var builder = new RestRequestBuilder();

            var requests = builder.GetOneWayRequests(collection);

            return ProcessOneWayRequests(requests);
        }
    }
}