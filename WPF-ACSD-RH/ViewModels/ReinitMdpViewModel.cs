using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WPF_ACSD_RH.Models;

namespace WPF_ACSD_RH.ViewModels
{
    class ReinitMdpViewModel : INotifyPropertyChanged
    {

        #region VARIABLES        
        private string email;
        private List<string> resultat;
        #endregion

        #region SETTER_GETTER        
        public string Email { get { return email; } set { email = value; OnPropertyChanged("Email"); } }        
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
        public ReinitMdpViewModel()
        {
            commmandReinitialiserMdp = new Command(ActionReinitialiserMdp);
        }        
        #endregion

        #region ICOMMAND
        public ICommand commmandReinitialiserMdp { get; set; }
        #endregion

        #region private void ActionReinitialiserMdp(object obj)
        private void ActionReinitialiserMdp(object obj)
        {
            if (email == null)
                MessageBox.Show("Merci de renseigner un email avant de cliquer sur Réinitialiser ! ");
            else
            {
                Users users = new Users(email);
                CrudUsers crudUsers = new CrudUsers();
                resultat = crudUsers.LireUser(users);

                if (resultat.Count != 0)
                {
                    MailSmtp mailSmtp = new MailSmtp();
                    mailSmtp.SendtTokenMessage(email);
                    MessageBox.Show("Demande de réinitialisation de votre mot de passe envoyé sur votre adresse mail : <br><br>" +
                                    "- " + email);
                }
                else
                    MessageBox.Show("Le mail renseigné n'est pas connu ! ");
            }                
        }
        #endregion
    }
}
