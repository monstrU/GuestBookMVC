using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel;

namespace FacadeServices.Interfaces
{
    public interface IGuestBookService
    {
        IEnumerable<GuestBookModel> LoadGuestBooks();
        IEnumerable<MessageModel> LoadMessages();
        void AddMessage(MessageModel model);
        IDataProvider Provider { get; }
        GuestBookModel LoadGuestBook(int guestBookId);
        void UpdateGuestBook(GuestBookModel  book);
        GuestBookModel AddGuestBook(GuestBookModel book);
        GuestBookModel DeleteGuestBook(GuestBookModel book);
        GuestBookModel GetDefaultGuestBook(Nullable<int> guestbookId);
        IEnumerable<MessageModel> LoadMessagesInBook(int guestBookId);
    }
}
