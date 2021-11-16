using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPF_ACSD_RH.Models;
using WPF_ACSD_RH.Views;
using WPF_ACSD_RH.ViewModels;
using System.Windows.Input;
using System.Web.UI.WebControls;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Data;
using MySql.Data.MySqlClient;
using System.Net.Mail;

namespace WPF_ACSD_RH.ViewModels 
{
    class ListeDemandeCongesViewModel : INotifyPropertyChanged
    {
        #region VARIABLES
        
        private int idUserConnecte;
        private int profilUserConnecte;
        private int idCentreUserConnecte;
        private List<string> resultat;
        private DateTime dateTimeDebut, dateTimeFin;
        private string dateDebutInverse, dateFinInverse;
        private List<Conges> listeConges;
        private string[] tableau;        
        //private Users newConges;
        private Conges congesSelectionne;
        private bool btnValider, btnRefuser, btnSupprimer;        
        private string mailDuN1, mailCongesConnuBdd;
        private DateTime DateDuJour = DateTime.Now;
        private string dateShort;
        private string etablissement;
        private Visibility filtreRechercheConges;

        private List<Etablissement> listeEtablissement;
        private Etablissement etablissementSelectionne;
        private List<Mois> listeMois;
        private Mois moisSelectionne;
        private List<Decision> listeDecision;
        private Decision decisionSelectionne;
        private int indexEtablissement;
        private string requete, indexMois;
        private string requeteEtablissement, requeteMois, requeteDecision;
        #endregion

        #region SETTER_GETTER
        public List<Conges> ListeConges { get { return listeConges; } set { listeConges = value; OnPropertyChanged("ListeConges"); } }
        //récupére le conges sélectionné pour remplir les textbox(s) et combobox de profil
        public Conges CongesSelectionne { get { return congesSelectionne; } set { congesSelectionne = value; OnPropertyChanged("ListeConges"); } }      
        public bool BtnValider { get { return btnValider; } set { btnValider = value; OnPropertyChanged("BtnValider"); } }
        public bool BtnRefuser { get { return btnRefuser; } set { btnRefuser = value; OnPropertyChanged("BtnRefuser"); } }
        public bool BtnSupprimer { get { return btnSupprimer; } set { btnSupprimer = value; OnPropertyChanged("BtnSupprimer"); } }
        public Visibility FiltreRechercheConges { get { return filtreRechercheConges; } set { filtreRechercheConges = value; OnPropertyChanged("FiltreRechercheConges"); } }
        public List<Etablissement> ListeEtablissement { get { return listeEtablissement; } set { listeEtablissement = value; OnPropertyChanged("ListeEtablissement"); } }
        //récupére l'etablissement sélectionné pour le filtrage des demandes de congés
        public Etablissement EtablissementSelectionne { get { return etablissementSelectionne; } set { etablissementSelectionne = value; OnPropertyChanged("EtablissementSelectionne"); } }
        public List<Mois> ListeMois { get { return listeMois; } set { listeMois = value; OnPropertyChanged("ListeMois"); } }
        //récupére le mois sélectionné pour le filtrage des demandes de congés
        public Mois MoisSelectionne { get { return moisSelectionne; } set { moisSelectionne = value; OnPropertyChanged("MoisSelectionne"); } }
        public List<Decision> ListeDecision { get { return listeDecision; } set { listeDecision = value; OnPropertyChanged("ListeDecision"); } }
        public Decision DecisionSelectionne { get { return decisionSelectionne; } set { decisionSelectionne = value; OnPropertyChanged("DecisionSelectionne"); } }
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
        public ICommand CommmandValiderConges { get; set; }
        public ICommand CommmandRefuserConges { get; set; }
        public ICommand CommmandSupprimerConges { get; set; }
        public ICommand CommmandAnnulerConges { get; set; }
        public ICommand CommandFiltrerDemandeConges { get; set; }
        #endregion

