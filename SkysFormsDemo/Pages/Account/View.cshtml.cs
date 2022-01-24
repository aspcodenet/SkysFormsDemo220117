using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SkysFormsDemo.Services;

namespace SkysFormsDemo.Pages.Account
{
    //[ResponseCache(Duration=30)]
    public class ViewModel : PageModel
    {
        private readonly IAccountService _accountService;

        public string Konto { get; set; }
        public DateTime DateWhen { get; set; }

        public ViewModel(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public void OnGet(int accountId )
        {
            DateWhen = DateTime.Now;
            Konto = accountId.ToString();
        }



    }
}
