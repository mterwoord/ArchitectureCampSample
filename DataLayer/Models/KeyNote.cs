using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace EndToEnd.DataLayer.Models {
  
  [Table("Sessions")]
  [DataContract]
  [KnownType(typeof(SessionBase))]
  public class KeyNote : SessionBase {

    public override TimeSpan Duration {
      get {
        return TimeSpan.FromMinutes(60);
      }
    }

  }
}
