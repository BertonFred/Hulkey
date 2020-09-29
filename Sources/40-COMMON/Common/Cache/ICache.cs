using System;

namespace Hulkey.Common.Cache
{
    /// <summary>
    /// Interface de base pour un gestionnaire de cache de niveau 2
    /// </summary>
    public interface ICache
    {
        /// <summary>
        /// Ajoute une cle/valeur dans le caches
        /// </summary>
        /// <typeparam name="T_DATA_TO_CACHE">Le type de la données a mettre dans le cache</typeparam>
        /// <param name="Key">La clé</param>
        /// <param name="value">La valeur</param>
        void AddValue<T_DATA_TO_CACHE>(string Key, T_DATA_TO_CACHE value);

        /// <summary>
        /// Effacer la valeur dans le cache
        /// </summary>
        /// <param name="Key">La clé a supprimée dans le cache</param>
        void ClearValue(string Key);

        /// <summary>
        /// Demande une valeur au cache via la clé, si la valeur n'est pas trouvé
        /// La fonction de chargement de la valeur est invoqué
        /// </summary>
        /// <typeparam name="T_DATA_TO_CACHE">Le type de la données a mettre dans le cache</typeparam>
        /// <param name="Key">La clé</param>
        /// <param name="loadFunc">Fonction de chargement de la données dans le cache</param>
        /// <returns>Retourne la valeur depuis la cache, si necessaire, chargement de la données</returns>
        T_DATA_TO_CACHE GetOrAddValue<T_DATA_TO_CACHE>(string Key, Func<T_DATA_TO_CACHE> loadFunc);

        /// <summary>
        /// Retourne la valeur pour la clé depuis la cache, null si non trouvé
        /// </summary>
        /// <typeparam name="T_DATA_TO_CACHE">Le type de la données a mettre dans le cache</typeparam>
        /// <param name="Key">La clé</param>
        /// <returns>La valeur correspondant à la cle, ou null si la clé n'est pas trouvé</returns>
        T_DATA_TO_CACHE GetValue<T_DATA_TO_CACHE>(string Key);
    }
}