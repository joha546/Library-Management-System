using LibraryData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryData
{
    public interface IPatron
    {
        // to get values of one particular patron
        Patron Get(int id);
        IEnumerable<Patron> GetAll();
        void Add(Patron newPatron);
        IEnumerable<CheckoutHistory> GetCheckoutHistory(int patronId);
        IEnumerable<Hold> GetHolds(int patronId);
        IEnumerable<Checkout> GetCheckouts(int patronId);

    }
}
