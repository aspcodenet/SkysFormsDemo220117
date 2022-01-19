using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using SkysFormsDemo.Data;
using SkysFormsDemo.Infrastructure.Validation;
using SkysFormsDemo.Services;

namespace SkysFormsDemo.Pages.Person
{
    [BindProperties]
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        [MaxLength(100)]
        [Required]
        public string Namnet { get; set; }

        [StringLength(100)]
        public string StreetAddress { get; set; }

        [StringLength(10)]
        public string PostalCode { get; set; }


        [Range(0, 100000, ErrorMessage = "Skriv ett tal mellan 0 och 100000")]
        public decimal Salary { get; set; }


        [Range(0, 100)]
        public int CarCount { get; set; } //Krysta fram ett int-usecase

        [StringLength(50)]
        [Required]
        public string City { get; set; }

        [StringLength(150)]
        [EmailAddress]
        public string Email { get; set; }


        [StringLength(150)]
        [EmailAddress]
        //[Compare("Email")]
        [Compare(nameof(Email))]
        public string EmailAgain { get; set; }
        

        public int CountryId { get; set; }
        public List<SelectListItem> Countries { get; set; }

        [GoodYear(ErrorMessage = "Varken Stefan eller hans fru f�ddes det �ret")]
        public int GoodYear { get; set; }

        public EditModel(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void OnGet(int personId)
        {
            var person = _context.Person
                .Include(e=>e.Country)
                .First(u=>u.Id == personId );

            _mapper.Map(person, this);

            FillCountryList();
        }

        private void FillCountryList()
        {
            Countries = _context.Countries.Select(e => new SelectListItem
            {
                Text = e.Name,
                Value = e.Id.ToString()
            }).ToList();
        }


        public IActionResult OnPost(int personId)
        {
            if (ModelState.IsValid)
            {
                var person = _context.Person.First(u=>u.Id == personId);

                _mapper.Map(this,person);
                person.Country = _context.Countries.First(e => e.Id == CountryId);

                //person.Name = Namnet;
                //person.CarCount = CarCount;
                //person.City = City;
                //person.Email = Email;
                //person.PostalCode = PostalCode;
                //person.Salary = Salary;
                //person.StreetAddress = StreetAddress;
                _context.SaveChanges();
                return RedirectToPage("Index");
            }

            FillCountryList();
            return Page();
        }


    }
}
