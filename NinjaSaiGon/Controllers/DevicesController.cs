using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NinjaSaiGon.Data;
using NinjaSaiGon.Data.Models;

namespace NinjaSaiGon.Admin.Controllers
{
    [Authorize(Policy = "Admin")]
    public class DevicesController : Controller
    {
        private readonly ApplicationDbContext _context;
        

        public DevicesController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,PushEndpoint,PushP256DH,PushAuth")] Device device)
        {
            if (ModelState.IsValid)
            {
                if (_context.Devices.All(d => d.PushEndpoint != device.PushEndpoint))
                {
                    _context.Add(device);
                    await _context.SaveChangesAsync();
                    return Json("saved");
                }
                return Json("existed");
            }

            return Json("not ok");
        }
        
    }
}