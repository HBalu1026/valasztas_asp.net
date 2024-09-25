using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;
using System.Net;
using valasztas.Modells;

namespace valasztas.Pages
{
    public class AdatokFeltolteseFajlbolModel : PageModel
    {
        public IWebHostEnvironment _env {  get; set; }
        public ValasztasDbContext _context { get; set; }
        public AdatokFeltolteseFajlbolModel(IWebHostEnvironment env, ValasztasDbContext context)
        {
            _env = env;
            _context = context;
        }

        [BindProperty]
        public IFormFile UploadFile { get; set; }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var UploadFilePath = Path.Combine(_env.ContentRootPath, "upload", UploadFile.FileName);
            using (var stream = new FileStream(UploadFilePath, FileMode.Create)) 
            {
                await UploadFile.CopyToAsync(stream);
            }

            StreamReader sr = new StreamReader(UploadFilePath);
            while (!sr.EndOfStream)
            {
                var sor = sr.ReadLine();
                var elemek = sor.Split();
                Jelolt ujJelolt = new Jelolt();
                Part ujPart;
                if (!_context.Partok.Select(x => x.RovidNev).Contains(elemek[4]))
                {
                    ujPart = new Part();
                    ujPart.RovidNev = elemek[4];
                    _context.Partok.Add(ujPart);
                }
                else
                {
                    ujPart = _context.Partok.Where(x => x.RovidNev == elemek[4]).First();
                }
                ujJelolt.Kerulet = int.Parse(elemek[0]);
                ujJelolt.SzavazatokSzama = int.Parse(elemek[1]);
                ujJelolt.Nev = elemek[2] + " " + elemek[3];
                ujPart.RovidNev = elemek[4];
                ujJelolt.Part = ujPart;
                _context.JeloltekListaja.Add(ujJelolt);
            }
            sr.Close();
            _context.SaveChanges();
            return Page();
        }
    }
}
