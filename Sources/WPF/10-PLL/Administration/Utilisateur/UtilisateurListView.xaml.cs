﻿using Hulkey.PLL.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hulkey.PLL.Administration
{
    /// <summary>
    /// Interaction logic for UtilisateurListView.xaml
    /// </summary>
    public partial class UtilisateurListView : ViewNavigationControl
    {
        public UtilisateurListView()
        {
            this.DataContext = new UtilisateurListViewModel();
            InitializeComponent();
        }
    }
}
