using Hulkey.Common;
using Hulkey.DAL.Entities;
using Hulkey.DAL.Repository;
using Hulkey.PLL.MVVM;
using Hulkey.SLL.ServiceCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hulkey.PLL.PresentationCommon
{
    /// <summary>
    /// Gestion du context utilisateur courant
    /// SINGLETON
    /// </summary>
    public sealed class UserContext : Observable, IUserContext
    {
        private UserContext()
        {
        }

        /// <summary>
        /// Retourne l'instance singleton de l'Application contexte
        /// </summary>
        public static UserContext Instance
        {
            get
            {
                if (m_Instance == null)
                    m_Instance = new UserContext();
                return m_Instance;
            }
        }
        private static UserContext m_Instance;

        /// <summary>
        /// Changer l'utilisateur courant
        /// </summary>
        /// <param name="iUserID">L'id de l'utilisateur qui doit devenir l'utilisateur courant. Null pour indiqué pas de usercourant</param>
        public void ChangeUtilisateurCourant(int? iUserID)
        {
            if (iUserID == null)
            {
                this.UtilisateurCourant = null;
                Log.Info($"Il n'y a plus d'utilisateur courant.");
            }
            else
            {
                try
                {
                    using (HulkeyUnitOfWork uow = new HulkeyUnitOfWork())
                    {
                        var repo = uow.GetRepository<UtilisateurRepository>();
                        UtilisateurCourant = repo.Read(iUserID.Value);
                        Log.Info($"Changement d'utiliateur courant : {UtilisateurCourant.ID}:{UtilisateurCourant.DisplayName}");
                    }
                }
                catch (Exception e)
                {
                    Log.Error($"Impossible de faire le changement d'utilisateur courant : {iUserID}",e);
                    this.UtilisateurCourant = null;
                }
            }
        }

        /// <summary>
        /// Retourne l'utilisateur courant, null, si pas d'utilisateur courant
        /// </summary>
        public Utilisateur UtilisateurCourant
        {
            get => m_UtilisateurCourant;
            private set
            {
                Set(ref m_UtilisateurCourant, value);

                ApplicationContext.Instance.ShellViewModel.UtilisateurCourant = value;
                NotifyPropertyChanged("UserName");
            }
        }
        private Utilisateur m_UtilisateurCourant;

        /// <summary>
        /// Retourne l'utilisateur courant, null, si pas d'utilisateur courant
        /// </summary>
        public string UserName
        {
            get
            {
                return UtilisateurCourant == null ? "Inconnu" : UtilisateurCourant.DisplayName;
            }
        }
    }
}
