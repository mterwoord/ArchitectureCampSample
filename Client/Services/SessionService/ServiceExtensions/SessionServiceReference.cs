using Contracts;
using System.ServiceModel;

namespace Services.SessionServiceReference
{
    public partial class SessionServiceClient : ClientBase<ISessionService>, ISessionService, IService
    {
        public IServicePool ServicePool { get; set; }
    }
}
