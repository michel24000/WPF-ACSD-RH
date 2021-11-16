using WPF_ACSD_RH.ViewModels;
using WPF_ACSD_RH.Views;
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
using WPF_ACSD_RH.Models;

namespace WPF_ACSD_RH.Views
{
    /// <summary>
    /// Logique d'interaction pour Appli.xaml
    /// </summary>
    public partial class Accueil : Window
    {
        Users userConnecte;
        public Accueil(Users _users)
        {
            InitializeComponent();
            userConnecte = new Users(_users.IdUser, _users.Nom, _users.Prenom, _users.Email, _users.Password,_users.CongesRestant, _users.RecuperationRestante, _users.Profil, _users.IdCentre);
            Nomuserconnecte.Text = userConnecte.Nom;
            Prenomuserconnecte.Text = userConnecte.Prenom;
            //Centreuserconnecte.Text = userConnecte.IdCentre.ToString();
            DataContext = new AccueilViewModel(userConnecte);

            if(userConnecte.Profil != 1)
            {
                ButtonAdmin.Visibility = Visibility.Hidden;
                ImageAdmin.Visibility = Visibility.Hidden;
            }
        }

        private void AccueilButton_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new AccueilViewModel(userConnecte);
        }

        private void TempsButton_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new TempsViewModel();
        }

        private void CongesButton_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new CongesViewModel(userConnecte);
        }

        private void PlanningButton_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new PlanningViewModel();
        }

        private void AdminButton_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new AdminViewModel(userConnecte);
        }

        private void DeconnexionButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow retourAuthentification = new MainWindow();
            retourAuthentification.Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new ListeDemandeCongesViewModel(userConnecte);
        }
    }
}
