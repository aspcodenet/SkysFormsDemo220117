namespace SkysFormsDemo.Services;

public interface IPersonSearchProvider
{
    (IEnumerable<int> ids, int totalPages) Search(string q, string sortOrder, string sortColumn, int page);
}