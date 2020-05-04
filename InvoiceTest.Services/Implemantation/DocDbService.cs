using InvoiceTest.DomainModel;
using InvoiceTest.Entity;
using InvoiceTest.Services.Interface;
using System.Collections.Generic;
using System.Linq;

namespace InvoiceTest.Services.Implementation
{
    public class DocDbService :  IDocService
    {
        public IList<ClientDocModel> GetClientDocs(string searchPattern)
        {
            if (string.IsNullOrEmpty(searchPattern))
            {
                return new List<ClientDocModel>();
            }

            using (InvoiceDocEntities context = new InvoiceDocEntities())
            {
                  IQueryable<Account> accounts = context.Accounts.Where(a => a.Number.Contains(searchPattern)
                    || a.Client.Name.Contains(searchPattern)                
                );                              

                var clientGroups = from a in accounts
                                   group a by a.ClientId;

                List<ClientDocModel> clientDocs = new List<ClientDocModel>();

                foreach (var g in clientGroups)
                {
                    ClientDocModel clientDocModel = new ClientDocModel();
                    clientDocModel.ClientName = g.Select(s => s.Client.Name).First();
                    clientDocModel.Docs = new List<DocModel>();

                    List<DocModel> docsModel = new List<DocModel>();
                    foreach (var d in g)
                    {
                        docsModel.Add(new DocModel()
                        {
                            DocName = d.Number,                           
                            AccountId = d.AccountId,
                            ClientId = d.ClientId
                        });

                    }
                    clientDocModel.Docs = docsModel;
                    clientDocs.Add(clientDocModel);
                }

                return clientDocs;
            }
        }        
        public IList<string> SaveDocAmount(int accountId, int clientId, decimal amount)
        {
            using (InvoiceDocEntities context = new InvoiceDocEntities())
            {
                List<string> errors = new List<string>();

                if (context.Accounts.Any(a => a.AccountId == accountId) == false)
                {
                    errors.Add(string.Format("Нет счёта с AccountId={0}", accountId));
                }

                if (context.Clients.Any(c => c.ClientId == clientId) == false)
                {
                    errors.Add(string.Format("Нет клиента с ClientId={0}", clientId));
                }

                if (amount < 0)
                {
                    errors.Add("Сумма должна быть больше либо равна 0");
                }

                if (errors.Count > 0) return errors;

                context.Docs.Add(new Doc()
                {
                    ClientId = clientId,
                    AccountId = accountId,
                    Amount = amount
                });

                context.SaveChanges();

                return errors;
            }
        }
    }
}