        #region CTOR
        public ListeDemandeCongesViewModel(Users _user)
        {            
            idUserConnecte = _user.IdUser;
            profilUserConnecte = _user.Profil;
            idCentreUserConnecte = _user.IdCentre;

            #region remplissage combobox filtre Etablissement
            //remplissage combobox filtre Etablissement
            listeEtablissement = new List<Etablissement>();
            Etablissement etablissementfiltre0 = new Etablissement("Tous");
            Etablissement etablissementfiltre1 = new Etablissement("00 - Siège");
            Etablissement etablissementfiltre2 = new Etablissement("01 - Béthune");
            Etablissement etablissementfiltre3 = new Etablissement("02 - Gayant");
            Etablissement etablissementfiltre4 = new Etablissement("03 - Dorignies");
            Etablissement etablissementfiltre5 = new Etablissement("04 - Esquerchin");
            Etablissement etablissementfiltre6 = new Etablissement("05 - Frais-Marais");
            listeEtablissement.Add(etablissementfiltre0);
            listeEtablissement.Add(etablissementfiltre1);
            listeEtablissement.Add(etablissementfiltre2);
            listeEtablissement.Add(etablissementfiltre3);
            listeEtablissement.Add(etablissementfiltre4);
            listeEtablissement.Add(etablissementfiltre5);
            listeEtablissement.Add(etablissementfiltre6);
            #endregion 

            #region remplissage combobox filtre mois
            //remplissage combobox filtre décision
            listeMois = new List<Mois>();
            Mois mois1 = new Mois("Tous");
            Mois mois2 = new Mois("Janvier");
            Mois mois3 = new Mois("Février");
            Mois mois4 = new Mois("Mars");
            Mois mois5 = new Mois("Avril");
            Mois mois6 = new Mois("Mai");
            Mois mois7 = new Mois("Juin");
            Mois mois8 = new Mois("Juillet");
            Mois mois9 = new Mois("Août");
            Mois mois10 = new Mois("Septembre");
            Mois mois11 = new Mois("Octobre");
            Mois mois12 = new Mois("Novembre");
            Mois mois13 = new Mois("Décembre");
            listeMois.Add(mois1);
            listeMois.Add(mois2);
            listeMois.Add(mois3);
            listeMois.Add(mois4);
            listeMois.Add(mois5);
            listeMois.Add(mois6);
            listeMois.Add(mois7);
            listeMois.Add(mois8);
            listeMois.Add(mois9);
            listeMois.Add(mois10);
            listeMois.Add(mois11);
            listeMois.Add(mois12);
            listeMois.Add(mois13);
            #endregion           

            #region remplissage combobox filtre décision
            //remplissage combobox filtre décision
            listeDecision = new List<Decision>();
            Decision decision0 = new Decision("Tous");
            Decision decision1 = new Decision("En attente");
            Decision decision2 = new Decision("Validé");
            Decision decision3 = new Decision("Refusé");
            Decision decision4 = new Decision("Annulé");
            listeDecision.Add(decision0);
            listeDecision.Add(decision1);
            listeDecision.Add(decision2);
            listeDecision.Add(decision3);
            listeDecision.Add(decision4);
            #endregion

            Requetebdd();

            CommmandValiderConges = new Command(ActionValiderConges);
            CommmandRefuserConges = new Command(ActionRefuserConges);
            CommmandAnnulerConges = new Command(ActionAnnulerConges);
            CommmandSupprimerConges = new Command(ActionSupprimerConges);
            CommandFiltrerDemandeConges = new Command(ActionFiltrerDemandeConges);
        }
        #endregion

        #region private void Requetebdd()
        private void Requetebdd()
        {
            //On détermine la date du jour
            dateShort = DateDuJour.ToString("dd/MM/yyyy");

            Conges newConges = new Conges(idUserConnecte);
            CrudConges verificationUser = new CrudConges();

            if (profilUserConnecte == 1)
            {
                resultat = verificationUser.LireCongesAdmin();
                btnValider = true; //active le button pour valider les demandes de congès
                btnRefuser = true; //active le button pour refuser les demandes de congès
                BtnSupprimer = true; //active le button pour supprimer une demande dont les date sont dépassées
                filtreRechercheConges = Visibility.Visible; //active les filtres de recherches
            }
            if (profilUserConnecte == 2)
            { 
                resultat = verificationUser.LireCongesN1(idCentreUserConnecte);
                btnValider = true; //active le button pour valider les demandes de congès
                btnRefuser = true; //active le button pour refuser les demandes de congès
                BtnSupprimer = false; //désactive le button pour supprimer une demande dont les date sont dépassées
                filtreRechercheConges = Visibility.Hidden; //désactive les filtres de recherches

            }
            if (profilUserConnecte == 3)
            {
                resultat = verificationUser.LireCongesSalarie(newConges);
                btnValider = false;
                btnRefuser = false;
                BtnSupprimer = false;
                filtreRechercheConges = Visibility.Hidden;
            }

            if (resultat.Count != 0)
            {
                ListeConges = new List<Conges> { };

                foreach (string verif in resultat)
                {
                    tableau = verif.Split('/');
                    dateTimeDebut = Convert.ToDateTime(tableau[5]);
                    dateDebutInverse = dateTimeDebut.ToString("dd/MM/yyyy");

                    dateTimeFin = Convert.ToDateTime(tableau[6]);
                    dateFinInverse = dateTimeFin.ToString("dd/MM/yyyy");

                    //on formate l'affichage de l'établissement en string car reçu de la bdd en int
                    if (Convert.ToInt16(tableau[0]) == 1)
                        etablissement = "00 - Siège";
                    if (Convert.ToInt16(tableau[0]) == 2)
                        etablissement = "01 - Béthune";
                    if (Convert.ToInt16(tableau[0]) == 3)
                        etablissement = "02 - Gayant";
                    if (Convert.ToInt16(tableau[0]) == 4)
                        etablissement = "03 - Dorignies";
                    if (Convert.ToInt16(tableau[0]) == 5)
                        etablissement = "04 - Esquerchin";
                    if (Convert.ToInt16(tableau[0]) == 6)
                        etablissement = "05 - Frais-Marais";

                    Conges newConges1 = new Conges(etablissement, tableau[1], tableau[2], Convert.ToInt16(tableau[3]), Convert.ToInt16(tableau[4]), dateDebutInverse, dateFinInverse, Convert.ToInt16(tableau[7]), Convert.ToInt16(tableau[8]), tableau[9], tableau[10]);
                    ListeConges.Add(newConges1);
                }
            }
            //else            
                //MessageBox.Show("Une erreur de lecture en base est survenue !");            
        }
        #endregion        

