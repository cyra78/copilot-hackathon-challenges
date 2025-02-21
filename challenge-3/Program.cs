using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

public class Product
{
    [JsonPropertyName("id")]
    public string Id { get; set; }
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("description")]
    public string Description { get; set; }
    [JsonPropertyName("price")]
    public decimal Price { get; set; }
}

class Program
{
    static void Main(string[] args)
    {
        string jsonFilePath = "sampledata.json";
        string json = File.ReadAllText(jsonFilePath);

        List<Product> products = JsonSerializer.Deserialize<List<Product>>(json);

        foreach (var product in products)
        {
            Console.WriteLine($"ID: {product.Id}, Name: {product.Name}, Description: {product.Description}, Price: {product.Price}");
        }
    }
}
