using WPF_ACSD_RH.ViewModels;
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

namespace WPF_ACSD_RH
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();                     
            DataContext = new AuthentificationViewModel(this);
        }

        private void ShowPassword_Checked(object sender, RoutedEventArgs e)
        {
            passwordTexbox2.Text = passwordTexbox.Password;
            passwordTexbox.Visibility = Visibility.Collapsed;
            passwordTexbox2.Visibility = Visibility.Visible;
        }

        private void ShowPassword_Unchecked(object sender, RoutedEventArgs e)
        {
            passwordTexbox.Password = passwordTexbox2.Text;
            passwordTexbox2.Visibility = Visibility.Collapsed;
            passwordTexbox.Visibility = Visibility.Visible;
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {

        }
    }
}
