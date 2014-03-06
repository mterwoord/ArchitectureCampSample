using System.Windows;
using SharedUI;

namespace Shell
{
    public partial class App : Application
    {
        public App()
        {
            // Bootstrapper initialisieren
            Bootstrapper.InititializeServices();
        }
    }
}
