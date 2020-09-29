using Hulkey.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hulkey.SLL.ServiceCommon
{
    /// <summary>
    /// Retourne les informations sur l'utilisateur connecté
    /// </summary>
    public interface IUserContext
    {
        Utilisateur UtilisateurCourant { get; }

        string UserName { get; }
    }
}
