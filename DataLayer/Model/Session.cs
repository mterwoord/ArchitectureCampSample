using System;
using System.ComponentModel.DataAnnotations.Schema;
using EndToEnd.DataLayer.Models;

namespace EndToEnd.DataLayer.Model
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
