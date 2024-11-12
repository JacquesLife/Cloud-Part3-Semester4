using System;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Services;

namespace WebApplication1.Controllers;

public class FileShareController : Controller
{
    private readonly FileShareService _fileShareService;

    public FileShareController(FileShareService fileShareService)
    {
        _fileShareService = fileShareService;
    }

    [HttpPost]
    // Action to upload a file to the directory
    public async Task<IActionResult> UploadFile(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return Content("File not selected");
        }

        await using var stream = file.OpenReadStream();
        await _fileShareService.UploadFileAsync("jacquesfileshare", file.FileName, stream);

        return RedirectToAction("FileShare");
    }

    // Action to list all files in the directory
    public async Task<IActionResult> FileShare()
    {
        var files = await _fileShareService.ListFilesAsync("jacquesfileshare");
        // return the list of files to the view from file path
        return View("~/Views/Home/FileShare.cshtml", files);
    }

    // Action to download a file from the directory
    public async Task<IActionResult> DownloadFile(string fileName)
    {
        var stream = await _fileShareService.DownloadFileAsync("jacquesfileshare", fileName);
        if (stream == null)
        {
            return NotFound();
        }

        return File(stream, "application/octet-stream", fileName);
    }

    // Action to delete a file from the directory
    public async Task<IActionResult> DeleteFile(string fileName)
    {
        await _fileShareService.DeleteFileAsync("jacquesfileshare", fileName);
        return RedirectToAction("FileShare");
    }
}
