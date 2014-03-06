using EndToEnd.DatabaseInitializer.Setup;
using EndToEnd.DataLayer.Context;
using EndToEnd.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace EndToEnd.DatabaseInitializer {
  class Program {
    static void Main(string[] args) {
      string c = "";
      do {
        Console.Write("Backup (b) | Setup (s) | Restore (r) | Output (o) | Exit (Enter) : ");
        c = Console.ReadKey().KeyChar.ToString().Trim().ToLower();
        Console.WriteLine("");
        Database.SetInitializer<EndToEndContext>(null);
        switch (c) {
          case "b" :
            Console.WriteLine("Backup");            
            using (var ctx = new EndToEndContext()) {
              var xsSessions = new XmlSerializer(typeof(List<SessionBase>), new[] { typeof(Session), typeof(KeyNote), typeof(Workshop) });
              var sessions = ctx.Sessions
                .Include(s => s.Tracks)
                .Include(s => s.Ratings)
                .ToList();
              using (var fileStream = new FileStream(@"..\..\DemoData\Sessions_Backup.xml", FileMode.Create)) {
                xsSessions.Serialize(fileStream, sessions);
              }
              var speakers = ctx.Speakers
                .ToList();
              var xsSpeakers = new XmlSerializer(typeof(List<Speaker>));
              using (var fileStream = new FileStream(@"..\..\DemoData\Speakers_Backup.xml", FileMode.Create)) {
                xsSpeakers.Serialize(fileStream, speakers);
              }
            }
            break;
          case "s":
            Console.WriteLine("Setup");
            Database.SetInitializer(new DbDataSetup());
            ReadTestData();
            Console.WriteLine("done");
            break;
          case "r":
            Console.WriteLine("Restore");
            break;
          case "o":
            Console.WriteLine("Output");
            ReadTestData();
            break;
        }
      } while (c != "");

      
      Console.WriteLine("Key to close");
      Console.ReadLine();
    }

    private static void ReadTestData() {
      Console.WriteLine("Read Test Data...");

      Console.WriteLine("Sessions: ");

      using (var ctx = new EndToEndContext()) {
        // Sessions only , using OfType
        var sessions = ctx.Sessions.Include(s => s.Speakers).OfType<Session>();
        foreach (var session in sessions) {
          Console.WriteLine(session.Title);
          if (session.Speakers.Count() == 1) {
            Console.WriteLine("  - " + session.Speakers.First().Name);
          }
          if (session.Speakers.Count() == 2) {
            Console.WriteLine("  - " + session.Speakers.Last().Name);
          }
        }
      }

      using (var ctx = new EndToEndContext()) {
        // Number of Keynotes
        var kn = ctx.Sessions.OfType<KeyNote>().Count();
        Console.WriteLine("{0} Keynotes.", kn);
      }

      using (var ctx = new EndToEndContext()) {
        // Speakers with > 1 sessions
        Console.WriteLine("Speakers wit multiple sessions");
        var sp = ctx.Speakers.Where(s => s.Sessions.Count() > 1);
        Action<Speaker> cw = (s) => Console.WriteLine(s.Name);
        sp.ToList().ForEach(s => cw(s));

      }


    }

  }
}
