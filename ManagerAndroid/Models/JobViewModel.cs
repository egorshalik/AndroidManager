using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagerAndroid.Models
{
    public class JobViewModel
    {
        public Job Job { get; set; }
        public List<Android> Androids { get; set; }

        public JobViewModel(Job job, List<Android> androids)
        {
            Job = job;
            Androids = androids;
        }
    }
}
