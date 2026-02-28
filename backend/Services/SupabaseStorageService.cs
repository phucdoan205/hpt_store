using Supabase;
using Supabase.Storage;

namespace backend.Services
{
    public class SupabaseStorageService
    {
        private readonly Supabase.Client _client;
        private const string BUCKET = "product-images";

        public SupabaseStorageService(IConfiguration config)
        {
            var url = config["Supabase:Url"];
            var key = config["Supabase:Key"];

            _client = new Supabase.Client(url, key);
            _client.InitializeAsync().Wait();
        }

        public async Task<string> UploadImage(IFormFile file)
        {
            var fileName = $"{Guid.NewGuid()}_{file.FileName}";

            using var stream = file.OpenReadStream();

            await _client.Storage
                .From(BUCKET)
                .Upload(stream, fileName);

            var publicUrl = _client.Storage
                .From(BUCKET)
                .GetPublicUrl(fileName);

            return publicUrl;
        }
    }
}