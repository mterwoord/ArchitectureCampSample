using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using EndToEnd.DataLayer.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndToEnd.DataLayer.Models {

  [Table("Sessions")]
  [KnownType(typeof(Session))]
  [KnownType(typeof(KeyNote))]
  [KnownType(typeof(Workshop))]
  [DataContract]
  [XmlRoot("Sessions")]
  [XmlInclude(typeof(Session))]
  [XmlInclude(typeof(KeyNote))]
  [XmlInclude(typeof(Workshop))]
  public abstract class SessionBase : EntityBase {
    protected SessionBase() {
      Tracks = new List<Track>();   // this is just for convenience
      Ratings = new List<Rating>(); // this is just for convenience
      Schedule = new Schedule();    // complex types are always required
    }

    [XmlElement("title")]
    [DataMember]
    [Required]
    [StringLength(100)]
    public string Title { get; set; }

    [XmlIgnore]
    [RestrictCount(2)]
    public virtual IList<Speaker> Speakers { get; set; }

    [DataMember] // for add
    [NotMapped]
    public int Speaker1Id { get; set; }

    [DataMember] // for add
    [NotMapped]
    public int Speaker2Id { get; set; }
    
    [XmlElement("speaker1")]
    [DataMember]
    [NotMapped]
    public Speaker Speaker1 { get; set; }

    [XmlElement("speaker2")]
    [DataMember]
    [NotMapped]
    public Speaker Speaker2 { get; set; }

    [XmlIgnore]
    [DataMember]
    [StringLength(5000)]
    public string Abstract { get; set; }

    // CDATA just for XML serialization 
    [NotMapped]
    [XmlElement("abstract")]
    public XmlCDataSection AbstractCdata {
      get {
        return new XmlDocument().CreateCDataSection(Abstract);
      }
      set {
        Abstract = value.Value;
      }
    }

    [XmlIgnore]
    [NotMapped]
    public abstract TimeSpan Duration { get; }

    [Required]
    [DataMember]
    public virtual List<Track> Tracks { get; set; }


    public List<Rating> Ratings { get; set; }

    [XmlElement("schedule")]
    [DataMember]
    [Unique]
    public Schedule Schedule { get; set; }


    public void SetSpeaker() {
      if (Speakers != null) {
        Speaker1 = Speakers.FirstOrDefault();
        Speaker1Id = (Speaker1 != null) ? Speaker1.Id : 0;
        if (Speaker1 != null && Speakers.Count == 2 && Speakers.LastOrDefault() != null) {
          Speaker2 = Speakers.LastOrDefault().Id == Speaker1.Id ? null : Speakers.LastOrDefault();
          Speaker2Id = (Speaker2 != null) ? Speaker2.Id : 0;
        }

      }

    }

    public static SessionBase Create(string p) {
      var ns = typeof(SessionBase).Namespace;
      var session = Activator.CreateInstance(Type.GetType(String.Format("{0}.{1}", ns, p))) as SessionBase;
      session.Speakers = new List<Speaker>();
      session.Tracks = new List<Track>();
      return session;
    }
  }
}
