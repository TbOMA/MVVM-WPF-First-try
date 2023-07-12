using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_FirsTry.Exeptions
{
    public class EmptyField : Exception
    {
        public string InputField { get; set; }

        public EmptyField(string inputField)
        {
            InputField = inputField;
        }

        public EmptyField(string? message, string inputField) : base(message)
        {
            InputField = inputField;
        }

        public EmptyField(string? message, Exception? innerException, string inputField) : base(message, innerException)
        {
            InputField = inputField;
        }


    }
}
