using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using NinjaSaiGon.Admin.Helpers;
using NinjaSaiGon.Data.Interfaces;

namespace NinjaSaiGon.Admin.Controllers
{
    [Authorize(Policy = "Admin")]
    public class HomeController : Controller
    {
        private readonly IDrinkRepository _drinkRepository;
        private readonly IHostingEnvironment _environment;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HomeController(IDrinkRepository drinkRepository, IHttpContextAccessor httpContextAccessor, IHostingEnvironment environment)
        {
            _drinkRepository = drinkRepository;
            _environment = environment;
            _httpContextAccessor = httpContextAccessor;
        }

        public ViewResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FroalaUploadImage(ICollection<IFormFile> files)
        {
            var httpRequest = _httpContextAccessor.HttpContext.Request;
            var links = new List<string>();
            var year = DateTime.Now.Year;
            var month = DateTime.Now.Month;
            var uploadPath = Path.Combine(_environment.WebRootPath, string.Format("uploaded\\{0}\\{1}", year, month));
            var rndName = string.Empty;

            foreach (var file in files)
            {
                if (file.Length > 0)
                {
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

                    links.Add(url);
                }
            }

            return Json(new { link = links });
        }

        [HttpPost]
        public IActionResult FroalaDelete(string src)
        {
            if (System.IO.File.Exists(src))
            {
                System.IO.File.Delete(src);
            }

            return new EmptyResult();
        }
    }
}
