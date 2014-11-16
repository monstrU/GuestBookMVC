using System.Web.Mvc;
using Moq;

namespace TestBook.FakedObject
{
    public class TestContextController : Controller
    {
        public TestContextController()
        {
            ControllerContext = (new Mock<ControllerContext>()).Object;
        } 

        public bool TestTryValidateModel(object model)
        {
            return TryValidateModel(model);
        }
    }
}
