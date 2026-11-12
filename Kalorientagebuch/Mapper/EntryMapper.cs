using Kalorientagebuch.Interfaces;
using Kalorientagebuch.Models;
using Kalorientagebuch.Models.Requests;
using Kalorientagebuch.Viewmodels;

namespace Kalorientagebuch.Mapper
{
    public class EntryMapper : IEntryMapper
    {

        public Entry Map(EntryViewModel entryViewModel)
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

        //public static EntryViewModel Map(Entry entry)
        //{
        //    return new EntryViewModel
        //    {
        //        Id = entry.Id,
        //        Description = entry.Description,
        //        Date = entry.Date,
        //        CaloriesAmount = entry.CaloriesAmount,
        //        UserId = entry.UserId,
        //        CategoryId = entry.CategoryId,
        //        Category = CategoryMapper.Map(entry.Category)
        //    };
        //}

        public Entry Map(EntryCreateRequest entryCreateRequest)
        {
            return new Entry
            {
                Description = entryCreateRequest.Description,
                CaloriesAmount = entryCreateRequest.CaloriesAmount,
                CategoryId = entryCreateRequest.CategoryId,
                Date = entryCreateRequest.Date,
                UserId = entryCreateRequest.UserId
            };
        }

        public Entry Map(EntryEditRequest entryEditRequest)
        {
            return new Entry
            {
                CaloriesAmount = entryEditRequest.CaloriesAmount,
                CategoryId = entryEditRequest.CategoryId,
                Date = entryEditRequest.Date,
                Description = entryEditRequest.Description,
                Id = entryEditRequest.Id,
                UserId = entryEditRequest.UserId
            };
        }

        public EntryEditRequest MapEntryEditRequest(Entry entry)
        {
            return new EntryEditRequest
            {
                CaloriesAmount = entry.CaloriesAmount,
                CategoryId = entry.CategoryId,
                Date = entry.Date,
                Description = entry.Description,
                Id = entry.Id,
                UserId = entry.UserId
            };
        }

        public IndexItemEntryViewModel Map(Entry entry)
        {
            return new IndexItemEntryViewModel()
            {
                CaloriesAmount = entry.CaloriesAmount,
                Color = entry.Category?.Color,
                Date = entry.Date,
                CategoryId = entry.CategoryId,
                Description = entry.Description,
                UserId = entry.UserId,
                Id = entry.Id
            };
        }

        public void Map(Entry entry, EntryViewModel entryViewModel)
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
