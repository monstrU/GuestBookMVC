using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using DomainModel;
using FacadeServices.Interfaces;

namespace GuestBook.Areas.Admin.Controllers
{
    public partial class EntityController : Controller
    {
        public IGuestBookService DataService { get; set; }

        public EntityController(IGuestBookService service)
        {
            DataService = service;
        }

        public virtual ActionResult Index()
        {
            var gbooks = DataService.LoadGuestBooks();
            return View(MVC.Admin.Entity.Views.Index, gbooks);
        }

        [HttpGet]
        public virtual ActionResult Add()
        {
            return View(MVC.Admin.Entity.ActionNames.Add);
        }

        [HttpGet]
        [HandleError()]
        public virtual ActionResult DeleteGuestBook(int id)
        {
            DataService.DeleteGuestBook(new GuestBookModel(){ GuestBookId = id});
            return RedirectToAction(MVC.Admin.Entity.ActionNames.Index,new {area=MVC.Admin.Entity.Area});
        }

        [HttpPost]
        public virtual ActionResult Add(GuestBookModel book)
        {
            if (!ModelState.IsValid)
            {
                DataService.AddGuestBook(book);
                return RedirectToAction(MVC.Admin.Entity.ActionNames.Index);
            }
            return View(MVC.Admin.Entity.Views.Add);
        }

        [HttpGet]
        public virtual ActionResult Edit(int id)
        {
            var book = DataService.LoadGuestBook(id);
            return View(MVC.Admin.Entity.Views.Edit, book);
        }

        [HttpPost]
        public virtual ActionResult Save(GuestBookModel book)
        {
            if (ModelState.IsValid)
            {
                DataService.UpdateGuestBook(book);
                return RedirectToAction(MVC.Admin.Entity.ActionNames.Index);
            }
            return View(MVC.Admin.Entity.Views.Edit, book );
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            filterContext.ExceptionHandled = true;

            filterContext.Result = View(MVC.Admin.Shared.Views.ControllerError, filterContext.Exception);
        }
    }
}
