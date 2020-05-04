using InvoiceTest.Services.Implementation;
using InvoiceTest.Services.Interface;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InvoiceTest.Web
{
    public static class IoC
    {
        public static IContainer Initialize()
        {

            ObjectFactory.Initialize(x =>
            {
                x.For<IDocService>().Use<DocDbService>();
            });

            return ObjectFactory.Container;
        }
    }
}