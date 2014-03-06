using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace EndToEnd.DataLayer.Models {
  
  [XmlRoot("tracks")]
  [DataContract]
  [Table("Tracks")]
  public class Track : EntityBase {

    [XmlElement("name")]
    [DataMember]
    [Required]
    [StringLength(255)]
    public string Name { get; set; }

    [XmlIgnore]
    public IList<SessionBase> Sessions { get; set; }

  }
}
