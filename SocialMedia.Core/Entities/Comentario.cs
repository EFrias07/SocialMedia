using SocialMedia.Infrastructure.Entities;
using System;
using System.Collections.Generic;

namespace SocialMedia.Infrastructure.Entitites
{
    public partial class Comentario
    {
        public int IdComentario { get; set; }
        public int IdPublicacion { get; set; }
        public int IdUsuario { get; set; }
        public string Descripcion { get; set; }
        public DateTime Fecha { get; set; }
        public bool Activo { get; set; }

        public virtual Publicacion IdPublicacionNavigation { get; set; }
        public virtual Usuario IdUsuarioNavigation { get; set; }
    }
}
