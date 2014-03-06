using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace EndToEnd.DataLayer.Models {

  [Table("Sessions")]
  [DataContract]
  [KnownType(typeof(SessionBase))]
  public class Session : SessionBase {

    public override TimeSpan Duration {
      get {
        return TimeSpan.FromMinutes(75);
      }
    }


  }
}
