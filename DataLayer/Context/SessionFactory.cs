using EndToEnd.DataLayer.Models;
using System;

namespace EndToEnd.DataLayer.Context {
  public static class SessionFactory {

    public static SessionBase CreateSession(string sessionType){
      switch (sessionType.ToLowerInvariant()){
        case "keynote":
          return new KeyNote();
        case "session":
          return new Session();
        case "workshop":
          return new Workshop();
        default:
          throw new ArgumentOutOfRangeException("sessionType");
      }
    }

  }
}
