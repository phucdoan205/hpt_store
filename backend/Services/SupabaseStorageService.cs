using Supabase;
using Microsoft.AspNetCore.Http;

namespace backend.Services
{
    public class SupabaseStorageService
    {
        private readonly Supabase.Client _client;
        private const string BUCKET = "products";

        public SupabaseStorageService(IConfiguration config)
        {
            var url = config["Supabase:Url"];
            var key = config["Supabase:Key"];

            _client = new Supabase.Client(url!, key!);
            _client.InitializeAsync().Wait();
        }

        public async Task<string> UploadFile(IFormFile file)
        {
            var fileName = $"{Guid.NewGuid()}_{file.FileName}";

            using var ms = new MemoryStream();
            await file.CopyToAsync(ms);

            var bytes = ms.ToArray();

            await _client.Storage
                .From(BUCKET)
                .Upload(bytes, fileName);

            var publicUrl = _client.Storage
                .From(BUCKET)
                .GetPublicUrl(fileName);

            return publicUrl;
        }
    }
}