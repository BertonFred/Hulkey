using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace Hulkey.Common.Cache
{
    /// <summary>
    /// Service locator pour le service de cache
    /// </summary>
    public class CacheLocator
    {
        /// <summary>
        /// Retourne l'instance du cache.
        /// Le cache est un singleton
        /// </summary>
        public static ICache Get
        {
            get
            {
                if (m_Cache == null)
                    m_Cache = new SimpleCache();
                return m_Cache;
            }
        }
        static SimpleCache m_Cache;
    }
}
