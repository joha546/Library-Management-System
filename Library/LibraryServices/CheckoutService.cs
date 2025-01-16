using LibraryData;
using LibraryData.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryServices
{
    public class CheckoutService : ICheckout
    {
        protected LibraryContext _context;

        //constructor.
        public CheckoutService(LibraryContext context)
        {
            _context = context;
        }
        public void Add(Checkout newcheckout)
        {
            _context.Add(newcheckout);
            _context.SaveChanges();
        }

        public IEnumerable<Checkout> GetAll()
        {
            return _context.Checkouts;
        }

        public Checkout GetById(int checkoutId)
        {
            return GetAll()
                .FirstOrDefault(checkout => checkout.Id == checkoutId);
        }

        public IEnumerable<CheckoutHistory> GetCheckoutHistories(int id)
        {
            return _context.CheckoutHistories
                .Include(h => h.LibrarysAsset)
                .Include(h => h.LibraryCard)
                .Where(h => h.LibrarysAsset.Id == id);
        }


        public IEnumerable<Hold> GetCurrentHolds(int id)
        {
            return _context.Holds
                .Include(h => h.LibrarysAsset)
                .Where(h => h.LibrarysAsset.Id == id);
        }

        public Checkout GetLatestCheckout(int assetid)
        {
            return _context.Checkouts
                .Where(c => c.LibraryAsset.Id == assetid)
                .OrderByDescending(c => c.Since)
                .FirstOrDefault();
        }

        public void MarkFound(int assetid)
        {
            var now = DateTime.Now;
            var item = _context.LibraryAssets
                .FirstOrDefault(a => a.Id == assetid);

            _context.Update(item);
            item.Status = _context.Statuses
                .FirstOrDefault(status => status.Name == "Available");

            // remove any existing checkouts on the item.
            var checkout = _context.Checkouts
                .FirstOrDefault(co => co.LibraryAsset.Id == assetid);
            if (checkout != null)
            {
                _context.Remove(checkout);
            }

            // close any existing checkout history.
            var history = _context.CheckoutHistories
                .FirstOrDefault(h => h.LibrarysAsset.Id == assetid && h.CheckedIn == null);

            if (history != null)
            {
                _context.Update(history);
                history.CheckedIn = now;
            }
            _context.SaveChanges();

            // we can refactor this methods by implementing these operations into three functions like-
            // RemoveExistingCheckout , CloseExistingCheckoutHistory, UpdateAssetStatus.
        }

        public void MarkLost(int assetid)
        {
            // updating a library asset status just from available to not available.
            var item = _context.LibraryAssets
                .FirstOrDefault(a => a.Id == assetid);

            _context.Update(item);
            item.Status = _context.Statuses
                .FirstOrDefault(status => status.Name == "Lost");

            _context.SaveChanges();
            
        }
        public void CheckInItem(int assetId, int libraryCardId)
        {
            throw new NotImplementedException();
        }

        public void CheckOutItem(int assetId, int libraryCardId)
        {
            throw new NotImplementedException();
        }
        public void PlaceHold(int assetid, int libraryCardId)
        {
            throw new NotImplementedException();
        }
        public string GetCurrentHoldPatronName(int id)
        {
            throw new NotImplementedException();
        }

        public DateTime GetCurrentHoldPlaced(int id)
        {
            throw new NotImplementedException();

        }
    }
}
