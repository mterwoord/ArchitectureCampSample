using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

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
