using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Model
{
    [Table("Sessions")]
    public class Workshop : SessionBase
    {
        public override TimeSpan Duration
        {
            get
            {
                return TimeSpan.FromHours(6);
            }
        }
    }
}
