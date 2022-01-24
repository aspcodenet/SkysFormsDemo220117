using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SkysFormsDemo.Data;
using SkysFormsDemo.Infrastructure.Paging;

namespace SkysFormsDemo.Pages.Person
{
    public class ViewModel : PageModel
    {
        public string Name { get; set; }
        public int Id { get; set; }


        public class Item
        {
            public int Id { get; set; }
            public string Vin { get; set; }
            public string Manufacturer { get; set; }
            public string Model { get; set; }
            public string Type { get; set; }
            public string Fuel { get; set; }
            public DateTime BoughtDate { get; set; }
        }

        private readonly ApplicationDbContext _context;

        public ViewModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGetFetchValue(int id)
        {
            return new JsonResult(new {value = id * 1000});
        }

        public void OnGet(int personId)
        {
            Id = personId;
            var person = _context.Person.First(person => person.Id == personId);
            Name = person.Name;
        }

        public IActionResult OnGetFetchMore(int personId, int pageNo)
        {

            var list = _context.Person
                .Where(e => e.Id == personId)
                .SelectMany(e => e.OwnedCars)
                .OrderBy(e => e.BoughtDate)
                .GetPaged(pageNo, 5).Results
                .Select(e => new Item
                {
                    BoughtDate = e.BoughtDate,
                    Id = e.Id,
                    Model = e.Model,
                    Fuel = e.Fuel,
                    Manufacturer = e.Manufacturer,
                    Type = e.Type,
                    Vin = e.Vin


                }).ToList();

            return new JsonResult(new {items = list});
        }
    }
}


