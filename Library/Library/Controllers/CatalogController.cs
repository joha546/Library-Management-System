using Microsoft.AspNetCore.Mvc;
using LibraryData;
using System.Linq;
using Library.Models.Catalog;
using Microsoft.AspNetCore.Authorization.Infrastructure;


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

        public IActionResult Detail(int Id)
        {
            // we need to show a particular asset by their id.
            var asset = _assets.GetById(Id);

            // Domain Model.
            var model = new AssetDetailModel
            {
                AssetId = Id,
                Title = asset.Title,
                Year = asset.Year,
                Cost = asset.Cost,
                Status = asset.Status.Name,
                ImageUrl = asset.ImageUrl,
                AuthorOrDirector = _assets.GetAuthorOrDirector(Id),
                CurrectLocation = _assets.GetCurrentLocation(Id).Name,
                DeweyCallNumber = _assets.GetDeweyIndex(Id),
                ISBN = _assets.GetIsbn(Id),

            };
            return View(model);
        }
    }
}
