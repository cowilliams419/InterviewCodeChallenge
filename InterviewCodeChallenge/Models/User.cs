using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterviewCodeChallenge.Models
{
    public class User
    {
        public string ID { get; set; }
        public List<User> Followers { get; set; }
        [JsonIgnore]
        public int Level { get; set; }
    }
}
