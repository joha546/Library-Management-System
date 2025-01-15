using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace LibraryData.Models
{
    public class Hold
    {
        public int Id { get; set; }
        public virtual LibraryAsset LibrarysAsset { get; set; }    
        public virtual LibraryCard LibraryCard { get; set; }
        public DateTime HoldPlaced { get; set; }

        // This will set up a Queue data structure to retreive the data.
    }
}
