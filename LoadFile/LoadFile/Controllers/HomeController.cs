
using System.IO;
using System.Threading.Tasks;
using LoadFile.Database;
using LoadFile.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace LoadFile.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HomeController(ApplicationContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Files.ToListAsync());
        }

        public async Task<IActionResult> AddFiles(IFormFileCollection uploads)
        {
            foreach (IFormFile uploadedFile in uploads)
            {
                string path = "/Files/" + uploadedFile.FileName;
                await using (var fileStream = new FileStream(_webHostEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }
                
                FileModel file = new FileModel
                {
                    Name = uploadedFile.FileName, 
                    Path = path
                };
                
                await _context.Files.AddAsync(file);
            }
            
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}