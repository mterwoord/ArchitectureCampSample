using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndToEnd.DataLayer.Models {

  [Table("Slots")]
  public class Slot : EntityBase {

    [Required]
    public DateTime Start { get; set; }

    public bool IsBreak { get; set; }

    public int SessionId { get; set; }
  }
}
