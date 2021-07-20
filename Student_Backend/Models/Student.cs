using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Student_Backend.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string  FirstName { get; set; }
        public string  LastName { get; set; }
        public string  Phone { get; set; }
        public string  Address { get; set; }
        public int  CoursNumber { get; set; }
        public int? TeacherId { get; set; }
        [JsonIgnore]
        public virtual Teacher Teacher { get; set; }
    }
}
