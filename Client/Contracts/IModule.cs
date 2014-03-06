
namespace Contracts
{
    public interface IModule
    {
        void Initialize(IServicePool servicePool);
        IView GetView();
        void Close();
    }
}
