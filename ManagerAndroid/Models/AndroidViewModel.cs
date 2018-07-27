using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ManagerAndroid.Models
{
    public class AndroidViewModel
    {
        public int Id { get; set; }
        [StringLength(24,MinimumLength =5)]
        [RegularExpression(@"[a-zA-Z0-9-]+")]
        public string Name { get; set; }
        public IFormFile Avatar { get; set; }
        public string Skills { get; set; }
        public byte[] ByteAvatar { get; set; }
    }
}
