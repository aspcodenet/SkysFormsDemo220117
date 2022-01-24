using System.Linq;
using AutoMapper;
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
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;


        public List<ProductViewModel> NewItems { get; set; }
        public List<ProductViewModel> OldestItems { get; set; }

        public IndexModel(ILogger<IndexModel> logger,
            IMapper mapper,
            ApplicationDbContext context)
        {
            _logger = logger;
            _mapper = mapper;
            _context = context;
        }

        public void OnGet()
        {
            //var prod = _context.Products.First();
            //var productViewModel = _mapper.Map<ProductViewModel>(prod);

            ////Onpost - ON EDIT kommer in NYA värden i productViewModel
            ////Uppdatera befiontlig = skicka in befintlig prod som parameter 2 
            //_mapper.Map(productViewModel,prod);
            //_context.SaveChanges();

            ////Onpost - ON NEW kommer in NYA värden i productViewModel
            //var product = new Product();
            //_mapper.Map(productViewModel, product);
            //_context.Products.Add(product);
            //_context.SaveChanges();



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