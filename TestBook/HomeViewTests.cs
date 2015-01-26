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
            service.Setup(s2 => s2.LoadMessagesInBook(bookId)).Returns(new List<MessageModel>
            {
                new MessageModel() { MessageId = 1, Body = "message 1"},
                new MessageModel() { MessageId = 2, Body = "message 2"}
            });

            var home = new HomeController(service.Object);
            var result = home.Index();
            Assert.IsNotNull(result);
            
        }
    }
}
