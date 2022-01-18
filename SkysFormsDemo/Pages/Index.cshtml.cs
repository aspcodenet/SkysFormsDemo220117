using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SkysFormsDemo.Data;

namespace SkysFormsDemo.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ApplicationDbContext _context;

        public class Item
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Color { get; set; }
            public string Ean13 { get; set; }
            public decimal Price { get; set; }
        }

        public List<Item> NewItems { get; set; }

        public IndexModel(ILogger<IndexModel> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public void OnGet()
        {
            NewItems = _context.Products.OrderByDescending(e => e.Created).Take(5)
                .Select(e => new Item
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