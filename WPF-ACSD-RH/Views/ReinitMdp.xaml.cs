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
using WPF_ACSD_RH.ViewModels;

namespace WPF_ACSD_RH.Views
{
    /// <summary>
    /// Logique d'interaction pour RéinitMdp.xaml
    /// </summary>
    public partial class ReinitMdp : Window
    {
        public ReinitMdp()
        {
            InitializeComponent();
            DataContext = new ReinitMdpViewModel();
        }
    }
}