        #region private void ActionValiderConges(object obj)
        private void ActionValiderConges(object obj)
        {
            if(congesSelectionne == null)            
                MessageBox.Show("Veuillez sélectionner un congés avant de cliquer sur valider !");            
            else
            {
                if (congesSelectionne.Decision != "Validé")
                {
                    if (idUserConnecte == congesSelectionne.Emetteur && profilUserConnecte != 1)                    
                        MessageBox.Show("Vous ne pouvez pas valider votre propre demande de congés !");                    
                    else
                    {
                        CrudConges valideConges = new CrudConges();
                        valideConges.validerConges(Convert.ToInt16(congesSelectionne.IdConges));

                        // récupére le mail de l'emetteur du congè
                        Users usersANotifier = new Users(congesSelectionne.Emetteur);
                        CrudUsers crudUsers = new CrudUsers();
                        resultat = crudUsers.LireUserId(usersANotifier);

                        if (resultat.Count != 0)
                        {
                            foreach (string verif in resultat)
                            {
                                tableau = verif.Split('/');
                            }

                            mailCongesConnuBdd = tableau[3];

                            MailSmtp mailSmtp = new MailSmtp();
                            mailSmtp.SendtMessage(mailCongesConnuBdd, true, congesSelectionne.DateDebut, congesSelectionne.DateFin);
                        }
                        Requetebdd();
                    }
                }else                
                    MessageBox.Show("Le congés sélectionné est déjà validé !");                
            }            
        }
        #endregion

        #region private void ActionRefuserConges(object obj)
        private void ActionRefuserConges(object obj)
        {
            if (congesSelectionne == null)
                MessageBox.Show("Veuillez sélectionner un congés avant de cliquer sur refuser !");
            else
            {
                if (congesSelectionne.Decision != "Validé")
                {
                    if (idUserConnecte == congesSelectionne.Emetteur && profilUserConnecte != 1)                    
                        MessageBox.Show("Vous ne pouvez pas refuser votre propre demande de congè !");                    
                    else
                    {
                        //refuser le congès
                        CrudConges refuserConges = new CrudConges();
                        refuserConges.refuserConges(Convert.ToInt16(congesSelectionne.IdConges));

                        // récupére le mail de l'emetteur du congè
                        Users usersANotifier = new Users(congesSelectionne.Emetteur);
                        CrudUsers crudUsers = new CrudUsers();
                        resultat = crudUsers.LireUserId(usersANotifier);

                        if (resultat.Count != 0)
                        {
                            foreach (string verif in resultat)
                            {
                                tableau = verif.Split('/');
                            }

                            mailCongesConnuBdd = tableau[3];

                            MailSmtp mailSmtp = new MailSmtp();
                            mailSmtp.SendtMessage(mailCongesConnuBdd, false, congesSelectionne.DateDebut, congesSelectionne.DateFin);                                                       
                        }

                        Requetebdd();
                    }
                }
                else
                    MessageBox.Show("Un congè validé ne peut être refusé !");
            }            
        }
        #endregion

