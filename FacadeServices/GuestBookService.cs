using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DomainModel;
using FacadeServices.Interfaces;

namespace FacadeServices
{
    public class GuestBookService : IGuestBookService
    {
        protected string  ConnectionString { get;private set; }
        public IDataProvider Provider { get;private  set; }

        public GuestBookService(IDataProvider provider)
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["GuestBookConnect"].ConnectionString;
            Provider = provider;
        }
        public IEnumerable<GuestBookModel> LoadGuestBooks()
        {
            var gbooks = Provider.Query<GuestBookModel>(ConnectionString 
                , @"select GuestBookId,  GuestBookName from GuestBooks order by GuestBookName"
                , CommandType.Text);

            return gbooks;
        }

        public GuestBookModel AddGuestBook(GuestBookModel book)
        {
           Provider.Execute(ConnectionString
                , @"insert into GuestBooks(GuestBookName) values(@szGuestBookName)"
                , new { szGuestBookName = book.GuestBookName }
                , CommandType.Text);
            return book;
        }

        public GuestBookModel DeleteGuestBook(GuestBookModel book)
        {
            Provider.Execute(ConnectionString
                 , @"delete from GuestBooks where GuestBookId=@nGuestBookId"
                 , new { nGuestBookId = book.GuestBookId }
                 , CommandType.Text);
            return book;
        }
        public GuestBookModel LoadGuestBook(int guestBookId)
        {
            var gbooks = Provider.Query<GuestBookModel>(ConnectionString
                , @"select GuestBookId,  GuestBookName from GuestBooks where GuestBookId=@nGuestBookId"
                , new { nGuestBookId = guestBookId}
                , CommandType.Text);

            return gbooks.FirstOrDefault();
        }

        public void UpdateGuestBook(GuestBookModel  book)
        {
            Provider.Execute(ConnectionString
                , @"update GuestBooks set GuestBookName=@szGuestBookName where GuestBookId=@nGuestBookId"
                , new { szGuestBookName = book.GuestBookName, nGuestBookId = book.GuestBookId }
                , CommandType.Text);
        }

        public IEnumerable<MessageModel> LoadMessages()
        {
            
            var messages = Provider.Query<MessageModel>(ConnectionString
                , @"select MessageId, Title, Body, CreateDate, GuestBookId, UserId from dbo.Messages order by CreateDate desc"
                , CommandType.Text);
            return messages;
        }

        public IEnumerable<MessageModel> LoadMessagesInBook(int guestBookId)
        {

            var messages = Provider.Query<MessageModel>(ConnectionString
                , @"select MessageId, Title, Body, CreateDate, GuestBookId, UserId from dbo.Messages 
                            where  GuestBookId=@nGuestBookId                       
                            order by CreateDate desc"
                , new { nGuestBookId = guestBookId}
                , CommandType.Text);
            return messages;
        }

        public void AddMessage(MessageModel model)
        {
            Provider.Execute(ConnectionString
                , @"insert into Messages(Title, Body, UserId, GuestBookId) values(@szTitle, @szBody, null, @szGuestBookId)"
                , new {szTitle = model.Title, szBody = model.Body, szGuestBookId = model.GuestBook.GuestBookId}
                , CommandType.Text);

        }

        public  GuestBookModel GetDefaultGuestBook(Nullable<int> guestbookId)
        {
            int tempGuestBookId = 1;
            GuestBookModel book= new GuestBookModel();
            book.GuestBookId = guestbookId.HasValue ? guestbookId.Value : tempGuestBookId;
            return book;
        }

    }
}
