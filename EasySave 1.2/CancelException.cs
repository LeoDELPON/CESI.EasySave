using System;
using System.Collections.Generic;
using System.Text;

namespace EasySave_1._2
{
    [Serializable]
    public class CancelException : Exception
    {
        public CancelException() : base() {  }
     //   public CancelException(string message) : base(message) {
       // }
      //  public CancelException(string message, Exception inner) : base(message, inner) { }

        // A constructor is needed for serialization when an
        // exception propagates from a remoting server to the client.
      //  protected CancelException(System.Runtime.Serialization.SerializationInfo info,
          //  System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