        #region private void ActionAnnulerConges(object obj)
        private void ActionAnnulerConges(object obj)
        {
            if (congesSelectionne == null)
                MessageBox.Show("Veuillez sélectionner un congés avant de cliquer sur annuler !");
            else
            {
                if (congesSelectionne.Decision == "en attente")
                {
                    if (idUserConnecte != congesSelectionne.Emetteur)
                        MessageBox.Show("Vous ne pouvez pas demander l'annulation d'une demande de congés qui n'est pas la vôtre !");
                    else
                    {
                        //annuler le congés
                        CrudConges annulerConges = new CrudConges();
                        annulerConges.AnnulerConges(Convert.ToInt16(congesSelectionne.IdConges));

                        // récupére le mail du n+1 pour le notifier de la demande d'annulation du congés                        
                        Users userN1 = new Users(2, idCentreUserConnecte);
                        CrudUsers crudUSers = new CrudUsers();
                        resultat = crudUSers.LireEmailN1(userN1);

                        if (resultat.Count != 0)
                        {
                            foreach (string verif in resultat)
                            {
                                tableau = verif.Split('/');
                            }

                            mailDuN1 = tableau[3];

                            MailSmtp mailSmtp = new MailSmtp();
                            //mailSmtp.SendtMessage(mailCongesConnuBdd, false, congesSelectionne.PremierJour, congesSelectionne.JourReprise);
                            mailSmtp.SendtMessageN1Annulation(mailDuN1, congesSelectionne.Prenom, congesSelectionne.Nom, congesSelectionne.DateDebut, congesSelectionne.DateFin);
                        }
                        Requetebdd();
                    }
                }
                else
                    MessageBox.Show("Un congés ne peut être annulé que si une décision n'a pas déjà été prise !");
            }
        }
        #endregion

        #region private void ActionSupprimerConges(object obj)
        private void ActionSupprimerConges(object obj)
        {            
            //si aucun congè n'est sélectionné            
            if (congesSelectionne == null)            
                MessageBox.Show("Veuillez sélectionner un congés avant de cliquer sur supprimer !");            
            else
            {
                // si le conges est en attente on ne le supprime pas
                if (congesSelectionne.Decision == "en attente")
                    MessageBox.Show("Veuillez apporter une réponse avant de supprimer la demande de congés !");
                else
                {
                    //si la date de fin du congè est pas dépassée on autorise la suppression
                    if (Convert.ToDateTime(dateShort).Date > Convert.ToDateTime(congesSelectionne.DateFin).Date)
                    {
                        //on autorise la suppression
                        string Message = "Souhaitez vous vraiment supprimer le congés dont voici le détail : \r\n\r\n" +
                                       "- Nom : " + congesSelectionne.Nom + "\r\n" +
                                       "- Prenom : " + congesSelectionne.Prenom + "\r\n" +
                                       "- Date de début : " + congesSelectionne.DateDebut + "\r\n" +
                                       "- Date de fin : " + congesSelectionne.DateFin + "\r\n" +
                                       "- Nombres de congès : " + congesSelectionne.NbJoursConges + "\r\n" +
                                       "- Nombres de récupérations : " + congesSelectionne.NbJoursRecuperations + "\r\n" +
                                       "- Décision : " + congesSelectionne.Decision + "\r\n" +
                                       "- motif de refus : " + congesSelectionne.MotifRefus + "\r\n";
                        string Titre = "Attention";
                        MessageBoxResult result = MessageBox.Show(Message, Titre, MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);

                        if (result == MessageBoxResult.Yes)
                        {
                            CrudConges delConges = new CrudConges();
                            delConges.supprimerConges(Convert.ToInt16(congesSelectionne.IdConges));                            
                        }
                        Requetebdd();
                    }
                    else
                        MessageBox.Show("Congés en cours, suppression impossible !");                                       
                }                
            }           
        }
        #endregion

