using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Xml.Linq;
using Microsoft.SqlServer.Server;
using System.Security.Cryptography;
using System.Text;
using EndToEnd.DataLayer.Context;
using EndToEnd.DataLayer.Models;
using System.Reflection;
using System.ComponentModel.DataAnnotations.Schema;
using EndToEnd.DataLayer.Validation;

namespace EndToEnd.DatabaseInitializer.Setup {

  public class DbDataSetup : DropCreateDatabaseAlways<EndToEndContext> {

    private EndToEndContext context;

    protected override void Seed(EndToEndContext context) {
      base.Seed(context);
      this.context = context;
      // create index
      foreach (var pi in ((object)context).GetType().GetProperties()) {
        // alle DbSet ermitteln
        if (pi.PropertyType.IsGenericType && pi.PropertyType.Name.Contains("DbSet")) {
          var t = pi.PropertyType.GetGenericArguments()[0];
          // Abweichender Tabellenname?
          string tableName;
          var mytableNameAttribute = t.GetCustomAttributes(typeof(TableAttribute), true);
          if (mytableNameAttribute.Length > 0) {
            TableAttribute mytable = mytableNameAttribute[0] as TableAttribute;
            tableName = mytable.Name;
          } else {
            tableName = pi.Name;
          }
          // Unqiue Attribut suchen
          foreach (var piEntity in t.GetProperties()) {
            if (piEntity.GetCustomAttributes(typeof(UniqueAttribute), true).Length > 0) {
              var fieldName = piEntity.Name;
              var keyName = fieldName;
              // resolve complex types into "basetype_property"
              if (piEntity.GetType().IsClass && piEntity.PropertyType.GetCustomAttributes(typeof(ComplexTypeAttribute), true).Length > 0) {
                fieldName = String.Join(", ", piEntity.PropertyType
                  .GetProperties()                                                                  // all properties
                  .Where(p => p.GetCustomAttributes(typeof(NotMappedAttribute), true).Length == 0)  // exclude not mapped
                  .Select(p => String.Format("{0}_{1}", keyName, p.Name)).ToArray());               // create same name as EF 
              }
              // create store expression 
              //context.Database.ExecuteSqlCommand(String.Format("CREATE UNIQUE NONCLUSTERED INDEX con_Unique_{0}_{1} ON {0} ({2}) WHERE {2} IS NOT NULL",
              //  tableName, keyName, fieldName));              
            }
          }
        }
      }
      Initialize();
    }

    public void Initialize() {

      // fixed calendar items:
      var slots = new[] { 
        new { h = 8, m = 30, b = false},
        new { h = 9, m = 15, b = true},
        new { h = 9, m = 30, b = false},
        new { h = 10, m = 15, b = false},
        new { h = 11, m = 0, b = false},
        new { h = 11, m = 45, b = false},
        new { h = 12, m = 30, b = true},
        new { h = 14, m = 0, b = false},
        new { h = 14, m = 45, b = false},
        new { h = 15, m = 30, b = true},
        new { h = 16, m = 15, b = false},
        new { h = 17, m = 0, b = false}
      };

      // all or nothing
      //using (var scope = context.BeginTransaction()) {

      var xDocSpeaker = XDocument.Load("DemoData/Speaker.xml"); // copied to output dir
      var xSpeaker = xDocSpeaker.Root.Elements("speaker");
      var speaker = xSpeaker.Select(item => new Speaker {
        Name = item.Element("name").Value,
        Bio = item.Element("bio").Value,
        Company = item.Element("company").Value,
        EMail = item.Element("mail").Value,
        Phone = item.Element("phone").Value,
        Photo = File.ReadAllBytes(Path.Combine("DemoData", item.Element("photo").Value))
      });
      speaker.ToList().ForEach(s => context.Speakers.Add(s));
      context.Save(); // get exception earlier

      var xDocTracks = XDocument.Load("DemoData/Tracks.xml");
      var xTracks = xDocTracks.Root.Elements("track");
      var tracks = xTracks.Select(item => new Track {
        Name = item.Attribute("name").Value
      });
      tracks.ToList().ForEach(t => context.Tracks.Add(t));
      context.Save(); // get exception earlier

      var xDocSessions = XDocument.Load("DemoData/Sessions.xml"); // copied to output dir
      var xSessions = xDocSessions.Root.Elements("session");
      var sessions = xSessions.Select(item => {
        var session = SessionFactory.CreateSession(item.Attribute("sessiontype").Value);
        session.Title = item.Element("title").Value;
        session.Abstract = item.Element("description").Value;
        var speakersForSession = item.Element("speakers").Elements("speaker").Select(s => s.Value);
        session.Speakers = context.Speakers.Where(s => speakersForSession.Any(e => e == s.Name)).ToList();
        var tracksForSession = item.Element("tracks").Elements("track").Select(t => t.Value);
        session.Tracks = context.Tracks.Where(t => tracksForSession.Any(e => e == t.Name)).ToList();
        //session.Schedule = new Schedule();
        return session;
      });
      sessions.ToList().ForEach(s => context.Sessions.Add(s));


      foreach (var item in slots) {
        context.Slots.Add(new Slot { Start = new DateTime(2013, 11, 25, item.h, item.m, 0), IsBreak = item.b });
      }

      context.Save();
      //scope.Commit();
      //}

    }

  }
}
