using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EndToEnd.DataLayer.Models {
  
  [DataContract]
  [XmlRoot("ratings")]
  [Table("Ratings")]
  public class Rating : EntityBase {

    [XmlIgnore] // serialize through session object
    [NotMapped] // use navigation property
    [DataMember]
    public int SpeakerId { get; set; }

    [XmlIgnore] // serialize through session object
    //[NotMapped] // use navigation property
    [DataMember]
    public int SessionId { get; set; }

    [XmlIgnore]
    [Required]
    public Speaker Speaker { get; set; }

    [XmlIgnore]
    [Required]
    public SessionBase Session { get; set; }

    [XmlElement("rate")]
    [Range(1,6)]
    [DataMember]
    public int Rate { get; set; }
    
    [XmlElement("comment")]
    [StringLength(500)]
    [MinLength(10)]
    [DataMember]
    public string Comment { get; set; }

  }
}
