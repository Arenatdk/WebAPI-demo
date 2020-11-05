using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WebApi.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        [JsonIgnore]
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}