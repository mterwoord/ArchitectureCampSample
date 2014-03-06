using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

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
