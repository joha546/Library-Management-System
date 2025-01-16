using LibraryData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryData
{
    public interface ICheckout
    {
        IEnumerable<Checkout> GetAll();  // done
        Checkout GetById(int checkoutId);  // done
        void Add(Checkout newcheckout);   // done.
        void CheckOutItem(int assetId, int libraryCardId);
        void CheckInItem(int assetId, int libraryCardId);
        IEnumerable<CheckoutHistory> GetCheckoutHistories(int id);  // done

        void PlaceHold(int assetid, int libraryCardId);
        string GetCurrentHoldPatronName(int id);
        DateTime GetCurrentHoldPlaced(int id);
        IEnumerable<Hold> GetCurrentHolds(int id);

        void MarkLost (int assetid);
        void MarkFound (int assetid);
    }
}
