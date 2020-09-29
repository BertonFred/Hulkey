
using System;
using Hulkey.Common;
using Hulkey.DAL.Repository;
using Windows.UI.Xaml.Controls;

namespace Hulkey.CreateDatabase
{
    public class DatabaseManager
    {
        public static void CreationDeLaBase(ListBox lbOutput)
        {
            Log.Info("Début CreationDeLaBase");

            bool result = DatabaseHelpers.EnsureCreated();

            Log.Info("Fin CreationDeLaBase");
        }
    }
}
