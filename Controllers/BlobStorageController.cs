using Microsoft.AspNetCore.Mvc;
using WebApplication1.Services;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace WebApplication1.Controllers;

public class BlobStorageController : Controller
{
    private readonly BlobStorageService _blobStorageService;

    public BlobStorageController(BlobStorageService blobStorageService)
    {
        _blobStorageService = blobStorageService;
    }

    [HttpPost]
    // Action to upload a file to the container
    public async Task<IActionResult> UploadFile(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return Content("File not selected");
        }

        await using var stream = file.OpenReadStream();
        await _blobStorageService.UploadFileAsync("jacquesblobcontainer", file.FileName, stream);

        return RedirectToAction("Multimedia"); 
    }

    // Action to list all files in the container
    public async Task<IActionResult> Multimedia()
    {
        var blobs = await _blobStorageService.ListBlobsAsync("jacquesblobcontainer");
        // return the list of blobs to the view from file path
        return View("~/Views/Home/Multimedia.cshtml", blobs);
    }

    // Action to download a file from the container
    public async Task<IActionResult> DownloadFile(string fileName)
    {
        var stream = await _blobStorageService.DownloadFileAsync("jacquesblobcontainer", fileName);
        if (stream == null)
        {
            return NotFound();
        }

        return File(stream, "application/octet-stream", fileName);
    }

    // Action to delete a file from the container
    public async Task<IActionResult> DeleteFile(string fileName)
    {
        await _blobStorageService.DeleteFileAsync("jacquesblobcontainer", fileName);
        return RedirectToAction("Multimedia"); 
    }
}