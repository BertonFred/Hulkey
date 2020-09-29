using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace Hulkey.Common.Cache
{
    /// <summary>
    /// Gestionnaire de cache de 2eme Niveau
    /// Implementation naive, mais threadsafe
    /// </summary>
    public class SimpleCache : ICache
    {
        /// <summary>
        /// Le cache est un singletion
        /// </summary>
        internal SimpleCache()
        {
            Values = new ConcurrentDictionary<string, object>();
        }

        /// <summary>
        /// Demande une valeur au cache via la clé, si la valeur n'est pas trouvé
        /// La fonction de chargement de la valeur est invoqué
        /// </summary>
        /// <typeparam name="T_DATA_TO_CACHE">Le type de la données a mettre dans le cache</typeparam>
        /// <param name="Key">La clé</param>
        /// <param name="loadFunc">Fonction de chargement de la données dans le cache</param>
        /// <returns>Retourne la valeur depuis la cache, si necessaire, chargement de la données</returns>
        public T_DATA_TO_CACHE GetOrAddValue<T_DATA_TO_CACHE>(string Key, Func<T_DATA_TO_CACHE> loadFunc)
        {
            object Value = null;

            // Recherche la cle dans le cache
            if (Values.TryGetValue(Key, out Value) == false)
            {
                // Si pas trouvé on ajoute la données dans le cache
                // la données est calculer par la fonction de chargement
                Value = loadFunc();
                Values[Key] = Value;
            }

            return (T_DATA_TO_CACHE)Value;
        }

        /// <summary>
        /// Retourne la valeur pour la clé depuis la cache, null si non trouvé
        /// </summary>
        /// <typeparam name="T_DATA_TO_CACHE">Le type de la données a mettre dans le cache</typeparam>
        /// <param name="Key">La clé</param>
        /// <returns>La valeur correspondant à la cle, ou null si la clé n'est pas trouvé</returns>
        public T_DATA_TO_CACHE GetValue<T_DATA_TO_CACHE>(string Key)
        {
            object Value = null;
            Values.TryGetValue(Key, out Value);
            return (T_DATA_TO_CACHE)Value;
        }

        /// <summary>
        /// Ajoute une cle/valeur dans le caches
        /// </summary>
        /// <typeparam name="T_DATA_TO_CACHE">Le type de la données a mettre dans le cache</typeparam>
        /// <param name="Key">La clé</param>
        /// <param name="value">La valeur</param>
        public void AddValue<T_DATA_TO_CACHE>(string Key, T_DATA_TO_CACHE value)
        {
            Values[Key] = (object)value;
        }

        /// <summary>
        /// Effacer la valeur dans le cache
        /// </summary>
        /// <param name="Key">La clé a supprimée dans le cache</param>
        public void ClearValue(string Key)
        {
            object Value;
            Values.TryRemove(Key, out Value);
        }

        /// <summary>
        /// Le cache cle/valeur
        /// </summary>
        private ConcurrentDictionary<string, object> Values;
    }
}
