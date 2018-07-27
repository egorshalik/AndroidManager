using System.Collections.Generic;
using System.IO;

namespace ManagerAndroid.Models
{
    public class Android
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] Avatar { get; set; }
        public int Reability { get; set; }
        public int Status { get; set; }
        public List<Assignent> Assignents{get;set;}
        public string Skills { get; set; }
        
        public Android()
        {
            Assignents = new List<Assignent>();
        }

        public static explicit operator AndroidViewModel(Android android)
        {
            return new AndroidViewModel
            {
                Name = android.Name,
                ByteAvatar = android.Avatar,
                Skills = android.Skills,
                Id = android.Id
            };
        }

        public static explicit operator Android(AndroidViewModel androidView) {
            Android android = new Android
            {
                Name = androidView.Name,
                Skills = androidView.Skills
            };
            if (androidView.Avatar != null)
            {
                byte[] imageData = null;
                using (var binaryReader = new BinaryReader(androidView.Avatar.OpenReadStream()))
                {
                    imageData = binaryReader.ReadBytes((int)androidView.Avatar.Length);
                }
                android.Avatar = imageData;
            }
            return android;
        }

        public void Change()
        {
            Reability--;
            if (Reability == 0)
                Status = 0;
        }
    }
}
