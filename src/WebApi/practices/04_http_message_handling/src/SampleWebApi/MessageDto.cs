using System.Runtime.Serialization;

namespace SampleWebApi
{
    [DataContract]
    public class MessageDto
    {
        [DataMember]
        public string Message { get; set; }
    }
}