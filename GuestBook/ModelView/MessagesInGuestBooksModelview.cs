using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DomainModel;

namespace GuestBook.ModelView
{
    public class MessagesInGuestBooksModelview
    {
        public IEnumerable<MessageModel> Messages { get; set; }
        public GuestBookModel Book { get; set; }

        public MessagesInGuestBooksModelview()
        {
            Book=new GuestBookModel();
        }
    }
}