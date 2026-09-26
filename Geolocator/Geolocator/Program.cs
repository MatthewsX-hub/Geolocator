using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace geolocate
{
    public class Data
    {
        public string? city { get; set; }
        public string? region { get; set; }
        public string? country { get; set; }
        public string? loc { get; set; }
        public string? org { get; set; }
        public string? postal { get; set; }
        public string? timezone { get; set; }
        public string? ip { get; set; }
        public string? hostname { get; set; }
    }

    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.Title = "Geolocator";
            Console.Write("Enter IP Address: ");
            string? ip = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(ip))
            {
                Console.WriteLine("No IP entered.");
                return;
            }

            string url = $"https://ipinfo.io/{ip}/json";

            try
            {
                using HttpClient client = new HttpClient();
                string json = await client.GetStringAsync(url);

                Data? data = JsonSerializer.Deserialize<Data>(json);

                if (data == null)
                {
                    Console.WriteLine("Failed to parse response.");
                    return;
                }

                Console.WriteLine();
                Console.WriteLine("[+] Request Successfully Made");
                Console.WriteLine($"IP:         {data.ip}");
                Console.WriteLine($"Hostname:   {data.hostname}");
                Console.WriteLine($"City:       {data.city}");
                Console.WriteLine($"Region:     {data.region}");
                Console.WriteLine($"Country:    {data.country}");
                Console.WriteLine($"Location:   {data.loc}");
                Console.WriteLine($"Org/ISP:    {data.org}");
                Console.WriteLine($"Postal:     {data.postal}");
                Console.WriteLine($"Timezone:   {data.timezone}");

                if (!string.IsNullOrWhiteSpace(data.loc))
                {
                    Console.WriteLine($"Google Maps: https://www.google.com/maps/?q={data.loc}");
                }
            }
            catch (HttpRequestException)
            {
                Console.WriteLine("Network error — could not reach ipinfo.io");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            Console.WriteLine();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}