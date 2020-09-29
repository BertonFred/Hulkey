using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hulkey.DAL.Entities
{
    /// <summary>
    /// Un Utilisateur, utilisateur du logiciel
    /// </summary>
    public class Utilisateur : PersistentObject
    {
        public Utilisateur()
        {
            Role = eRoleUtilisateur.Serveur;
        }

        [Required]
        [MaxLength(60)]
        public string Nom { get; set; }

        [Required]
        [MaxLength(60)]
        public string Prenom { get; set; }

        /// <summary>
        /// Password est sur 4 digit, il s'agit de chiffre entre 0..9
        /// </summary>
        [Required]
        [MaxLength(10)]
        public string Password { get; set; }

        public string eMail { get; set; }
        public string Telephonne { get; set; }

        /// <summary>
        /// Role de l'utilisateur
        /// </summary>
        public eRoleUtilisateur Role { get; set; }

        /// <summary>
        /// Nom a afficher pour l'utilisateur
        /// </summary>
        [NotMapped]
        public string DisplayName { get { return $"{Nom} {Prenom}"; } }

    }
}
