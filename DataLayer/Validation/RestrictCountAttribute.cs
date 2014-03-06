using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndToEnd.DataLayer.Validation {

  [AttributeUsage(AttributeTargets.Property, AllowMultiple= false)]
  public class RestrictCountAttribute : ValidationAttribute {

    public int MaxCount { get; set; }

    public RestrictCountAttribute(int maxCount) {
      MaxCount = maxCount;
    }

    public override bool IsValid(object value) {
      if (value is IList) {
        return ((IList)value).Count <= MaxCount;
      } else {
        return false;
      }
    }

  }
}
