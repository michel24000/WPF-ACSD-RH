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
    class CongesViewModel : INotifyPropertyChanged
    {
        #region VARIABLES
        Users userConnecte;
        private int idUser;
        private string etablissement;
        private string fonction;
        private string nom;
        private string prenom;
        private string dateDebut;
        private string dateFin;
        private DateTime dateTimeDebut;
        private DateTime dateTimeFin;
        private string dateDebutInverse;
        private string dateFinInverse;
        private int congesAnnuel = 0;
        private int recuperation = 0;
        private int congesAutres = 0;
        private int congesTotal;
        private List<string> resultat;
        string[] tableauResultat;
        private int congesRestantconnubdd;
        private int recuperationRestanteconnubdd;
        private string mailDuN1;
        #endregion

        #region SETTER_GETTER
        public int IdUser { get => idUser; set => idUser = value; }
        public string Etablissement { get { return etablissement; } set { etablissement = value; OnPropertyChanged("Etablissement"); } }
        public string Fonction { get { return fonction; } set { fonction = value; OnPropertyChanged("Fonction"); } }
        public string Nom { get { return nom; } set { nom = value; OnPropertyChanged("Nom"); } }
        public string Prenom { get { return prenom; } set { prenom = value; OnPropertyChanged("Prenom"); } }
        public string DateDebut { get { return dateDebut; } set { dateDebut = value; OnPropertyChanged("DateDebut"); } }
        public string DateFin { get { return dateFin; } set { dateFin = value; OnPropertyChanged("DateFin"); } }
        public int CongesAnnuel { get { return congesAnnuel; } set { congesAnnuel = value; CongesTotal = CongesAnnuel + Recuperation + CongesAutres; OnPropertyChanged("CongesAnnuel"); } }
        public int Recuperation { get { return recuperation; } set { recuperation = value; CongesTotal = CongesAnnuel + Recuperation + CongesAutres; OnPropertyChanged("Recuperation"); } }
        public int CongesAutres { get { return congesAutres; } set { congesAutres = value; CongesTotal = CongesAnnuel + Recuperation + CongesAutres; OnPropertyChanged("CongesAutres"); } }
        public int CongesTotal { get { return congesTotal; } set { congesTotal = value; OnPropertyChanged("CongesTotal"); } }
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

        #region ICOMMAND
        public ICommand EnvoyerDemandeConges { get; set; }
        #endregion

        #region CTOR
        public CongesViewModel(Users _user)
        {
            userConnecte = new Users(_user.IdUser, _user.Nom, _user.Prenom, _user.Email, _user.Profil, _user.IdCentre);
            IdUser = userConnecte.IdUser;
            Nom = userConnecte.Nom;
            Prenom = userConnecte.Prenom;
            if (userConnecte.IdCentre == 1)
                Etablissement = "Siège";
            if (userConnecte.IdCentre == 2)
                Etablissement = "Béthune";
            if (userConnecte.IdCentre == 3)
                Etablissement = "Gayant";
            if (userConnecte.IdCentre == 4)
                Etablissement = "Dorignies";
            if (userConnecte.IdCentre == 5)
                Etablissement = "Esquerchin";
            if (userConnecte.IdCentre == 6)
                Etablissement = "Frais-Marais";

            if (userConnecte.Profil == 1)
                Fonction = "Admin";
            if (userConnecte.Profil == 2)
                Fonction = "N+1";
            if(userConnecte.Profil == 3)
                Fonction = "Salarié";

            EnvoyerDemandeConges = new Command(ActionEnvoyerDemandeConges);            
        }
        #endregion

        #region private void ActionEnvoyerDemandeConges(object obj)
        private void ActionEnvoyerDemandeConges(object obj)
        {            
            //transformation des dates pour insertion en bdd
            dateTimeDebut = Convert.ToDateTime(dateDebut);
            dateDebutInverse = dateTimeDebut.ToString("yyyy-MM-dd");

            dateTimeFin = Convert.ToDateTime(dateFin);
            dateFinInverse = dateTimeFin.ToString("yyyy-MM-dd");

            if (dateDebutInverse != "0001-01-01" && dateFinInverse != "0001-01-01")
            {
                if (CongesTotal != 0)
                {
                    //on vérifie en bdd si le nombre de congès est suffisant
                    Users UserVerifieConges = new Users(IdUser);
                    CrudUsers crudVerifieConges = new CrudUsers();
                    resultat = crudVerifieConges.LireUserId(UserVerifieConges);

                    if (resultat.Count != 0)
                    {
                        foreach (string verif in resultat)
                        {
                            tableauResultat = verif.Split('/');
                        }

                        congesRestantconnubdd = Convert.ToInt16(tableauResultat[5]);
                        recuperationRestanteconnubdd = Convert.ToInt16(tableauResultat[6]);

                        if (CongesAnnuel <= congesRestantconnubdd && Recuperation <= recuperationRestanteconnubdd)
                        {
                            //on insére la demande de congès si le solde est supérieur ou égale
                            Conges newConges = new Conges(IdUser, dateDebutInverse, dateFinInverse, CongesAnnuel, Recuperation, "en attente", "en attente");
                            CrudConges insertionConges = new CrudConges();
                            insertionConges.ajouterConges(newConges);

                            //on veux notifier le n+1 d'une demande de congés
                            //pour cela on recherche son mail                          
                            Users userN1 = new Users(2, userConnecte.IdCentre);
                            CrudUsers crudUSers = new CrudUsers();
                            resultat = crudUSers.LireEmailN1(userN1);

                            if (resultat.Count != 0)
                            {
                                foreach (string verif in resultat)
                                {
                                    tableauResultat = verif.Split('/');
                                }
                                mailDuN1 = tableauResultat[3];                                
                            }

                            //on envoi un mail de notification de demande de congés au n+1 trouvé
                            MailSmtp mailSmtp = new MailSmtp();
                            mailSmtp.SendtMessageN1(mailDuN1,userConnecte.Prenom, userConnecte.Nom, dateDebut, dateFin);
                        }
                        else
                        {
                            if(CongesAnnuel > congesRestantconnubdd)
                            MessageBox.Show("Attention, votre solde de congés annuel est inférieur à votre demande !");
                            if (Recuperation > recuperationRestanteconnubdd)
                            MessageBox.Show("Attention, votre solde de récuperation est inférieur à votre demande !");
                        }
                    }                                                                
                }
                else                
                    MessageBox.Show("Veuillez ajouter un nombre de jour de congés !");                                                 
            }            
            else                         
                MessageBox.Show("Veuillez choisir une date de début et de fin de congés !");                                       
        }
        #endregion
    }
}
