using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Kalorientagebuch.Models
{

    [NotMapped]
    public class OpenFoodFactsResponse
    {
        [JsonPropertyName("code")]
        public string? Code { get; set; }
        [JsonPropertyName("status")]
        public int Status { get; set; }
        [JsonPropertyName("status_verbose")]
        public string? StatusVerbose { get; set; }
        [JsonPropertyName("product")]
        public Product? Product { get; set; }
    }

    [NotMapped]
    public class Product
    {
        [JsonPropertyName("code")]
        public string? Code { get; set; }
        [JsonPropertyName("product_name")]
        public string? ProductName { get; set; }
        [JsonPropertyName("product_name_de")]
        public string? ProductNameDe { get; set; }
        [JsonPropertyName("brands")]
        public string? Brands { get; set; }
        [JsonPropertyName("quantity")]
        public string? Quantity { get; set; }
        [JsonPropertyName("serving_size")]
        public string? ServingSize { get; set; }
        [JsonPropertyName("serving_quantity")]
        [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
        public double? ServingQuantity { get; set; }
        [JsonPropertyName("image_url")]
        public string? ImageUrl { get; set; }
        [JsonPropertyName("nutriscore_grade")]
        public string? NutriscoreGrade { get; set; }
        [JsonPropertyName("nutriments")]
        public Nutriments? Nutriments { get; set; }
        // Fängt ungemappte Produkt-Metadaten ab, statt zu crashen
        [JsonExtensionData]
        public Dictionary<string, JsonElement>? AdditionalData { get; set; }
    }

    [NotMapped]
    public class Nutriments
    {
        // Kalorien
        [JsonPropertyName("energy-kcal_100g")]
        [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
        public double? EnergyKcal100g { get; set; }
        [JsonPropertyName("energy-kcal_serving")]
        [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
        public double? EnergyKcalServing { get; set; }
        // Fett & gesättigte Fettsäuren
        [JsonPropertyName("fat_100g")]
        [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
        public double? Fat100g { get; set; }
        [JsonPropertyName("fat_serving")]
        [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
        public double? FatServing { get; set; }
        [JsonPropertyName("saturated-fat_100g")]
        [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
        public double? SaturatedFat100g { get; set; }
        [JsonPropertyName("saturated-fat_serving")]
        [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
        public double? SaturatedFatServing { get; set; }
        // Kohlenhydrate & Zucker
        [JsonPropertyName("carbohydrates_100g")]
        [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
        public double? Carbohydrates100g { get; set; }
        [JsonPropertyName("carbohydrates_serving")]
        [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
        public double? CarbohydratesServing { get; set; }
        [JsonPropertyName("sugars_100g")]
        [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
        public double? Sugars100g { get; set; }
        [JsonPropertyName("sugars_serving")]
        [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
        public double? SugarsServing { get; set; }
        // Ballaststoffe & Eiweiß
        [JsonPropertyName("fiber_100g")]
        [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
        public double? Fiber100g { get; set; }
        [JsonPropertyName("fiber_serving")]
        [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
        public double? FiberServing { get; set; }
        [JsonPropertyName("proteins_100g")]
        [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
        public double? Proteins100g { get; set; }
        [JsonPropertyName("proteins_serving")]
        [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
        public double? ProteinsServing { get; set; }
        // Salz
        [JsonPropertyName("salt_100g")]
        [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
        public double? Salt100g { get; set; }
        [JsonPropertyName("salt_serving")]
        [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
        public double? SaltServing { get; set; }
        // Fängt alle restlichen Nährwert-Felder ab
        [JsonExtensionData]
        public Dictionary<string, JsonElement>? AdditionalNutriments { get; set; }
    }

}