        #region private void ActionFiltrerDemandeConges(object obj)
        private void ActionFiltrerDemandeConges(object obj)
        {        //si aucun des filtres n'est sélectionnés    
            if (etablissementSelectionne == null && moisSelectionne == null && decisionSelectionne == null)
                MessageBox.Show("Veuillez sélectionner un filtre avant de cliquer sur filtrer les demandes !");
            else //si au moin un des filtres est sélectionné
            {
                #region action si combobox etablissement est sélectionné
                if (etablissementSelectionne != null)
                {
                    if (etablissementSelectionne.Nom != "Tous")
                    {
                        if (etablissementSelectionne.Nom == "00 - Siège")
                            indexEtablissement = 1;
                        if (etablissementSelectionne.Nom == "01 - Béthune")
                            indexEtablissement = 2;
                        if (etablissementSelectionne.Nom == "02 - Gayant")
                            indexEtablissement = 3;
                        if (etablissementSelectionne.Nom == "03 - Dorignies")
                            indexEtablissement = 4;
                        if (etablissementSelectionne.Nom == "04 - Esquerchin")
                            indexEtablissement = 5;
                        if (etablissementSelectionne.Nom == "05 - Frais-Marais")
                            indexEtablissement = 6;

                        requeteEtablissement = " AND user.idCentre = " + indexEtablissement;
                    }
                    else
                        requeteEtablissement = "";
                }
                else
                    requeteEtablissement = "";
                #endregion

                #region action si combobox mois est sélectionné
                if (moisSelectionne != null)
                {
                    if (moisSelectionne.Nom != "Tous")
                    {
                        if (moisSelectionne.Nom == "Janvier")
                            indexMois = "01";
                        if (moisSelectionne.Nom == "Février")
                            indexMois = "02";
                        if (moisSelectionne.Nom == "Mars")
                            indexMois = "03";
                        if (moisSelectionne.Nom == "Avril")
                            indexMois = "04";
                        if (moisSelectionne.Nom == "Mai")
                            indexMois = "05";
                        if (moisSelectionne.Nom == "Juin")
                            indexMois = "06";
                        if (moisSelectionne.Nom == "Juillet")
                            indexMois = "07";
                        if (moisSelectionne.Nom == "Août")
                            indexMois = "08";
                        if (moisSelectionne.Nom == "Septembre")
                            indexMois = "09";
                        if (moisSelectionne.Nom == "Octobre")
                            indexMois = "10";
                        if (moisSelectionne.Nom == "Novembre")
                            indexMois = "11";
                        if (moisSelectionne.Nom == "Décembre")
                            indexMois = "12";
                        requeteMois = " AND conges.dateDebut LIKE '%" + indexMois + "%'";
                    }
                    else
                        requeteMois = "";
                }
                else
                    requeteMois = "";
                #endregion

                #region action si combobox décision est sélectionné
                if (decisionSelectionne != null)
                {
                    if (decisionSelectionne.Nom != "Tous")
                        requeteDecision = " AND conges.decision = '" + decisionSelectionne.Nom + "'";
                    else
                        requeteDecision = "";
                }
                else
                    requeteDecision = "";
                #endregion

                #region execution de la requete
                requete = string.Format("SELECT user.idCentre, user.Nom, user.Prenom, conges.* FROM user INNER JOIN conges WHERE " +
                    "user.idUser = conges.emetteur " + requeteEtablissement + requeteMois + requeteDecision + " ORDER BY user.Nom");

                CrudConges filtrageConges = new CrudConges();
                resultat = filtrageConges.FiltrageLireCongesAdmin(requete);

                if (resultat.Count != 0)
                {
                    ListeConges = new List<Conges> { };

                    foreach (string verif in resultat)
                    {
                        tableau = verif.Split('/');
                        //on inverse la date reçu de la bdd pour un meilleur affichage
                        dateTimeDebut = Convert.ToDateTime(tableau[5]);
                        dateDebutInverse = dateTimeDebut.ToString("dd/MM/yyyy");

                        dateTimeFin = Convert.ToDateTime(tableau[6]);
                        dateFinInverse = dateTimeFin.ToString("dd/MM/yyyy");
                        //on formate l'affichage de l'établissement en string car reçu de la bdd en int
                        if (Convert.ToInt16(tableau[0]) == 1)
                            etablissement = "00 - Siège";
                        if (Convert.ToInt16(tableau[0]) == 2)
                            etablissement = "01 - Béthune";
                        if (Convert.ToInt16(tableau[0]) == 3)
                            etablissement = "02 - Gayant";
                        if (Convert.ToInt16(tableau[0]) == 4)
                            etablissement = "03 - Dorignies";
                        if (Convert.ToInt16(tableau[0]) == 5)
                            etablissement = "04 - Esquerchin";
                        if (Convert.ToInt16(tableau[0]) == 6)
                            etablissement = "05 - Frais-Marais";

                        Conges newConges2 = new Conges(etablissement, tableau[1], tableau[2], Convert.ToInt16(tableau[3]), Convert.ToInt16(tableau[4]), dateDebutInverse, dateFinInverse, Convert.ToInt16(tableau[7]), Convert.ToInt16(tableau[8]), tableau[9], tableau[10]);
                        ListeConges.Add(newConges2);
                    }
                }
                else
                {
                    MessageBox.Show("Aucune demande de congés ne correspond aux critéres de recherche !");
                }
                #endregion
            }
        }
        #endregion
    }
}