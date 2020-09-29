using Hulkey.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hulkey.SLL.ServiceCommon
{
    /// <summary>
    /// Une classe Business Action permet d'executer un service complexe pour un domaine 
    /// metier
    /// Une business action est déclenché par une methode d'un Business Service.
    /// Une classe buisnessAction doit traiter un service unique offert par le domaine metier.
    /// La difference entre une methode de service dans un BusinessService et une BuisnessAction
    /// est la compléxité du traitement. 
    /// Si un traitement de service est trop complexe (plus de deux ecrans) alors il doit être 
    /// implementée comme une BusinessAction pour pouvoir structurer le code en sous fonctions.
    /// </summary>
    public abstract class BusinessAction : IBusinessAction
    {
        /// <summary>
        /// Une business Action est liée à un Business Service
        /// </summary>
        public BusinessAction(IBusinessServiceBase _ParentBS)
        {
            this.ParentBS = _ParentBS;
        }

        public abstract bool CanExecute();
        public abstract bool Execute();

        public IBusinessServiceBase ParentBS { get; set; }
        public IUnitOfWork uow { get { return this.ParentBS.uow;  } }
    }
}
