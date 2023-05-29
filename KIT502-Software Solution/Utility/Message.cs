using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

internal class Message
{
    public enum MessageTypes
    {
        Information = 0,
        Error = 1,
        Warning = 2
    }

    public MessageTypes Type { get; set; }
    public string Msg { get; set; }

    public Message()
    {
        this.Type = MessageTypes.Information;
        this.Msg = string.Empty;
    }

    public Message(MessageTypes type, string msg)
    {
        this.Type = type;
        this.Msg = msg;
    }
}
