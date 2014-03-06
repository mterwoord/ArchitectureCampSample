using System;
using System.Collections.ObjectModel;

namespace Shell.Models
{
    public class ModulesDictionary
    {
        private const string BASE_URI = "pack://application:,,,/SharedUI;component/Images/";
        private ObservableCollection<Module> _modules;

        public ModulesDictionary()
        {
            _modules = new ObservableCollection<Module>()
            {
                new Module { Name = "Sport", ImageUri = new Uri(BASE_URI + "Basketball.png") },
                new Module { Name = "Tiere", ImageUri = new Uri(BASE_URI + "Fish.png") },
                new Module { Name = "Auto", ImageUri = new Uri(BASE_URI + "Car.png") },
                new Module { Name = "Dokumente", ImageUri = new Uri(BASE_URI + "Documents.png") },
                new Module { Name = "Reisen", ImageUri = new Uri(BASE_URI + "Flights.png") },
                new Module { Name = "Freunde", ImageUri = new Uri(BASE_URI + "Friends.png") },
                new Module { Name = "Konten", ImageUri = new Uri(BASE_URI + "Accounts.png") },
                new Module { Name = "Geld", ImageUri = new Uri(BASE_URI + "Money.png") },
                new Module { Name = "Telefonbuch", ImageUri = new Uri(BASE_URI + "PhoneBook.png") },
                new Module { Name = "Bilder", ImageUri = new Uri(BASE_URI + "Pictures.png") },
                new Module { Name = "Musik", ImageUri = new Uri(BASE_URI + "Music.png") },
                new Module { Name = "Bücher", ImageUri = new Uri(BASE_URI + "Books.png") },
                new Module { Name = "Filme", ImageUri = new Uri(BASE_URI + "Movies.png") },
                new Module { Name = "Kochen", ImageUri = new Uri(BASE_URI + "Cooking.png") },
                new Module { Name = "Schule", ImageUri = new Uri(BASE_URI + "School.png") },
                new Module { Name = "Wein", ImageUri = new Uri(BASE_URI + "Wine.png") },
                new Module { Name = "Cocktails", ImageUri = new Uri(BASE_URI + "Cocktails.png") },
            };
        }

        public ObservableCollection<Module> GetModules()
        {
            return _modules;
        }
    }
}
