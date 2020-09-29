using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Hulkey.PLL.MVVM
{
    /// <summary>
    /// Classe de base pour les objets qui doivent notifier via INotifyPropertyChanged
    /// Lorsque la valeur d'une propriété est changé
    /// et qui doivent en plus gerer un etat de modification
    /// lors de l'utilisation du Set
    /// </summary>
    public class ObservableWithState : INotifyPropertyChanged
    {
        /// <summary>
        /// Change property value, and send propertynotify changed
        /// also change the state IsModified
        /// </summary>
        protected void Set<T>(ref T storage, T value, [CallerMemberName]string propertyName = null,bool bMarkAsModified=true)
        {
            if (Equals(storage, value) == false)
            {
                if (bMarkAsModified == true)
                    MarkAsModified();
                storage = value;
                NotifyPropertyChanged(propertyName);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Notify property changed
        /// </summary>
        public void NotifyPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Notify all properties
        /// </summary>
        public void NotifyChanges()
        {
            NotifyPropertyChanged("");
        }

        /// <summary>
        /// Retourne true si l'objet doit être sauvegardé
        /// </summary>
        public bool NeedSave
        {
            get 
            {
                if (State == eState.Unchanged) return false;
                if (PersistanteState == ePersistantState.Transiant && State == eState.Deleted) return false;

                return true;
            }
        }

        /// <summary>
        /// Marque l'objet comme Transiant, donc il n'exist pas encore en base de données
        /// </summary>
        public void MarkAsTransiant()
        {
            this.PersistanteState = ePersistantState.Transiant;
            this.State = eState.Unchanged;
        }

        /// <summary>
        /// Marque l'objet comme Persistant, il vient de la base de données
        /// </summary>
        public void MarkAsPersistant()
        {
            this.PersistanteState = ePersistantState.Persistant;
            this.State = eState.Unchanged;
        }

        /// <summary>
        /// Marquer l'onjet comme modifier
        /// </summary>
        public void MarkAsModified()
        {
            if (this.State == eState.Deleted) return; // si l'objet est delete ne plus suivre les modificiations
            this.State = eState.Modified;
        }

        /// <summary>
        /// Marquer l'onjet comme modifier
        /// </summary>
        public void MarkAsUnchanged()
        {
            this.State = eState.Unchanged;
        }

        /// <summary>
        /// Marquer l'onjet comme supprimer
        /// </summary>
        public void MarkAsDeleted()
        {
            this.State = eState.Deleted;
        }

        /// <summary>
        /// Etat de persistance de l'objet
        /// </summary>
        public ePersistantState PersistanteState { get; set; }

        /// <summary>
        /// Etat de modification de l'objet
        /// </summary>
        public eState State { get; set; }
    }

    /// <summary>
    /// Etat de persistance d'un objet.
    /// issu ou pas de la base de données
    /// S'il vient de la base de données il est à l'état initiale Persistant
    /// S'il n'est pas encore en base de données, il est a l'état transiant
    /// </summary>
    public enum ePersistantState
    { 
        Transiant=1, // L'objet n'existe pas en base de données
        Persistant=2 // L'objet existe en base de données
    }

    /// <summary>
    /// Etat de modification d'un objet
    /// </summary>
    public enum eState
    {
        Unchanged=1, // Non modifier, l'objet est forcement persistant
        Modified=2,  // Modifier, l'objet peut etre transiant ou persistant
        Deleted=3    // Supprimer, l'objet peut etre transiant ou persistant
    }
}
