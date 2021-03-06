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
    /// Interaction logic for AddSeasonWindow.xaml
    /// </summary>
    public partial class AddSeasonWindow : Window
    {
        public AddSeasonWindow(int show)
        {
            InitializeComponent();
            DataContext = new AddSeasonWindowViewModel() { Window = this, IdTvShow = show };
        }
    }
}
