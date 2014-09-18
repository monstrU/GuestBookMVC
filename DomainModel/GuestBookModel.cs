using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DomainModel
{
    
    public class GuestBookModel
    {
        public int GuestBookId { get; set; }
        
        [Required(ErrorMessage = "Не задано название гостевой книги") ]
        [StringLength(150, ErrorMessage = "Длина не должна превышать 150 символов")]
        [DisplayName("Гостевая книга")]
        public string GuestBookName { get; set; }
    }
}