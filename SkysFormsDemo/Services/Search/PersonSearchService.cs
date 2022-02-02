using Azure.Search.Documents.Indexes.Models;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Razor.Language.Intermediate;
using SkysFormsDemo.Data;

namespace SkysFormsDemo.Services;

class PersonSearchService : IPersonSearchService
{
    private readonly ApplicationDbContext _context;
    private readonly IPersonSearchProvider _searchProvider;

    public PersonSearchService(ApplicationDbContext context, IPersonSearchProvider searchProvider)
    {
        _context = context;
        _searchProvider = searchProvider;
    }
    public IPersonSearchService.PersonSearchServiceResult Search(string q, string sortOrder, string sortColumn, int page)
    {
         var (ids, totalPages) = _searchProvider.Search(q, sortOrder, sortColumn, page);
         var result = new IPersonSearchService.PersonSearchServiceResult();
         result.Page = page;
         result.TotalPages = totalPages;
         result.Items = _context.Person
             .Where(e => ids.Contains(e.Id))
             .Select(e => new IPersonSearchService.PersonSearchServiceResult.Item
             {
                 Email = e.Email,
                 City = e.City,
                 Name = e.Name,
                 Id = e.Id
             }).ToList();
         result.Items = ids.Select(x => result.Items.First(r => r.Id == x)).ToList();
        return result;

    }
}