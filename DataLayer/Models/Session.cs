using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace EndToEnd.DataLayer.Models
{
    [Table("Sessions")]
    public class Session : SessionBase
    {
        public override TimeSpan Duration
        {
            get
            {
                return TimeSpan.FromMinutes(75);
            }
        }
    }
}
