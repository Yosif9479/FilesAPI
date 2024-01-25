using FilesAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        DatabaseContext _databaseContext;

        public FilesController(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        [HttpPost("Upload")]
        public IActionResult UploadFile([FromBody] string fileName)
        {
            string path = Directory.GetCurrentDirectory() + "/Files/Storage/" + fileName;

            FileInfo fileInfo = new FileInfo(path);
            FileModel file = new FileModel();

            file.Data = System.IO.File.ReadAllBytes(path);
            file.Name = fileInfo.Name;
            file.Extension = fileInfo.Extension;

            _databaseContext.Files.Add(file);
            _databaseContext.SaveChanges();

            return Ok("File Saved!");
        }

        [HttpGet("Name:{fileName}")]
        public IActionResult DownloadFile(string fileName) 
        {
            string path = Directory.GetCurrentDirectory() + "/Files/Downloads/" + fileName;
            FileModel? file = _databaseContext.Files.FirstOrDefault(c => c.Name == fileName);

            if (file == null) 
            {
                return NotFound();
            }

            System.IO.File.WriteAllBytes(path, file.Data);

            return Ok(file);
        }
    }
}
