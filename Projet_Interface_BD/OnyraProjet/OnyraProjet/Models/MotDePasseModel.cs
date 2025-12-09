using System.ComponentModel.DataAnnotations;

namespace OnyraProjet.Models
{
    public class ChangerMotDePasseModel
    {
        [Required(ErrorMessage = "Le mot de passe actuel est obligatoire.")]
        public string MotDePasseActuel { get; set; } = string.Empty;

        [Required(ErrorMessage = "Le nouveau mot de passe est obligatoire.")]
        [MinLength(6, ErrorMessage = "Le mot de passe doit contenir au moins 6 caractères.")]
        public string NouveauMotDePasse { get; set; } = string.Empty;

        [Required(ErrorMessage = "La confirmation est obligatoire.")]
        [Compare("NouveauMotDePasse", ErrorMessage = "Les mots de passe ne correspondent pas.")]
        public string ConfirmationMotDePasse { get; set; } = string.Empty;
    }
}
