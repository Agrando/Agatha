using System;
using System.Threading.Tasks;

namespace Agatha.Common
{
	public interface IRequestProcessor : IDisposable
	{
		Task<Response[]> Process(params Request[] requests);
		Task ProcessOneWayRequests(params OneWayRequest[] requests);
	}
}