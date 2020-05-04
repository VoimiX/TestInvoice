using InvoiceTest.Services.Interface;
using System.Web.Mvc;

namespace InvoiceTest.Web.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly IDocService _docService;
              

        public InvoiceController(IDocService docService)
        {
            this._docService = docService;
        }


        // GET: Invoice
        public ActionResult Index()
        { 
            return View();
        }

        [HttpPost]
        public ActionResult Search(string input)
        {                   
            var clientDocs = _docService.GetClientDocs(input);                     
            if (clientDocs.Count > 0)
            {
                return PartialView("_Result", clientDocs);
            }

            return PartialView("_NothingFound");
        }

        [HttpPost]
        public ActionResult Add(int clientId, int accountId, decimal amount)
        {
            var errors = _docService.SaveDocAmount(accountId, clientId, amount);

            return Json(errors);
        }
    }
}