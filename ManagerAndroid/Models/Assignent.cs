using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagerAndroid.Models
{
    public class Assignent
    {
        public int Id { get; set; }
        public int AndroidId { get; set; }
        public Android Android { get; set; }
        public int JobId { get; set; }
        public Job Job { get; set; }
    }
}
