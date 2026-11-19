using Kalorientagebuch.Models;
using Kalorientagebuch.Models.Requests;

namespace Kalorientagebuch.Interfaces
{
    public interface IEntryService
    {
        public EntryCreateRequest GetProductInfosByBarcode(string barcode);

    }
}
