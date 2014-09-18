using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuestBook.ModelView
{
    public class GuestBooksOnCardModelView
    {
        public int SelectedGuestBookId { get; set; }
        public IEnumerable<DomainModel.GuestBookModel> GuestBooks { get; set; }
        
    }
}