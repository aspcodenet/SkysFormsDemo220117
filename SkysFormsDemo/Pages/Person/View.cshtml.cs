using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SkysFormsDemo.Data;

namespace SkysFormsDemo.Pages.Person
{
    public class ViewModel : PageModel
    {
        public string Name { get; set; }
        public int Id { get; set; }

        public List<Item> Items { get; set; }

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

        public void OnGet(int personId)
        {
            Id = personId;
            var person = _context.Person.Include(e => e.OwnedCars).First(person => person.Id == personId);
            Name = person.Name;
            Items = person.OwnedCars.Select(e => new Item
            {
                BoughtDate = e.BoughtDate,
                Id = e.Id,
                Model = e.Model,
                Fuel = e.Fuel,
                Manufacturer = e.Manufacturer,
                Type = e.Type,
                Vin = e.Vin


            }).ToList();

        }

    }
}
