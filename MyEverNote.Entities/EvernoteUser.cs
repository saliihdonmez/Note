using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEverNote.Entities
{
    [Table("EvernotUsers")]
    public class EvernoteUser:MyEntityBase
    {
        [DisplayName("İsim"), StringLength(25)]
        public string Name { get; set; }

        [DisplayName("Soyisim"), StringLength(25)]
        public string Surname { get; set; }

        [DisplayName("Kullanıcı Adı"), Required,StringLength(25)]
        public string Username { get; set; }

        [DisplayName("E-Posta"), Required, StringLength(100)]
        public string Email { get; set; }

        [DisplayName("Şifre"), Required, StringLength(100)]
        public string Paswword { get; set; }

        [StringLength(30),ScaffoldColumn(false)] //images/user_*.jpg
        public string ProfileImageFilename { get; set; }

        [Required, ScaffoldColumn(false)]
        public Guid ActivateGuid { get; set; }

        [DisplayName("Is Active")]
        public bool IsActive { get; set; }
        [DisplayName("Is Admin")]
        public bool  IsAdmin{ get; set; }

        public virtual List<Note> Notes { get; set; }
        public virtual List<Comment> Comments { get; set; }
        public virtual List<Liked> Likes { get; set; }
    }
}
