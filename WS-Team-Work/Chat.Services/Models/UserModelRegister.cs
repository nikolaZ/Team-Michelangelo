using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Chat.Services.Models
{
    [DataContract]
    public class UserModelRegister : UserModelLogin
    {
        [DataMember(Name = "nickname")]
        public string Nickname { get; set; }
    }
}