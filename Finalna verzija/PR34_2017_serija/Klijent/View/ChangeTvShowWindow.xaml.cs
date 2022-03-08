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
    /// Interaction logic for ChangeTvShowWindow.xaml
    /// </summary>
    public partial class ChangeTvShowWindow : Window
    {
        ITvShowCommandExecutor tvShowCommandExecutor;
        public ChangeTvShowWindow(TvShow tv, ITvShowCommandExecutor tvShowCommandExecutor)
        {
            InitializeComponent();
            DataContext = new ChangeTvShowWindowViewModel(tvShowCommandExecutor, tv) { Window = this, ChangeTvShow = tv };
            this.tvShowCommandExecutor = tvShowCommandExecutor;
        }
    }
}
