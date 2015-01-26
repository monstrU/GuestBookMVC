using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using DomainModel;
using FacadeServices.Interfaces;
using GuestBook.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace TestBook
{
    [TestClass]
    public class HomeViewTests
    {
        [TestMethod]
        public void IndexViewTest()
        {
            var service = new Mock<IGuestBookService>();
            const int bookId = 1;
            service.Setup(s => s.GetDefaultGuestBook(bookId)).Returns(new GuestBookModel()
            {
                GuestBookId = bookId,
                GuestBookName = "Book name"
            });
            var messageModels = new List<MessageModel>
            {
                new MessageModel() { MessageId = 1, Body = "message 1"},
                new MessageModel() { MessageId = 2, Body = "message 2"}
            };
            service.Setup(s2 => s2.LoadMessagesInBook(bookId)).Returns(messageModels);

            var home = new HomeController(service.Object);
            
            dynamic result = home.Index(1);
            
            Assert.IsNotNull(result);
            var model = result.Model;
            Assert.IsNotNull(model);
            Assert.AreEqual(model.Messages.Count, messageModels.Count);



        }
    }
}
