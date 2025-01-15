using System;
using System.Collections.Generic;
using LibraryData;
using System.Text;

using LibraryData.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace LibraryServices
{
    public class LibraryAssetService : ILibraryAsset
    {
        private LibraryContext _context;   // instance of our DbContext
        public LibraryAssetService(LibraryContext context)
        {
            _context = context;   // Now we've access to all methods belong to our DbContext.
            // Now we can use in our methods to query the data.
        }
        public void Add(LibraryAsset newAsset)
        {
            _context.Add(newAsset);
            _context.SaveChanges();  // This will commit changes to database.
        }

        public IEnumerable<LibraryAsset> GetAll()
        {
            return _context.LibraryAssets
                .Include(asset => asset.Status)
                .Include(asset => asset.Location);
        }


        public LibraryAsset GetById(int id)
        {
            return _context.LibraryAssets
                .Include(asset => asset.Status)
                .Include(asset => asset.Location)
                .FirstOrDefault(asset => asset.Id == id);

            // Here it can be refactored.
            // GetAll()..FirstOrDefault(asset => asset.Id == id);
            // because we've written these 2 lines of code on first methods. it reduces our queries
        }

        public LibraryBranch GetCurrentLocation(int id)
        {
            return _context.LibraryAssets.FirstOrDefault(asset => asset.Id == id).Location;

            // Here it can be refactored as 
            // return _GetById(id).Location
        }

        public string GetDeweyIndex(int id)
        {
            // we've a discrimantor column in database to store data of multiple tables.

            if(_context.Books.Any(book => book.Id == id))
            {
                return _context.Books
                    .FirstOrDefault(book => book.Id == id).DeweyIndex;
            }

            else
            {
                return "";
            }
        }

        public string GetIsbn(int id)
        {
            if (_context.Books.Any(book => book.Id == id))
            {
                return _context.Books
                    .FirstOrDefault(book => book.Id == id).ISBN;
            }

            else
            {
                return "";
            }
        }

        public string GetTitle(int id)
        {
            return _context.LibraryAssets
                    .FirstOrDefault(a => a.Id == id)
                    .Title;
        }

        public string GetType(int id)
        {
            var book = _context.LibraryAssets.OfType<Book>()
                .Where(book => book.Id == id);

            return book.Any() ? "Book" : "Video";
        }

        public string GetAuthorOrDirector(int id)
        {
            // implementing using offtype method
            var isBook = _context.LibraryAssets.OfType<Book>()
                .Where(book => book.Id == id).Any();

            var isVideo = _context.LibraryAssets.OfType<Video>()
                .Where(video => video.Id == id).Any();

            return isBook ?
                _context.Books.FirstOrDefault(book => book.Id == id).Author :
                _context.Videos.FirstOrDefault(video => video.Id == id).Director
                ?? "Unknown";
        }
    }
}
