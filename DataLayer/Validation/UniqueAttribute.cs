using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndToEnd.DataLayer.Validation {
  
  
  [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
  public class UniqueAttribute : ValidationAttribute {
    public override Boolean IsValid(Object value) {
      // constraint implemented on database
      return true;
    }
  }

}
