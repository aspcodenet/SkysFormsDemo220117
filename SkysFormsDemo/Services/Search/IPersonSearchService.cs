namespace SkysFormsDemo.Services;

public interface IPersonSearchService
{
    public PersonSearchServiceResult Search(string q, string sortOrder, string sortColumn, int page);
    public class PersonSearchServiceResult
    {
        public class Item
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string City { get; set; }
            public string Email { get; set; }
        }

        public List<Item> Items { get; set; }

        public int TotalPages { get; set; }

        public int Page { get; set; }
    }

}