using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorApp.Client
{
    public class Program
    {
        static string subscriptionKey = ConfigurationHelper.GetConfig("AzureCognitiveKey");
        static string endpoint = ConfigurationHelper.GetConfig("AzureCognitiveEndpoint");

        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            var baseAddress = builder.Configuration["BaseAddress"] ?? builder.HostEnvironment.BaseAddress;
            builder.Services.AddScoped(_ => new HttpClient { BaseAddress = new Uri(baseAddress) });

            await builder.Build().RunAsync();
        }

        // Create a client
        ComputerVisionClient client = Authenticate(endpoint, subscriptionKey);

        // Analyze an image to get features and other properties.
        AnalyzeImageUrl(client, ANALYZE_URL_IMAGE).Wait();


        public static ComputerVisionClient Authenticate(string endpoint, string key)
        {
            ComputerVisionClient client =
              new ComputerVisionClient(new ApiKeyServiceClientCredentials(key))
              { Endpoint = endpoint };
            return client;
        }

        // URL image used for analyzing an image (image of puppy)
        private const string ANALYZE_URL_IMAGE = "https://moderatorsampleimages.blob.core.windows.net/samples/sample16.png";

        public static async Task AnalyzeImageUrl(ComputerVisionClient client, string imageUrl)
        {
            List<VisualFeatureTypes?> features = new List<VisualFeatureTypes?>()
            {
                VisualFeatureTypes.Categories,
                VisualFeatureTypes.Description,
                VisualFeatureTypes.Faces,
                VisualFeatureTypes.ImageType,
                VisualFeatureTypes.Tags,
                VisualFeatureTypes.Adult,
                VisualFeatureTypes.Color,
                VisualFeatureTypes.Brands,
                VisualFeatureTypes.Objects
            };

            // Analyze the URL image 
            ImageAnalysis results = await client.AnalyzeImageAsync(imageUrl, visualFeatures: features);

        }
    }
}