using Kalorientagebuch.Models;
using Kalorientagebuch.Models.Requests;
using Kalorientagebuch.Viewmodels;

namespace Kalorientagebuch.Interfaces
{
    public interface IEntryMapper
    {
        public Entry Map(EntryViewModel entryViewModel);

        public Entry Map(EntryCreateRequest entryCreateRequest);

        public EntryEditRequest MapEntryEditRequest(Entry entry);

        public Entry Map(EntryEditRequest entryEditRequest);

        public IndexItemEntryViewModel Map(Entry entry);

        public void Map(Entry entry, EntryViewModel entryViewModel);


    }
}
