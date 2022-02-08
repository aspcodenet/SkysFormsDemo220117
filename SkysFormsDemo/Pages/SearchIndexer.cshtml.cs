using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SkysFormsDemo.Data;
using SkysFormsDemo.Services;

namespace SkysFormsDemo.Pages
{
    public class SearchIndexerModel : PageModel
    {
        private readonly ApplicationDbContext context;
        private readonly IPersonSearchIndexer personSearchIndexer;

        public SearchIndexerModel(ApplicationDbContext context, IPersonSearchIndexer personSearchIndexer)
        {
            this.context = context;
            this.personSearchIndexer = personSearchIndexer;
        }
        public void OnGet()
        {
            personSearchIndexer.Index();
        }
    }
}
