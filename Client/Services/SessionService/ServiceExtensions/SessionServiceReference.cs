using Contracts;

namespace Services.SessionServiceReference
{
    public partial class SessionServiceClient : ISessionService, IService
    {
        public IServicePool ServicePool { get; set; }
    }
}
