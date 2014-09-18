using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using DomainModel;
using FacadeServices;
using FacadeServices.Interfaces;
using GuestBook.ModelView;


namespace GuestBook.Controllers
{
    public partial class HomeController : Controller
    {
        public IGuestBookService DataService { get; set; }

        public HomeController(IGuestBookService service)
        {
            DataService = service;
        }

        public virtual ActionResult Index(Nullable<int> id)
        {
            var book = DataService.GetDefaultGuestBook(id);
            var messages = DataService.LoadMessagesInBook(book.GuestBookId);
            return View(MVC.Home.Views.Index, new MessagesInGuestBooksModelview()
            {
                Messages = messages,
                Book = book
            });
            
        }

        public virtual ActionResult GuestBooksList(Nullable<int> id)
        {
            var def = DataService.GetDefaultGuestBook(id);
            var books = DataService.LoadGuestBooks();
            return View(MVC.Home.Views.GuestBooksList, new GuestBooksOnCardModelView()
            {
                GuestBooks = books,
                SelectedGuestBookId = def.GuestBookId
            });

        }
        [HttpPost]
        public virtual ActionResult Edit( MessageModel message)
        {
            bool isValid = true;
            if (!ModelState.IsValidField("Title"))
            {
                ModelState.AddModelError("Title", "Необходимо ввести заголовок сообщения");
                isValid = false;
            }

            if (!ModelState.IsValidField("Body"))
            {
                ModelState.AddModelError("Body", "Необходимо ввести сообщение");
                isValid = false;
            }

            if (isValid)
            {
                
                try
                {
                    DataService.AddMessage(message);
                    return RedirectToAction(MVC.Home.ActionNames.Index, MVC.Home.Name, new {id=message.GuestBook.GuestBookId});
                }
                catch (Exception e)
                {
                    ModelState.AddModelError(string.Empty, e);
                }
                
            }
            var gbooks = DataService.LoadGuestBooks();
            return View(MVC.Home.Views.Edit, new EditMessageModelView()
            {
                Message = message,
                GuestBooks = gbooks
            });
        }

        [HttpGet]
        public virtual ActionResult Edit()
        {
            var gbooks = DataService.LoadGuestBooks();
            EditMessageModelView card=new EditMessageModelView();
        
            
            card.GuestBooks = gbooks;
            
            return View(MVC.Home.Views.Edit, card);
        }

        public virtual ActionResult About()
        {
            return PartialView(MVC.Home.Views.About);
        }
    }
}
