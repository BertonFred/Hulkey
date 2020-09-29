using Hulkey.PLL.MVVM;

namespace Hulkey.PLL.BackOffice
{
    /// <summary>
    /// Interaction logic for ProduitListView.xaml
    /// </summary>
    public partial class ProduitListView : ViewNavigationControl
    {
        public ProduitListView()
        {
            this.DataContext = new ProduitListViewModel();
            InitializeComponent();
        }
    }
}
