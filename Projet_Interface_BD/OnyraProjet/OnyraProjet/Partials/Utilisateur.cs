using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnyraProjet.Models
{
    public partial class Utilisateur
    {
        [NotMapped]
        [Required]
        public string mdpInscription { get; set; } 
    }
}
