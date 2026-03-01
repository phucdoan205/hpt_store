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

        public async Task<string> UploadFile(IFormFile file, string folder)
        {
            var fileName = $"{Guid.NewGuid()}_{file.FileName}";
            var path = $"{folder}/{fileName}";

            using var ms = new MemoryStream();
            await file.CopyToAsync(ms);

            var bytes = ms.ToArray();

            await _client.Storage
                .From(BUCKET)
                .Upload(bytes, path);

            return _client.Storage
                .From(BUCKET)
                .GetPublicUrl(path);
        }

        public async Task DeleteFolder(string folder)
        {
            var files = await _client.Storage
                .From(BUCKET)
                .List(folder);

            if (files == null || files.Count == 0)
                return;

            var paths = files
                .Select(x => $"{folder}/{x.Name}")
                .ToList();

            await _client.Storage
                .From(BUCKET)
                .Remove(paths);
        }
    }
}