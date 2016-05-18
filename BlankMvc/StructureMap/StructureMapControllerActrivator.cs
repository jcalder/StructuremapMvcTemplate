using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;

namespace BlankMvc.StructureMap
{
    public class StructureMapControllerActrivator : IControllerActivator
    {
        public IController Create(RequestContext requestContext, Type controllerType)
        {
            var nestedContainer = requestContext.HttpContext.GetNestedContainer();
            try 
            {
              return nestedContainer.GetInstance(controllerType) as IController;
            }
            catch (Exception e) //cause sm can throw if we don't have a registration               
            {
                return null;
            }
        }
    }
}