using Entites.Enums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entites.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [Column("USR_CPF")]
        public string CPF { get; set; }

        [Column("USR_TIPO")]
        public TipoUsuario? Tipo { get; set; }
    }
}