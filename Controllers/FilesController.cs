using FilesAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

        [HttpPost]
        public async Task<IActionResult> UploadFile([Bind("formFile")] IFormFile formFile)
        {
            FileModel file = new FileModel();

            using (MemoryStream stream = new MemoryStream())
            {
                await formFile.CopyToAsync(stream);
                file.Name = formFile.FileName;
                file.Extension = formFile.FileName.Split('.')[formFile.FileName.Split('.').Length - 1];
                file.Data = stream.ToArray();
            }

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
