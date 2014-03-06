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
  [Table("Speakers")]
  public class Speaker : EntityBase {

    [XmlElement("name")]
    [DataMember]
    [Required]
    [StringLength(100)]
    public string Name { get; set; }

    [XmlElement("company")]
    [DataMember]
    [StringLength(100)]
    public string Company { get; set; }

    [XmlElement("email")]
    [DataMember]
    [EmailAddress]
    [StringLength(100)]
    public string EMail { get; set; }

    [XmlElement("phone")]
    [DataMember]
    [RegularExpression(@"\d{2,5}/\d+")]
    [StringLength(100)]
    public string Phone { get; set; }

    [XmlElement("bio")]
    [DataMember]
    [StringLength(2000)]
    public string Bio { get; set; }

    [XmlElement("photo")]
    [DataMember]
    public byte[] Photo { get; set; }

    [XmlIgnore]
    public IList<SessionBase> Sessions { get; set; }


  }
}
