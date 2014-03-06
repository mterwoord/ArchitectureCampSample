using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;

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
