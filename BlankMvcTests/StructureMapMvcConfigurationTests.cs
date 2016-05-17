using System;
using System.IO;
using System.Web;
using BlankMvc.StructureMap;
using NUnit.Framework;
using StructureMap;
using Assert = NUnit.Framework.Assert;

namespace BlankMvcTests
{
    [TestFixture]
    public class StructureMapMvcConfigurationTests
    {
        [TearDown]
        public void Teardown()
        {
            StructureMapMvcConfiguration.Reset();
            _callCount = 0;
        }

        [Test]
        public void Set_should_throw_when_given_null()
        {
            Assert.Throws<ArgumentNullException>(() => StructureMapMvcConfiguration.SetParentContainer(null));
        }

        [Test]
        public void Set_should_throw_when_given_nested_container()
        {
            var container = new Container().GetNestedContainer();
            Assert.Throws<ArgumentException>(() => StructureMapMvcConfiguration.SetParentContainer(container));
        }

        [Test]
        public void Set_should_throw_when_given_child_container()
        {
            var container = new Container().CreateChildContainer();
            Assert.Throws<ArgumentException>(() => StructureMapMvcConfiguration.SetParentContainer(container));
        }

        [Test]
        public void Get_nested_container_should_throw_if_root_container_unset()
        {
            var wrapper = CreateContextWrapper();
            Assert.Throws<InvalidOperationException>(() => wrapper.GetNestedContainer());
        }

        [Test]
        public void Pre_nested_container_request_should_be_called_each_time_we_build_a_new_container()
        {
            var container = new Container();
            StructureMapMvcConfiguration.SetParentContainer(container);
            StructureMapMvcConfiguration.PerNestedConatinerConfiguration(IncrementCount);
            var wrapper = CreateContextWrapper();
            wrapper.GetNestedContainer();
            Assert.AreEqual(1, _callCount);
            var wrapper2 = CreateContextWrapper();
            wrapper2.GetNestedContainer();
            Assert.AreEqual(2, _callCount);
        }

        private static Action<ConfigurationExpression> IncrementCount(HttpContextBase context)
        {
            return (x =>
            {
                _callCount++;
            });
        }


        private static int _callCount;

        private static HttpContextWrapper CreateContextWrapper()
        {
            var context = new HttpContext(new HttpRequest("foo", "http://www.google.com/", ""), new HttpResponse(new StringWriter()));
            var wrapper = new HttpContextWrapper(context);
            return wrapper;
        }

        [Test]
        public void Get_nested_container_should_return_nested_container()
        {
            StructureMapMvcConfiguration.SetParentContainer(new Container());
            var wrapper = CreateContextWrapper();
            Assert.DoesNotThrow(() => wrapper.GetNestedContainer());
        }
    }
}
