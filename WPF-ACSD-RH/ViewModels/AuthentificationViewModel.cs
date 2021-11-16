using WPF_ACSD_RH.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows;
using WPF_ACSD_RH.Views;
using System.Windows.Controls;

namespace WPF_ACSD_RH.ViewModels
{
    class AuthentificationViewModel : INotifyPropertyChanged
    {
        #region VARIABLES
        private int idconnubdd;
        private string nomconnubdd;
        private string prenomconnubdd;
        private string mailconnuBdd;
        private string passconnuBdd;
        private int congesRestantconnubdd;
        private int recuperationRestanteconnubdd;
        private int profilconnubdd;
        private int centreconnubdd;

        private string email;
        private string password;
        private List<string> resultat;
        string[] tableauResultat;
        private Window _window;
        #endregion

        #region SETTER_GETTER
        public string Email { get { return email; } set { email = value; OnPropertyChanged("Email"); } }
        //public string Password { get { return password; } set { password = value; OnPropertyChanged("Password"); } }
        #endregion

        #region PROPERTYCHANGED
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string nomPropriete)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(nomPropriete));
            }
        }
        #endregion

        #region CTOR
        public AuthentificationViewModel(Window window)
        {
            commmandSeConnecter = new Command(ActionSeConnecter);
            commandReinitialiserMdp = new Command(ActionReinitialiserMdp);
            _window = window;

            // cherche si un identifiant et mdp sont enregistrés
            if (Properties.Settings.Default.mdp != null && Properties.Settings.Default.mail != null)
            {
                Email = Properties.Settings.Default.mail;
                password = Properties.Settings.Default.mdp;
            }
        }
        #endregion

        #region ICOMMAND
        public ICommand commmandSeConnecter { get; set; }
        public ICommand commandReinitialiserMdp { get; set; }

        #endregion

        #region private void ActionSeConnecter(object obj)
        private void ActionSeConnecter(object obj)
        {
            var passwordBox = obj as PasswordBox;
            password = passwordBox.Password;
            if (Email != null && password != null)
            {
                //on cherche si un id et mdp son enregistré
                //if (Properties.Settings.Default.mdp == null && Properties.Settings.Default.mail == null)
                //{
                //    Properties.Settings.Default.mail = Email;
                //    Properties.Settings.Default.mdp = password;
                //    Properties.Settings.Default.Save();
                //}
                Users users = new Users(password, Email);
                CrudUsers verificationUser = new CrudUsers();
                resultat = verificationUser.LireUser(users);

                if (resultat.Count != 0)
                {
                    foreach (string verif in resultat)
                    {
                        tableauResultat = verif.Split('/');
                    }

                    idconnubdd = Convert.ToInt16(tableauResultat[0]);
                    nomconnubdd = tableauResultat[1];
                    prenomconnubdd = tableauResultat[2];
                    mailconnuBdd = tableauResultat[3];
                    passconnuBdd = tableauResultat[4];
                    congesRestantconnubdd = Convert.ToInt16(tableauResultat[5]);
                    recuperationRestanteconnubdd = Convert.ToInt16(tableauResultat[6]);
                    profilconnubdd = Convert.ToInt16(tableauResultat[7]);
                    centreconnubdd = Convert.ToInt16(tableauResultat[8]);

                    HachageMdp hachageMdp = new HachageMdp();
                    var passwordSaisiHach = hachageMdp.Hash(password);

                    if (mailconnuBdd == Email && passconnuBdd == passwordSaisiHach)
                    {
                        //création d'un user pour l'envoyer en paramétre vers la view Accueil
                        Users userconnecte = new Users(idconnubdd, nomconnubdd, prenomconnubdd, mailconnuBdd, passconnuBdd, congesRestantconnubdd, recuperationRestanteconnubdd, profilconnubdd, centreconnubdd);

                        Accueil application = new Accueil(userconnecte);
                        application.Show();
                        _window.Close();
                    }
                    else
                        MessageBox.Show("Les identifiants de connexion sont incorrectes !");
                }
                else
                    MessageBox.Show("L'email est inconnu !");
            }
            else
                MessageBox.Show("Veuillez compléter les champs !");
        }
        #endregion

        #region private void ActionReinitialiserMdp(object obj)
        private void ActionReinitialiserMdp(object obj)
        {
            ReinitMdp reinitMdp = new ReinitMdp();
            reinitMdp.Show();
        }
        #endregion

    }
}
