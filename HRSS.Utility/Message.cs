using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSS.Utility
{
    public enum MessageType
    {
        Error,
        Warning,
        Information
    }

    /// <summary>
    /// Simple class for message.
    /// </summary>
    //[DataContract(IsReference = true)]
    public class Message
    {
        #region Class Attribute and Property

        //[DataMember]
        public string Code { get; set; }

        //[DataMember]
        public MessageType Type { get; set; }

        //[DataMember]
        public string Value { get; set; }

        private List<string> _extendedValues = new List<string>();

        //[DataMember]
        public List<string> ExtendedValues
        {
            get
            {
                return _extendedValues;
            }
            set
            {
                _extendedValues = value;
            }
        }

        public override string ToString()
        {
            return Value;
        }

        #endregion

        #region Constructor

        public Message()
        {

        }

        public Message(string message)
        {
            Type = MessageType.Error;
            Value = message;
        }

        public Message(string message, MessageType type)
        {
            Type = type;
            Value = message;
        }

        #endregion
    }
}
