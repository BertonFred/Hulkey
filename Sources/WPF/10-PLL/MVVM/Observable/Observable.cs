using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Hulkey.PLL.MVVM
{
    /// <summary>
    /// Classe de base pour les objets qui doivent notifier via INotifyPropertyChanged
    /// Lors la valeur d'une propriété est changé
    /// </summary>
    public class Observable : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Change property value, and send propertynotify changed
        /// also change the state IsModified
        /// </summary>
        protected void Set<T>(ref T storage, T value, [CallerMemberName]string propertyName = null)
        {
            if (Equals(storage, value) == false)
            {
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
