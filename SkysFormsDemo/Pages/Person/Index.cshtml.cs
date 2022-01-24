using MessagePack.Formatters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SkysFormsDemo.Data;
using SkysFormsDemo.Infrastructure.Paging;
using SkysFormsDemo.Services;

namespace SkysFormsDemo.Pages.Person
{
    public class IndexModel : PageModel
    {
        private readonly IPersonService _personService;
        private readonly ApplicationDbContext _context;
        public List<PersonViewModel> Persons { get; set; }

        [BindProperty(SupportsGet = true)]
        public string q { get; set; }
        [BindProperty(SupportsGet = true)]
        public ExtensionMethods.QuerySortOrder sortOrder { get; set; }
        [BindProperty(SupportsGet = true)]
        public string sortColumn { get; set; }


        [BindProperty(SupportsGet = true)]
        public int pageno { get; set; }
        
        public class PersonViewModel
        {
            public int Id { get; set; }       
            public string Name { get; set; }
            public string City { get; set; }
            public string Email { get; set; }
        }

        public IndexModel(IPersonService personService, ApplicationDbContext context)
        {
            _personService = personService;
            _context = context;
        }

        public IActionResult OnGetFetchInfo(int id)
        {
            var person = _context.Person.Include(e => e.OwnedCars).First(e => e.Id == id);
            return new JsonResult(new { 
                namn = person.Name, 
                antalBilar = person.OwnedCars.Count });

        }

        public void OnGet()
        {
            var query = _context.Person.AsQueryable();
            if (!string.IsNullOrEmpty(q))
                query = query.Where(e => e.Name.Contains(q) || e.City.Contains(q));

            if (string.IsNullOrEmpty(sortColumn)) sortColumn = "Name";
            if (pageno == 0) pageno = 1;
            query = query.OrderBy(sortColumn, sortOrder);
            var result = query.GetPaged(pageno, 20);

            PageCount = result.PageCount;

            Persons = result.Results.Select(e => new PersonViewModel
            {
                City = e.City,
                Name = e.Name,
                Email = e.Email,
                Id = e.Id
            }).ToList();
        }

        public int PageCount { get; set; }
    }
}