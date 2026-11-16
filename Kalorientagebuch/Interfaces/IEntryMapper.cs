using Kalorientagebuch.Models;
using Kalorientagebuch.Models.Requests;
using Kalorientagebuch.Models.Responses;
using Kalorientagebuch.Viewmodels;

namespace Kalorientagebuch.Interfaces
{
    public interface IEntryMapper
    {
        //veraltet
        public Entry Map(EntryViewModel entryViewModel);

        //Map CreateRequest to Domain Model
        public Entry Map(EntryCreateRequest entryCreateRequest);

        //Map Domain Model to EditRequest
        public EntryEditRequest MapEntryEditRequest(Entry entry);

        //Map EditRequest to Domain Model
        public void Map(Entry entry, EntryEditRequest entryEditRequest);

        //
        public EntryDetailResponse MapEntryDetailResponse(Entry entry);

        public IndexItemEntryViewModel Map(Entry entry);

        public void Map(Entry entry, EntryViewModel entryViewModel);


    }
}
