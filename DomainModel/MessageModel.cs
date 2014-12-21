using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.Validations;

namespace DomainModel
{
    
    public class MessageModel
    {
        public int MessageId { get; set; }

        [DisplayName("заголовок сообщения")]
        [Required(ErrorMessage = "Введите заголовок сообщения")]
        [StringLength(50, ErrorMessage = "Заголовок сообщения не должен превышать 50 символов")]
        public string Title { get; set; }

        [DisplayName("текст сообщения")]
        [StringLength(250, ErrorMessage = "Длина сообщения не должна превышать 250 символов")]
        [Required(ErrorMessage = "Введите текст сообщения")]
        public string Body { get; set; }

        public DateTime CreateDate { get; set; }

        [DisplayName("гостевая книга")]
        public GuestBookModel GuestBook { get; set; }

        [DataType(DataType.DateTime)]
        [UpdateValidate]
        public DateTime UpdateDate { get; set; }

        //[Required(AllowEmptyStrings = true, ErrorMessage = "Введите число")]
        //[RegularExpression(@"\d+(\,\d+){0,1}", ErrorMessage = "Введите число")]
        public decimal Weight { get; set; }



        public Nullable<decimal> WeightDouble { get; set; }

        public MessageModel()
        {
            GuestBook = new GuestBookModel();
        }
    }


}
