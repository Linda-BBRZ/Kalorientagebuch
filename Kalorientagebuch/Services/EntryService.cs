using Kalorientagebuch.Models;
using Kalorientagebuch.Interfaces;
using System.Text.Json;
using Kalorientagebuch.Models.Requests;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Kalorientagebuch.Services
{
    public class EntryService : IEntryService
    {
        public EntryCreateRequest GetProductInfosByBarcode(string barcode)
        {
            OpenFoodFactsResponse openFoodFactsResponse = callAPI(barcode).Result;
            EntryCreateRequest entryCreateRequest = new EntryCreateRequest();

            var kcal = openFoodFactsResponse.Product.Nutriments.EnergyKcalServing;
            var kcal100g = openFoodFactsResponse.Product.Nutriments.EnergyKcal100g;

            entryCreateRequest.CaloriesAmount = Int32.Parse(kcal.ToString());
            entryCreateRequest.Description = string.Join(" ", [openFoodFactsResponse.Product.Brands, openFoodFactsResponse.Product.ProductNameDe]);
            entryCreateRequest.Date = DateTime.Now;
            entryCreateRequest.Barcode = barcode;
            entryCreateRequest.Categories = openFoodFactsResponse.Product.AdditionalData != null && openFoodFactsResponse.Product.AdditionalData.ContainsKey("categories_tags")
                ? new SelectList(openFoodFactsResponse.Product.AdditionalData["categories_tags"].EnumerateArray().Select(c => c.GetString()).Where(c => c != null).Select(c => new { Id = c, Caption = c }).ToList(), "Id", "Caption")
                : new SelectList(Enumerable.Empty<object>(), "Id", "Caption");

            return entryCreateRequest;
            
        }

        private async Task<OpenFoodFactsResponse> callAPI(string barcode)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.UserAgent.TryParseAdd("Kalorientagebuch - C# - <ITN343533@qualifizierung.at>");
                var response = await httpClient.GetAsync($"https://de.openfoodfacts.org/api/v2/product/{barcode}");
                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var productResponse = JsonSerializer.Deserialize<OpenFoodFactsResponse>(jsonResponse);
                    return productResponse;
                }
                else
                {
                    throw new Exception($"Fehler beim Abrufen des Produkts: {response.ReasonPhrase}");
                }
            }
        }

    }
}
