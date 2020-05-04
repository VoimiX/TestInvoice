using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace InvoiceTest.Web
{
    public class StructureMapControllerFactory
     : DefaultControllerFactory
    {
        protected override IController GetControllerInstance(
            RequestContext requestContext, Type controllerType)
        {
            try
            {
                if (controllerType == null)
                    return base.GetControllerInstance(
                        requestContext, controllerType);

                return ObjectFactory.GetInstance(controllerType)
                    as Controller;
            }
            catch (StructureMapException)
            {
              
                throw;
            }
        }
    }

}