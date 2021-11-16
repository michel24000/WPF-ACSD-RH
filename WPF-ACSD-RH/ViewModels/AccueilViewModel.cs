using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WPF_ACSD_RH.Models;
using WPF_ACSD_RH.Views;

namespace WPF_ACSD_RH.ViewModels
{
    class AccueilViewModel
    {
        #region VARIABLES        
        private int iduserconnecte;
        private string nomuserconnecte;
        private string prenomuserconnecte;
        private string emailuserconnecte;
        private string profiluserconnecte;
        private int idprofil;
        #endregion

        #region SETTER_GETTER
        public int Iduserconnecte { get { return iduserconnecte; } set { iduserconnecte = value; OnPropertyChanged("Iduserconnecte"); } }
        public string Nomuserconnecte { get { return nomuserconnecte; } set { nomuserconnecte = value; OnPropertyChanged("Nomuserconnecte"); } }
        public string Prenomuserconnecte { get { return prenomuserconnecte; } set { prenomuserconnecte = value; OnPropertyChanged("Prenomuserconnecte"); } }
        public string Emailuserconnecte { get { return emailuserconnecte; } set { emailuserconnecte = value; OnPropertyChanged("Emailuserconnecte"); } }
        public string Profiluserconnecte { get { return profiluserconnecte; } set { profiluserconnecte = value; OnPropertyChanged("Profiluserconnecte"); } }
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
        public AccueilViewModel(Users _user)
        {                           
            iduserconnecte = _user.IdUser;
            nomuserconnecte = _user.Nom;
            prenomuserconnecte = _user.Prenom;
            emailuserconnecte = _user.Email;
            idprofil = _user.Profil;

            //test du profil            
            if (idprofil == 1)
            {
                profiluserconnecte = "Admin";
            }

            if (idprofil == 2)
            {
                profiluserconnecte = "N + 1";
            }

            if (idprofil == 3)
            {
                profiluserconnecte = "Salarié";
            }

            CommandelectureVideo = new Command(ActionLectureVideo);            
        }
        #endregion

        #region ICOMMAND
        public ICommand CommandelectureVideo { get; set; }
        public ICommand CommandeDeconnexion { get; set; }
        #endregion

        #region private void ActionLectureVideo(object obj)
        private void ActionLectureVideo(object obj)
        {
            tuto1 tuto1 = new tuto1();
            tuto1.Show();
        }
        #endregion
    }
}
