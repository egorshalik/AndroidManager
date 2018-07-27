using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ManagerAndroid.Models
{
    public class Job
    {
        public int Id { get; set; }
        [StringLength(16, MinimumLength = 2)]
        [RegularExpression(@"[a-zA-Z0-9-]+")]
        public string Name { get; set; }
        [StringLength(255)]
        public string Description { get; set; }
        public int Complexity { get; set; }
        public List<Assignent> Assignents { get; set; }

        public Job()
        {
            Assignents = new List<Assignent>();
        }
    }
}
