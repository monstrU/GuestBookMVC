using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DomainModel;

namespace GuestBook.ModelView
{
    public class EditMessageModelView
    {
        public MessageModel Message { get; set; }
        public IEnumerable<GuestBookModel> GuestBooks { get; set; }

        public EditMessageModelView()
        {
            Message=new MessageModel();
            GuestBooks = new GuestBookModel[]{};
        }
    }
}