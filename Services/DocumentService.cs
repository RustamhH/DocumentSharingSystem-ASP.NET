using DocumentSharingSystem.Context;
using DocumentSharingSystem.Models;

namespace DocumentSharingSystem.Services
{
    public class DocumentService
    {
        private readonly IWebHostEnvironment _env;
        private readonly AppDBContext _db;

        public DocumentService(IWebHostEnvironment env, AppDBContext db)
        {
            _env = env;
            _db = db;
        }

        public async Task<string> SaveFileAsync(IFormFile file)
        {
            var uploads = Path.Combine(_env.ContentRootPath, "Storage", "uploads");
            Directory.CreateDirectory(uploads);

            var fileId = Guid.NewGuid().ToString();
            var path = Path.Combine(uploads, fileId);

            using var stream = new FileStream(path, FileMode.Create);
            await file.CopyToAsync(stream);

            var entity = new Document
            {
                FileName = file.FileName,
                FilePath = path,
                ContentType = file.ContentType,
                FileSize = file.Length
            };

            _db.Documents.Add(entity);
            await _db.SaveChangesAsync();

            return entity.Id;
        }
    }
}
