using System;
using System.Collections.Generic;
using System.Text;

namespace Hulkey.DAL
{
    /// <summary>
    /// Interface de base des objects de type Item de Liste
    /// Implementation du pattern DTO
    /// Permet de reduire le nombre de propriété d'un objet (persistant) au nombre
    /// de propriété visualisé dans la liste
    /// A minima un ID
    /// </summary>
    public interface IListItemDTO
    {
        int ID { get; set; }
    }
}
