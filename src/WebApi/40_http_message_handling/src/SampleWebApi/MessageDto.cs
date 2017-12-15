using System.Runtime.Serialization;

namespace SampleWebApi
{
    [DataContract]
    class MessageDto
    {
        [DataMember]
        public string Message { get; set; }
    }
}