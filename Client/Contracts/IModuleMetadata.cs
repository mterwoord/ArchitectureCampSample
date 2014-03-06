
namespace Contracts
{
    public interface IModuleMetadata
    {
        string Name { get; }
        string DisplayName { get; }
        string ImageUri { get; }
        int DisplayIndex { get; }
    }
}
