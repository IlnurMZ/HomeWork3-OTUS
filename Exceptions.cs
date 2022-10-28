using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork
{
    public class ParseException : Exception
    {
        public string message;
        public ParseException(string message)
        {
            this.message = message;
        }
    }

    public class BadOperationException : Exception 
    {
        public string message;
        public BadOperationException(string message)
        {
            this.message=message;
        }
    }

    public class Evil13Exception : Exception { } 
      
    public class IncorrectFormatException : Exception { }

    public class LostOperationException : Exception 
    {
        public string dopInfo = string.Empty;
    }

}
