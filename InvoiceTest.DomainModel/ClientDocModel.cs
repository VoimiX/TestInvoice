using System.Collections.Generic;

namespace InvoiceTest.DomainModel
{
    public class ClientDocModel
    {
        public string ClientName { get; set; }      

        public IEnumerable<DocModel> Docs { get; set; }
    }
}
