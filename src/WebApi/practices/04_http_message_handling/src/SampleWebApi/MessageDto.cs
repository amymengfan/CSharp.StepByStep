using System.Runtime.Serialization;

namespace SampleWebApi
{
    [DataContract]
    public class MessageDto
    {
        [DataMember]
        public string Message { get; set; }

        public MessageDto(string message)
        {
            Message = message;
        }
    }
}