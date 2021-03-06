using Common.Models;
using Klijent.ViewModel;
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
using System.Windows.Shapes;

namespace Klijent.View
{
    /// <summary>
    /// Interaction logic for ShowSeasonsWindow.xaml
    /// </summary>
    public partial class ShowSeasonsWindow : Window
    {
        public ShowSeasonsWindow(TvShow show)
        {
            InitializeComponent();
            DataContext = new ShowSeasonsWindowViewModel(show) { Window = this, ShowTvShow = show };
        }
    }
}
