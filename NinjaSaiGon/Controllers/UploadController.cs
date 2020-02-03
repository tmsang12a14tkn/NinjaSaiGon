using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http.Extensions;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using NinjaSaiGon.Admin.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace VITCorp.HR.Controllers
{
    [Authorize(Policy = "Admin")]
    public class UploadController
    {
        private readonly IHostingEnvironment _environment;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UploadController(IHttpContextAccessor httpContextAccessor, IHostingEnvironment environment)
        {
            _environment = environment;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost]
        public async Task<JsonResult> UploadPhoto(IList<IFormFile> files)
        {
            try
            {
                var httpRequest = _httpContextAccessor.HttpContext.Request;
                var rndName = string.Empty;

                foreach (IFormFile file in files)
                {
                    if (file != null && file.Length > 0)
                    {
                        int MaxContentLength = 1024 * 1024 * 5; //Size = 5 MB  

                        IList<string> AllowedFileExtensions = new List<string> { ".jpg", ".jpeg", ".gif", ".png" };
                        var ext = Path.GetExtension(file.FileName).ToLower();
                        if (!AllowedFileExtensions.Contains(ext))
                        {
                            return new JsonResult(new { success = false, status = "Please Upload image of type .jpg,.jpeg,.gif,.png." });
                        }
                        else if (file.Length > MaxContentLength)
                        {
                            return new JsonResult(new { success = false, status = "Please Upload a file upto 1 mb." });
                        }
                        else
                        {
                            var year = DateTime.Now.Year; var month = DateTime.Now.Month;
                            var uploadPath = Path.Combine(_environment.WebRootPath, string.Format("uploaded\\{0}\\{1}", year, month));
                            if (!Directory.Exists(uploadPath))
                                Directory.CreateDirectory(uploadPath);
                            var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim();
                            var fileExtension = Path.GetExtension(fileName).ToString();
                            rndName = Guid.NewGuid() + fileExtension;
                            var imagePath = Path.Combine(uploadPath, rndName);
                            using (var fileStream = new FileStream(imagePath, FileMode.Create))
                            {
                                await file.CopyToAsync(fileStream);
                            }
                            
                            var url = new Uri(httpRequest.GetDisplayUrl()).GetLeftPart(UriPartial.Authority) + $"/uploaded/{year}/{month}/" + rndName;
                            var url2 = new Uri(httpRequest.GetDisplayUrl()).GetLeftPart(UriPartial.Authority) + ImageHelper.ScaleImage(_environment.WebRootPath, imagePath, 300);
                            return new JsonResult(new { success = true, status = "Image uploaded successfully.", Url = url, Url2 = url2});
                        }
                    }
                }
                return new JsonResult(new { success = false, status = "Please Upload a image." });
            }
            catch (Exception ex)
            {
                var res = string.Format("some Message:" + ex.Message);
                return new JsonResult(new { success = false, status = res });
            }
        }
    }
}