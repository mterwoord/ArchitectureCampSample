using System;
using Contracts;

namespace Shell.Models
{
    public class Module : ModelBase
    {
        private string _name;
        private Uri _imageUri;

        public string Name
        {
            get { return _name; }
            set { _name = value; this.OnPropertyChanged(); }
        }

        public Uri ImageUri
        {
            get { return _imageUri; }
            set { _imageUri = value; this.OnPropertyChanged(); }
        }
    }
}
