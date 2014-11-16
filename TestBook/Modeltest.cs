using System;
using System.Activities.Validation;
using DomainModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestBook.FakedObject;

namespace TestBook
{
    [TestClass]
    public class ModelTest
    {
        [TestMethod]
        public void ModelValidateTestOk()
        {
            var model = new MessageModel();
            model.UpdateDate = DateTime.Now.AddDays(1);
            model.Title = "title";
            model.Body = "body";
            TestContextController controller = new TestContextController();
            var result= controller.TestTryValidateModel(model);
            
            Assert.IsTrue(result);

            var state = controller.ModelState;
            Assert.IsTrue(state.IsValid);
            

        }

        [TestMethod]
        public void ModelValidateTestFalse()
        {
            var model = new MessageModel();
            model.UpdateDate = DateTime.Now.AddDays(-1);
            model.Title = "title";
            model.Body = "body";
            TestContextController controller = new TestContextController();
            var result = controller.TestTryValidateModel(model);
            
            Assert.IsFalse(result);
            
            var state = controller.ModelState;
            Assert.IsFalse(state.IsValid);


        }
    }
}
