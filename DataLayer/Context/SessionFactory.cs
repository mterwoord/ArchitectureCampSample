using EndToEnd.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
