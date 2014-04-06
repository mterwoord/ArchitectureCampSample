using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Model
{
    [Table("Speakers")]
    public class Speaker : EntityBase
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(100)]
        public string Company { get; set; }

        [EmailAddress]
        [StringLength(100)]
        public string EMail { get; set; }

        [RegularExpression(@"\d{2,5}/\d+")]
        [StringLength(100)]
        public string Phone { get; set; }

        [StringLength(2000)]
        public string Bio { get; set; }

        public byte[] Photo { get; set; }

        public IList<SessionBase> Sessions { get; set; }
    }
}
