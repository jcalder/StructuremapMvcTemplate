using System;
using System.Web;
using System.Web.WebPages;
using StructureMap;

namespace BlankMvc.StructureMap
{
    public static class StructureMapMvcConfiguration
    {
        private static IContainer _container;
        public static void SetParentContainer(IContainer parentContainer)
        {
            if (parentContainer == null)
            {
                throw new ArgumentNullException("parentContainer");
            }
            if (parentContainer.Role != ContainerRole.Root)
            {
                throw new ArgumentException("parentContainer may only be a root container, it cannot be a nested or child/profile container");
            }
            _container = parentContainer;
        }

        public static IContainer GetNestedContainer(this HttpContextBase context)
        {
            if(_container == null) throw new InvalidOperationException("You cannot retrieve a nested container if the root container is never set.");
            
            var nestedContainer = context.Items[HttpContextItemKeys.NestedContainerKey] as IContainer;
            if (nestedContainer != null) return nestedContainer;

            nestedContainer = _container.GetNestedContainer();
            context.RegisterForDispose(nestedContainer);
            context.Items[HttpContextItemKeys.NestedContainerKey] = nestedContainer;
            return nestedContainer;
        }

        /// <summary>
        /// Mostly for testing, static context makes negative test 
        /// cases succeed if they run after positive test cases.
        /// </summary>
        public static void Reset()
        {
            _container = null;
        }
    }
}