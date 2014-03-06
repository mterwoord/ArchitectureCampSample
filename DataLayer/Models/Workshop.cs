using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace EndToEnd.DataLayer.Models {

  [DataContract]
  [KnownType(typeof(SessionBase))]
  [Table("Sessions")]
  public class Workshop : SessionBase {

    public override TimeSpan Duration {
      get {
        return TimeSpan.FromHours(6);
      }
    }

  }
}
