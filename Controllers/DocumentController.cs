using DocumentSharingSystem.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DocumentSharingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly DocumentService _documentService;
        private readonly TokenService _tokenService;
        public DocumentController(DocumentService documentService,TokenService tokenService)
        {
            _documentService = documentService;
            _tokenService = tokenService;
        }



        [HttpPost("UploadDocument")]
        public async Task<IActionResult> UploadDocument(IFormFile file)
        {
            var id = await _documentService.SaveFileAsync(file);
            return Ok(new { fileId = id });
        }


        [HttpGet("GetAllTokens")]
        public async Task<IActionResult> GetAllTokens()
        {
            var tokens = await _tokenService.GetAllTokens();

            return Ok(tokens);
        }

        [HttpPost("CreateToken")]
        public async Task<IActionResult> CreateToken([FromQuery] string documentId)
        {
            var token = await _tokenService.CreateTokenAsync(documentId,DateTime.UtcNow.AddMinutes(10));

            return Ok(new { token });
        }

        [HttpPost("RevokeToken")]
        public async Task<IActionResult> RevokeToken([FromQuery] string tokenId)
        {
            var token = await _tokenService.RevokeTokenAsync(tokenId);

            return Ok(new { token });
        }




        [HttpGet("DownloadDocument")]
        public async Task<IActionResult> DownloadDocument([FromQuery] string token)
        {
            var valid = await _tokenService.ValidateTokenAsync(token);

            if (valid == null)
                return Unauthorized();

            var file = valid.Document;

            if (!System.IO.File.Exists(file.FilePath))
                return NotFound();

            var stream = new FileStream(file.FilePath, FileMode.Open, FileAccess.Read);

            return File(stream, file.ContentType, file.FileName);
        }

    }
}
