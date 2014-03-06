using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace EndToEnd.DataLayer.Models {
  
  [DataContract]
  [ComplexType]
  public class Schedule {

    [XmlAttribute("room")]
    [DataMember]
    [StringLength(5)]
    [RegularExpression(@"[A-C]\.\d{2,3}")]
    public string Room { get; set; }

    [XmlAttribute("starttime")]
    [DataMember]
    [Column(TypeName="datetime2")]
    public DateTime? StartTime { get; set; }

    [XmlIgnore]
    [DataMember]
    [NotMapped]
    public int SessionId { get; set; }

  }
}
