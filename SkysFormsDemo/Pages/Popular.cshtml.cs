using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SkysFormsDemo.Data;
using SkysFormsDemo.ViewModels;

namespace SkysFormsDemo.Pages
{
    public class PopularModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public List<ProductViewModel> Items { get; set; }

        public PopularModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public void OnGet()
        {
            Items = _context.Products.OrderByDescending(e => e.PopularityPercent).Take(5)
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
