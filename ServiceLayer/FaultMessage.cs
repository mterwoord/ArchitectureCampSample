using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ServiceLayer {

  [DataContract]
  public class FaultMessage {

    public FaultMessage(Exception ex) {
      ErrorMessage = ex.Message;
      if (ex.InnerException != null) {
        InnerExceptionMessage = ex.InnerException.Message;
      }
    }

    [DataMember]
    public string ErrorMessage { get; set; }

    [DataMember]
    public string InnerExceptionMessage { get; set; }

  }
}