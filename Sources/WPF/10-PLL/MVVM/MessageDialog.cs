using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hulkey.PLL.MVVM
{
    /// <summary>
    /// Classe ppur faire des message sous forme de  boite de dialogue a l'utilisateur
    /// </summary>
    public sealed class MessageDialog
    {
        /// <summary>
        /// Message de confirmation avec une reponse Oui ou non
        /// </summary>
        /// <param name="title">Le titre</param>
        /// <param name="message">Le message</param>
        /// <returns>Affirmative ou Nagative en fonction du bouton utilisé (OK, Annuler)</returns>
        static public async Task<MessageDialogResult> ShowAffirmativeAndNegative(string title, string message)
        {
            var result = await ApplicationContext.Instance.ShellView.ShowMessageAsync(title, message, MessageDialogStyle.AffirmativeAndNegative);
            return result;
        }

        /// <summary>
        /// Message d'information avec une reponse ok
        /// </summary>
        /// <param name="title">Le titre</param>
        /// <param name="message">Le message</param>
        static public async void ShowAffirmative(string title, string message)
        {
            await ApplicationContext.Instance.ShellView.ShowMessageAsync(title, message, MessageDialogStyle.Affirmative);
        }
    }
}
