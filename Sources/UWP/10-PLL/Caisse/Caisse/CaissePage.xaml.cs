using System;


using Windows.UI.Xaml.Controls;

namespace Hulkey.PLL.Caisse
{
    public sealed partial class CaissePage : Page
    {
        public CaisseViewModel ViewModel { get; } = new CaisseViewModel();

        public CaissePage()
        {
            InitializeComponent();
        }
    }
}
