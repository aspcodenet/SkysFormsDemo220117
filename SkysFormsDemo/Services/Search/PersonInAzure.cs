using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;

namespace SkysFormsDemo.Services;

public class PersonInAzure
{
    [SimpleField(IsKey = true, IsFilterable = true)]
    public string Id { get; set; }

    [SearchableField(IsSortable = true)]
    public string Namn { get; set; }

    [SearchableField(IsSortable = true)]
    public string Email { get; set; }

    [SearchableField(IsSortable = true)]
    public string City { get; set; }



    //Personbeskrivning mwed vanlig text - stol/stolar osv sov

    [SearchableField(AnalyzerName = LexicalAnalyzerName.Values.SvLucene)]
    public string Description { get; set; }

}