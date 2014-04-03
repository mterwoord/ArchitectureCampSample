using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace EndToEnd.DataLayer.Models
{
    [Table("Sessions")]
    public class KeyNote : SessionBase
    {
        public override TimeSpan Duration
        {
            get
            {
                return TimeSpan.FromMinutes(60);
            }
        }
    }
}
