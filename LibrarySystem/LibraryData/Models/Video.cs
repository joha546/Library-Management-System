using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryData.Models
{
    public class Video
    {
        public int Id { get; set; }
        [Required]
        public string Director { get; set; }
    }
}
