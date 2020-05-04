using InvoiceTest.DomainModel;
using System.Collections.Generic;

namespace InvoiceTest.Services.Interface
{
    public interface IDocService
    {
        IList<ClientDocModel> GetClientDocs(string searchPattern);
        IList<string> SaveDocAmount(int accountId, int clientId, decimal amount);
    }
}
