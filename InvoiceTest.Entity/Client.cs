//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace InvoiceTest.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class Client
    {
        public Client()
        {
            this.Accounts = new HashSet<Account>();
            this.Docs = new HashSet<Doc>();
        }
    
        public int ClientId { get; set; }
        public string Name { get; set; }
    
        public virtual ICollection<Account> Accounts { get; set; }
        public virtual ICollection<Doc> Docs { get; set; }
    }
}
