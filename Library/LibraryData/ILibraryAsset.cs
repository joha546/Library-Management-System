using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryData.Models;

namespace LibraryData
{
    public interface ILibraryAsset
    {
        // The purpose of the interface is simply defines -
        // The series of methods will be required for any kinds of services that implements the inrterface.

        IEnumerable<LibraryAsset> GetAll();
        LibraryAsset GetById(int id);

        // Method to actually add a new asset to dataset.
        void Add(LibraryAsset newAsset);
        string GetAuthorOrDirector(int id);
        string GetDeweyIndex(int id);
        string GetType(int id);
        string GetTitle(int id);
        string GetIsbn(int id);


        // to current location belong to the asset.
        LibraryBranch GetCurrentLocation(int id);

    }
}
