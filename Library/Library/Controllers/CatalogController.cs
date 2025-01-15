using Microsoft.AspNetCore.Mvc;
using LibraryData;
using System.Linq;
using Library.Models.Catalog;

namespace Library.Controllers
{
    public class CatalogController : Controller
    {
        private ILibraryAsset _assets;
        public CatalogController(ILibraryAsset assets) 
        {
            // constructor to keep data private.
            _assets = assets;
        }

        public IActionResult Index()
        {
            var assetModels = _assets.GetAll();
            var listingResult = assetModels
                .Select(result => new AssetIndexListingModel       // our custom view model.
                {
                    Id = result.Id,
                    ImageUrl = result.ImageUrl,
                    AuthorOrDirector = _assets.GetAuthorOrDirector(result.Id),
                    DeweyCallNumber = _assets.GetDeweyIndex(result.Id),
                    Title = result.Title,
                    Type = _assets.GetType(result.Id),
                });

            var model = new AssetIndexListingModel()
            {
                Assets = listingResult
            };
            return View(model);
        }
    }
}
