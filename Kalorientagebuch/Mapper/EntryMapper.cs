using Kalorientagebuch.Models;
using Kalorientagebuch.Viewmodels;

namespace Kalorientagebuch.Mapper
{
    public class EntryMapper
    {

        public static Entry Map(EntryViewModel entryViewModel)
        {
            return new Entry
            {
                Id = entryViewModel.Id,
                Description = entryViewModel.Description,
                Date = entryViewModel.Date,
                CaloriesAmount = entryViewModel.CaloriesAmount,
                UserId = entryViewModel.UserId,
                CategoryId = entryViewModel.CategoryId
            };
        }

        public static EntryViewModel Map(Entry entry)
        {
            return new EntryViewModel
            {
                Id = entry.Id,
                Description = entry.Description,
                Date = entry.Date,
                CaloriesAmount = entry.CaloriesAmount,
                UserId = entry.UserId,
                CategoryId = entry.CategoryId
            };
        }

        public static void Map(Entry entry, EntryViewModel entryViewModel)
        {
            entry.Id = entryViewModel.Id;
            entry.Description = entryViewModel.Description;
            entry.Date = entryViewModel.Date;
            entry.CaloriesAmount = entryViewModel.CaloriesAmount;
            entry.UserId = entryViewModel.UserId;
            entry.CategoryId = entryViewModel.CategoryId;
        }
    }
}
