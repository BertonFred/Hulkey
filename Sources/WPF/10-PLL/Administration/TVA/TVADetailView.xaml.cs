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
    /// Interaction logic for TVADetailView.xaml
    /// </summary>
    public partial class TVADetailView : ViewNavigationControl
    {
        public TVADetailView()
        {
            this.DataContext = new TVADetailViewModel(0);
            InitializeComponent();
        }

        public TVADetailView(int iTVAID)
        {
            this.DataContext = new TVADetailViewModel(iTVAID);
            InitializeComponent();
        }
    }
}
