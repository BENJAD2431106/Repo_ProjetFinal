using System.ComponentModel.DataAnnotations;

namespace OnyraProjet.Models
{
    public class ProfilModel
    {
        public string NomUtilisateur { get; set; } = string.Empty;
        public string PrenomUtilisateur { get; set; } = string.Empty;
        public string Courriel { get; set; } = string.Empty;

        [Range(0, 115, ErrorMessage = "L'âge doit être entre 0 et 115.")]
        public int Age { get; set; }
        [RegularExpression(@"^\d*$", ErrorMessage = "La RAMQ doit contenir uniquement des chiffres.")]
        public string? RamQ { get; set; }
        public byte[]? Photo { get; set; } // Pour affichage / téléchargement de la photo
    }
}
