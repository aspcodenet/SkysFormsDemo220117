using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SkysFormsDemo.Data;
using SkysFormsDemo.Pages.Person;
using SkysFormsDemo.ViewModels;

namespace SkysFormsDemo.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ApplicationDbContext _context;


        public List<ProductViewModel> NewItems { get; set; }
        public List<ProductViewModel> OldestItems { get; set; }

        public IndexModel(ILogger<IndexModel> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public void OnGet()
        {
            NewItems = _context.Products.OrderByDescending(e => e.Created).Take(5)
                .Select(e => new ProductViewModel
                {
                    Id = e.Id,
                    Color = e.Color,
                    Ean13 = e.Ean13,
                    Name = e.Name,
                    Price = e.Price
                }).ToList();

            OldestItems = _context.Products.OrderBy(e => e.Created).Take(5)
                .Select(e => new ProductViewModel
                {
                    Id = e.Id,
                    Color = e.Color,
                    Ean13 = e.Ean13,
                    Name = e.Name,
                    Price = e.Price
                }).ToList();

        }
    }
}