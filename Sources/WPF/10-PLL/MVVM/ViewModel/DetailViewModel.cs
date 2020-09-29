using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace Hulkey.PLL.MVVM
{
    /// <summary>
    /// Classe de base des ViewModel Detail
    /// Donc a utiliser pour les view qui affiche une information
    /// </summary>
    public abstract class DetailViewModel : ViewModelBase
    {
        public DetailViewModel()
            : base()
        {
        }

        /// <summary>
        /// Chargement de la donnée affiché par la view
        /// Le chargement est a prendre au sens chargement de la vue
        /// donc la données peut être issu de la base ou être une données transient
        /// </summary>
        public abstract void Load(int iProduitID);

        /// <summary>
        /// Charge les données necessaire a l'affiche de la données courante
        /// par exemple les listes pour les combobox, ou les listes de données liée
        /// à la données courante
        /// </summary>
        protected virtual void LoadDependences()
        {
        }

        /// <summary>
        ///  Sauvegarde les modification de la view de detaille
        /// </summary>
        public abstract void Save();

        /// <summary>
        /// Retourne true s'il y a besoins de faire une sauvegarde
        /// </summary>
        public virtual bool CanSave()
        {
            return this.NeedSave;
        }

        /// <summary>
        ///  Ferme la vue de detaille
        /// </summary>
        public abstract void Close();

        /// <summary>
        ///  demande la confirmation de la femerture de la vue de detaille
        ///  si les données n'ont pas etait sauvegardé
        /// </summary>
        public async void ConfirmClose()
        {
            if (NeedSave == true)
            {
                var result = await MessageDialog.ShowAffirmativeAndNegative("Enregistrer", "Les données modifiées vont être perdu");
                if (result == MessageDialogResult.Negative)
                    return;
            }
        }

        /// <summary>
        /// Retourne true si on peu fermer la vue de détaille
        /// </summary>
        /// <returns></returns>
        public bool CanClose()
        {
            return true;
        }

        #region COMMAND
        public ICommand HomeCmd
        {
            get
            {
                return m_HomeCmd ?? (m_HomeCmd = new RelayCommand(Close, CanClose));
            }
        }
        private ICommand m_HomeCmd;
        public ICommand SaveCmd
        {
            get
            {
                return m_SaveCmd ?? (m_SaveCmd = new RelayCommand(Save, CanSave));
            }
        }
        private ICommand m_SaveCmd;
        public ICommand CloseCmd
        {
            get
            {
                return m_CloseCmd ?? (m_CloseCmd = new RelayCommand(Close, CanClose));
            }
        }
        private ICommand m_CloseCmd;
        #endregion
    }
}
