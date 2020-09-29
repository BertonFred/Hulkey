using Hulkey.Common;
using Hulkey.Common.Cache;
using Hulkey.DAL;
using Hulkey.DAL.DTO;
using Hulkey.DAL.Entities;
using Hulkey.DAL.Repository;
using Hulkey.SLL.ServiceCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hulkey.SLL.Services
{
    /// <summary>
    /// Service de journalisation
    /// Permet de tracé les action réalisé avec le logiciel
    /// Le journal ecris dans la base avec son propre context
    /// Chaque ecriture est ecrite dans sa propre transaction
    /// </summary>
    public sealed class JournalService
    {
        /// <summary>
        /// Historise une info dans le journal
        /// </summary>
        /// <param name="sAction">L'action concerné</param>
        /// <param name="sParametre">Parametre eventuel</param>
        /// <param name="UserContext">Le user context</param>
        public static void Historise(string sAction, string sParametre,IUserContext UserContext)
        {
            Journal DataToLog;
            try
            {
                string sUserName = UserContext == null ? "Inconnu" : UserContext.UserName;

                var repo = Instance.uow.GetRepository<JournalRepository>();
                DataToLog = new Journal()
                {
                    Action = sAction,
                    Parametre = sParametre,
                    CreatedBy = sUserName,
                    CreatedOn = DateTime.Now,

                };
                repo.Create(DataToLog);
                Instance.uow.SaveChanges();
            }
            catch (Exception e)
            {
                Log.Error($"JournalService.Log Probléme pendant l'ecriture dans le journal", e);
            }
        }

        /// <summary>
        /// GetList renvois la liste des données du journal
        /// </summary>
        /// <param name="dtStart">Critere de recherche sur la date de creation dans le journal</param>
        /// <param name="SearchText">Critere de recherche sur action</param>
        /// <returns>Les données trouvées dans une liste, qui peut être vide</returns>
        public static List<JournalListItemDTO> GetList(DateTimeOffset? dtStart, string SearchText = null)
        {
            List<JournalListItemDTO> lst;

            try
            {
                var repo = Instance.uow.GetRepository<JournalRepository>();
                IQueryable<Journal> query = repo.FindAll();

                if (SearchText != null)
                    query = query.Where(a => a.Action.ToUpper().Contains(SearchText.ToUpper()) == true);

                if (dtStart != null)
                    query = query.Where(a => a.CreatedOn >= dtStart);

                lst = query.OrderBy(a => a.CreatedOn)
                            .Select(a => new JournalListItemDTO()
                            {
                                ID = a.ID,
                                Action = a.Action,
                                Parametre = a.Parametre,
                                CreatedBy = a.CreatedBy,
                                CreatedOn = a.CreatedOn,
                            })
                            .ToList();
            }
            catch (Exception e)
            {
                Log.Error($"Probléme pendant la recherche des données: {SearchText}", e);
                lst = new List<JournalListItemDTO>();
            }

            return lst;
        }

        private JournalService()
        {
            this.uow = new HulkeyUnitOfWork();
        }

        private static JournalService Instance 
        {
            get
            {
                if (m_Instance == null)
                    m_Instance = new JournalService();
                return m_Instance;
            }
        }
        private static JournalService m_Instance = null;

        private HulkeyUnitOfWork uow { get; set; }
    }
}
