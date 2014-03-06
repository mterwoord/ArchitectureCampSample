using ServiceTestClient.SessionServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ServiceTestClient {
  class Program {
    static void Main(string[] args) {

      var client = new SessionServiceClient();

      Console.WriteLine("---");
      var speakers = client.GetSpeakerList();
      foreach (var speaker in speakers) {
        Console.WriteLine("{0}:{1}", speaker.Id, speaker.Name);
      }


      var types = client.GetSessionTypes();
      foreach (var item in types)
	    {
		      Console.WriteLine("{0}", item);
      }

      var sessions = client.GetSessionList();
      foreach (var sessionBase in sessions) {
        Console.WriteLine("{0}:{1}", sessionBase.SessionBaseId, sessionBase.Title);
        //Console.WriteLine("{0}:{1}", sessionBase.Speaker1Id == null ? "X" : sessionBase.Speaker1.Name, sessionBase.Speaker2 == null ? "X" : sessionBase.Speaker2.Name);
      }
      Console.WriteLine("");
      Console.Write("ID: ");
      var id = Console.ReadLine();
      Action<IEnumerable<Slot>> showSlots = s => s.ToList().ForEach(slot => Console.WriteLine("{0} {1}", slot.Start.ToShortTimeString(), slot.IsBreak ? "Break" : "Session"));
      Console.WriteLine("Pausen:");
      var slots = client.AllBreaks();
      showSlots(slots);
      Console.WriteLine("Alle:");
      slots = client.AllSlots();
      showSlots(slots);
      Console.WriteLine("Freie:");
      slots = client.GetNotAssignedSlots();
      showSlots(slots);

      // Slot zuweisen
      Console.WriteLine("Ersten zuweisen.");
      var session = client.GetSessionById(Int32.Parse(id));
      session.StartTime = slots.Where(slot => !slot.IsBreak).First().Start;
      session.Room = "A.111";
      client.UpdateSession(session);
      showSlots(slots);
      Console.WriteLine("** Freie Slots: ");
      // danach muss die Anzahl der freien Slots einer weniger sein
      slots = client.AllAssignableSlots();
      showSlots(slots);
      //var rating = new Rating();
      //var session = client.GetSessionById(Int32.Parse(id));
      //rating.SessionId = session.Id;
      //rating.SpeakerId = session.Speaker1.Id;
      //rating.Rate = 3;
      //rating.Comment = "Toll, es sollten 10 Zeichen sein";
      //client.AddRating(rating);

      //Console.WriteLine("");
      //Console.Write("Id: ");
      //var id = Console.ReadLine();
      //if (!String.IsNullOrEmpty(id)) {
      //  Console.WriteLine("");
      //  Console.Write("Title: ");
      //  var title = Console.ReadLine();
      //  var session = client.GetSessionById(Int32.Parse(id));
      //  session.Title = title;
      //  client.UpdateSession(session);
      //  // 
      //  Console.WriteLine("");
      //  Console.WriteLine("Room:");
      //  var room = Console.ReadLine();
      //  var schedule = session.Schedule;
      //  schedule.Room = room;
      //  client.AddSchedule(Int32.Parse(id), schedule);
      //}

      //Console.WriteLine("");
      //Console.Write("Id: ");
      //id = Console.ReadLine();
      //if (!String.IsNullOrEmpty(id)) {
      //  Console.WriteLine("");
      //  Console.Write("Name: ");
      //  var name = Console.ReadLine();
      //  var speaker = client.GetSpeakerById(Int32.Parse(id));
      //  speaker.Name = name;
      //  speaker.Photo = null;
      //  client.UpdateSpeaker(speaker);
      //}

      Console.WriteLine("Done");
      Console.ReadLine();
    }
  }
}
