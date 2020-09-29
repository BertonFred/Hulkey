using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Hulkey.DAL.Entities;

namespace Hulkey.DAL.DTO
{
    /// <summary>
    /// Utilisateur pour un affichage en liste
    /// </summary>
    public class UtilisateurListItemDTO : IListItemDTO
    {
        public int ID { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string DisplayName { get => $"{Nom} {Prenom}"; }
        public string Password { get; set; }
        public string eMail { get; set; }
        public string Telephonne { get; set; }
        public eRoleUtilisateur Role { get; set; }
    }
}
