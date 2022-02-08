using Azure;
using Azure.Search.Documents;
using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;
using Azure.Search.Documents.Models;
using SkysFormsDemo.Data;

namespace SkysFormsDemo.Services;

public class AzurePersonSearchProvider : IPersonSearchProvider, IPersonSearchIndexer
{
    private readonly ApplicationDbContext _context;
    string indexName = "personerna2";
    private string searchUrl = "https://stefanpersonsearch.search.windows.net";
    private string key = "F4B85AB85B8662E8B66EACDF1B98E581";

    public AzurePersonSearchProvider(ApplicationDbContext context)
    {
        _context = context;
        CreateIndexIfNotExists();
    }




    private void CreateIndexIfNotExists()
    {

        var serviceEndpoint = new Uri(searchUrl);
        var credential = new AzureKeyCredential(key);
        var adminClient = new SearchIndexClient(serviceEndpoint, credential);

        var fieldBuilder = new FieldBuilder();
        var searchFields = fieldBuilder.Build(typeof(PersonInAzure));

        var definition = new SearchIndex(indexName, searchFields);

        adminClient.CreateOrUpdateIndex(definition);
    }

    public (IEnumerable<int> ids, int totalPages) Search(string q, string sortOrder, string sortColumn, int page)
    {
        var searchClient = new SearchClient(new Uri(searchUrl),
            indexName, new AzureKeyCredential(key));
        if (sortColumn == "Name")
            sortColumn = "Namn";
        var searchOptions = new SearchOptions
        {
            OrderBy = { sortColumn + " " + sortOrder.ToLower() },
            Skip = (page -1) * 10,
            Size = 10,
            IncludeTotalCount = true
        };


        var searchResult = searchClient.Search<PersonInAzure>(q, searchOptions);
        int totalPage = (int)Math.Ceiling((double)searchResult.Value.TotalCount / 10);
        return (searchResult.Value.GetResults().Select(e => Convert.ToInt32(e.Document.Id)), totalPage);

    }

    public void Index()
    {
        var searchClient = new SearchClient(new Uri(searchUrl),
            indexName, new AzureKeyCredential(key));

        var batch = new IndexDocumentsBatch<PersonInAzure>();
        foreach (var person in _context.Person)
        {
            //Update or add new in Azure
            var personInAzure = new PersonInAzure
            {
                City = person.City,
                Description = person.StreetAddress + " " + person.Position.ToString(),
                Id = person.Id.ToString(),
                Namn = person.Name,
                Email = person.Email
            };
            batch.Actions.Add(new IndexDocumentsAction<PersonInAzure>(IndexActionType.MergeOrUpload,
                personInAzure));

        }
        var result = searchClient.IndexDocuments(batch);
    }
}