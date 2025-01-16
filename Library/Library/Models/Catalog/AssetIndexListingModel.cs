using System.Collections.Generic;

namespace Library.Models.Catalog
{
    public class AssetIndexListingModel
    {
        // to retrieve information of a particular asset details, images etc.
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public string Title { get; set; }
        public string AuthorOrDirector { get; set; }
        public string Type { get; set; }
        public string DeweyCallNumber { get; set; }
        public string NumberOfCopies { get; set; }
        public IEnumerable<AssetIndexListingModel> Assets { get; internal set; }
    }
}
