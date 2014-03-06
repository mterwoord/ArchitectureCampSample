using System;
using System.ComponentModel.Composition;

namespace Contracts
{
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple=false, Inherited=true)]
    public class ModuleMetadataAttribute : ExportAttribute, IModuleMetadata
    {
        public ModuleMetadataAttribute(string name, string displayName, string imageUri, int displayIndex)
            : base(typeof(IModule))
        {
            this.Name = name;
            this.DisplayName = displayName;
            this.ImageUri = imageUri;
            this.DisplayIndex = displayIndex;
        }
        public string Name { get; private set; }
        public string DisplayName { get; private set; }
        public string ImageUri { get; private set; }
        public int DisplayIndex { get; private set; }
    }
}
