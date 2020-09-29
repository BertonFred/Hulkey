using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Hulkey.PLL.PresentationCommon
{
    /// <summary>
    /// Classe de base pour les objets qui doivent notifier via INotifyPropertyChanged
    /// Lors la valeur d'une propriété est changé
    /// </summary>
    public class Observable : INotifyPropertyChanged
    {
        /// <summary>
        /// Gets or sets a value that indicates whether the underlying model has been modified. 
        /// </summary>
        /// <remarks>
        /// Used when sync'ing with the server to reduce load and only upload the models that have changed.
        /// </remarks>
        public bool IsModified { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Change property value, and send propertynotify changed
        /// also change the state IsModified
        /// </summary>
        protected void Set<T>(ref T storage, T value, [CallerMemberName]string propertyName = null,bool bMarkAsModified=true)
        {
            if (Equals(storage, value) == false)
            {
                IsModified = bMarkAsModified;
                storage = value;
                NotifyPropertyChanged(propertyName);
            }
        }

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
    }
}
