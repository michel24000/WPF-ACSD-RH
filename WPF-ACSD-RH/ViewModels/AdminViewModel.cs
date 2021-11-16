using WPF_ACSD_RH.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPF_ACSD_RH.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows.Media;

namespace WPF_ACSD_RH.ViewModels
{
    public class AdminViewModel : INotifyPropertyChanged
    {
        #region VARIABLES        
        private int profiluserconnecte, idUser,congesRestant,recuperationRestante, profil, centre;        
        private string nom, prenom, email, password;        
        private List<string> resultat;
        private string[] tableau;
        private Users newUser, userSelectionne, userRefresh; 
        private bool itemsAdmin, itemsN1, itemsSalarie;  //binding des items du combobox de profil        
        private bool itemsSiege, itemsBethune, itemsGayant, itemsDorignies, itemsEsquerchin, itemsFraisMarais;  //binding des items du combobox de centre        
        private bool isUserSelected; //binding button modifier et supprimer pour désactivation ou activation        
        private string profilSelectionne; //binding du combobox de profil retour en string
        private string centreSelectionne; //binding du combobox de centre retour en string   
        private string ProfilPopUpProfil; //variable de profil de la boite de dialogue suppression utilisateur
        private string ProfilPopUpCentre; //variable de centre de la boite de dialogue suppression utilisateur
        //private List<Users> usersBddList;
        private int nombresCollaborateurs, nombresDetenteurs, nombresEtablissements, nombresProfils, nombresConges; //procedures stockées
        private string mdpHache;
        private SolidColorBrush colorBtnModifier;
        private SolidColorBrush colorBtnSupprimer;
        private Visibility cacheMdp; //cache les information de mot de passe 
        //private Thickness borderThicknessBtn;
        #endregion

        #region SETTER_GETTER
        public int IdUser { get { return idUser; } set { idUser = value; OnPropertyChanged("IdUser"); } }
        public string Nom { get { return nom; } set { nom = value; OnPropertyChanged("Nom"); } }
        public string Prenom { get { return prenom; } set { prenom = value; OnPropertyChanged("Prenom"); } }
        public string Email { get { return email; } set { email = value; OnPropertyChanged("Email"); } }
        public string Password { get { return password; } set { password = value; OnPropertyChanged("Password"); } }
        public int CongesRestant { get { return congesRestant; } set { congesRestant = value; OnPropertyChanged("CongesRestant"); } }
        public int RecuperationRestante { get { return recuperationRestante; } set { recuperationRestante = value; OnPropertyChanged("RecuperationRestante"); } }
        public int Profil { get { return profil; } set { profil = value; OnPropertyChanged("Profil"); } }
        public int Centre { get { return centre; } set { centre = value; OnPropertyChanged("Centre"); } }
        public bool ItemsAdmin { get { return itemsAdmin; } set { itemsAdmin = value; OnPropertyChanged("ItemsAdmin"); } }
        public bool ItemsN1 { get { return itemsN1; } set { itemsN1 = value; OnPropertyChanged("ItemsN1"); } }
        public bool ItemsSalarie { get { return itemsSalarie; } set { itemsSalarie = value; OnPropertyChanged("ItemsSalarie"); } }
        public bool ItemsSiege { get { return itemsSiege; } set { itemsSiege = value; OnPropertyChanged("ItemsSiege"); } }
        public bool ItemsBethune { get { return itemsBethune; } set { itemsBethune = value; OnPropertyChanged("ItemsBethune"); } }
        public bool ItemsGayant { get { return itemsGayant; } set { itemsGayant = value; OnPropertyChanged("ItemsGayant"); } }
        public bool ItemsDorignies { get { return itemsDorignies; } set { itemsDorignies = value; OnPropertyChanged("ItemsDorignies"); } }
        public bool ItemsEsquerchin { get { return itemsEsquerchin; } set { itemsEsquerchin = value; OnPropertyChanged("ItemsEsquerchin"); } }
        public bool ItemsFraisMarais { get { return itemsFraisMarais; } set { itemsFraisMarais = value; OnPropertyChanged("ItemsFraisMarais"); } }
        public bool IsUserSelected { get { return isUserSelected; } set { isUserSelected = value; OnPropertyChanged("IsUserSelected"); } }
        public string ProfilSelectionne { get { return profilSelectionne; } set { profilSelectionne = value; OnPropertyChanged("ProfilSelectionne"); } }
        public string CentreSelectionne { get { return centreSelectionne; } set { centreSelectionne = value; OnPropertyChanged("CentreSelectionne"); } }                
        public int NombresCollaborateurs { get { return nombresCollaborateurs; } set { nombresCollaborateurs = value; OnPropertyChanged("NombresCollaborateurs"); } }
        public int NombresDetenteurs { get { return nombresDetenteurs; } set { nombresDetenteurs = value; OnPropertyChanged("NombresDetenteurs"); } }
        public int NombresEtablissements { get { return nombresEtablissements; } set { nombresEtablissements = value; OnPropertyChanged("NombresEtablissements"); } }
        public int NombresProfils { get { return nombresProfils; } set { nombresProfils = value; OnPropertyChanged("NombresProfils"); } }
        public int NombresConges { get { return nombresConges; } set { nombresConges = value; OnPropertyChanged("NombresConges"); } }
        public SolidColorBrush ColorBtnModifier { get { return colorBtnModifier; } set { colorBtnModifier = value; OnPropertyChanged("ColorBtnModifier"); } }
        public SolidColorBrush ColorBtnSupprimer { get { return colorBtnSupprimer; } set { colorBtnSupprimer = value; OnPropertyChanged("ColorBtnSupprimer"); } }
        public Visibility CacheMdp { get { return cacheMdp; } set { cacheMdp = value; OnPropertyChanged("CacheMdp"); } }


        private List<Users> usersBddList;
        // binding de la souce du combobox des utilisateurs.
        public List<Users> UsersBddList { get { return usersBddList; } set { usersBddList = value; OnPropertyChanged("UsersBddList"); } }
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
        public ICommand CommandCreateUserBdd { get; set; }
        public ICommand CommandUpdateUserBdd { get; set; }
        public ICommand CommandDeleteUserBdd { get; set; }
        #endregion

        #region CTOR
        public AdminViewModel(Users _user)
        {
            //user servant au refresh
            userRefresh = _user;
            profiluserconnecte = _user.Profil;
            if(profiluserconnecte != 1)
            {
                MessageBox.Show("Vous n'avez pas les accès nécessaires pour accéder à cette interface !");
            }
            else
            {                
                Requetebdd();
                CommandCreateUserBdd = new Command(ActionCreateUserBdd);
                CommandUpdateUserBdd = new Command(ActionUpdateUserBdd);
                CommandDeleteUserBdd = new Command(ActionDeleteUserBdd);

                ItemsAdmin = false;
                ItemsN1 = false;
                ItemsSalarie = true;

                ItemsSiege = true;
                ItemsBethune = false;
                ItemsGayant = false;
                ItemsDorignies = false;
                ItemsEsquerchin = false;
                ItemsFraisMarais = false;

                IsUserSelected = false;

                //appel toutes mes procédures stockées (fonction)
                ProcedureStockees procédureSynthese = new ProcedureStockees();
                NombresCollaborateurs = Convert.ToInt16(procédureSynthese.Synthese("SELECT nbCollaborateurs() AS nbCollaborateurs"));
                NombresDetenteurs = Convert.ToInt16(procédureSynthese.Synthese("SELECT nbDetenteurs() AS nbDetenteurs"));
                NombresEtablissements = Convert.ToInt16(procédureSynthese.Synthese("SELECT nbEtablissements() AS nbEtablissements"));
                NombresProfils = Convert.ToInt16(procédureSynthese.Synthese("SELECT nbProfils() AS nbProfils"));
                NombresConges = Convert.ToInt16(procédureSynthese.Synthese("SELECT nbConges() AS nbConges"));

                ColorBtnModifier = new SolidColorBrush(Color.FromRgb(247, 248, 252));
                ColorBtnSupprimer = new SolidColorBrush(Color.FromRgb(247, 248, 252));               
            }                    
        }
        #endregion

        #region Requetebdd appelé depuis CTOR
        private void Requetebdd()
        {
            CrudUsers verificationUser = new CrudUsers();
            resultat = verificationUser.LireUserAll();
            
            if (resultat.Count != 0)
            {
                UsersBddList = new List<Users> { };                

                foreach (string verif in resultat)
                {
                    tableau = verif.Split('/');
                    newUser = new Users(Convert.ToInt16(tableau[0]), tableau[1], tableau[2], tableau[3], tableau[4],
                        Convert.ToInt16(tableau[5]), Convert.ToInt16(tableau[6]), Convert.ToInt16(tableau[7]), Convert.ToInt16(tableau[8]));
                    UsersBddList.Add(newUser);
                }
            }
            else            
                MessageBox.Show("Une erreur de lecture en base est survenue !");            
        }
        #endregion

        #region UserSelectionne
        //récupére l'user sélectionné pour remplir les textbox(s) et combobox de profil
        public Users UserSelectionne 
        { 
            get { return userSelectionne; } 
            set 
            {
                //cache les information de mot de passe si user selectionné
                CacheMdp = Visibility.Hidden;

                userSelectionne = value; OnPropertyChanged("UsersBddList");
                IdUser =  Convert.ToInt16(userSelectionne.IdUser);
                Nom = userSelectionne.Nom;
                Prenom = userSelectionne.Prenom;
                Email = userSelectionne.Email;
                Password = userSelectionne.Password;
                CongesRestant = UserSelectionne.CongesRestant;
                RecuperationRestante = UserSelectionne.RecuperationRestante;





                //test l'id de profil pour afficher le nom correspondant
                if (userSelectionne.Profil == 1)
                {
                    ItemsAdmin = true;
                    ItemsN1 = false;
                    ItemsSalarie = false;                   
                }
                if (userSelectionne.Profil == 2)
                {
                    ItemsAdmin = false;
                    ItemsN1 = true;
                    ItemsSalarie = false;                    
                }
                if (userSelectionne.Profil == 3)
                {
                    ItemsAdmin = false;
                    ItemsN1 = false;
                    ItemsSalarie = true;                    
                }
                if(userSelectionne.IdCentre == 1)
                {
                    ItemsSiege = true;
                    ItemsBethune = false;
                    ItemsGayant = false;
                    ItemsDorignies = false;
                    ItemsEsquerchin = false;
                    ItemsFraisMarais = false;
                }
                if (userSelectionne.IdCentre == 2)
                {
                    ItemsSiege = false;
                    ItemsBethune = true;
                    ItemsGayant = false;
                    ItemsDorignies = false;
                    ItemsEsquerchin = false;
                    ItemsFraisMarais = false;
                }
                if (userSelectionne.IdCentre == 3)
                {
                    ItemsSiege = false;
                    ItemsBethune = false;
                    ItemsGayant = true;
                    ItemsDorignies = false;
                    ItemsEsquerchin = false;
                    ItemsFraisMarais = false;
                }
                if (userSelectionne.IdCentre == 4)
                {
                    ItemsSiege = false;
                    ItemsBethune = false;
                    ItemsGayant = false;
                    ItemsDorignies = true;
                    ItemsEsquerchin = false;
                    ItemsFraisMarais = false;
                }
                if (userSelectionne.IdCentre == 5)
                {
                    ItemsSiege = false;
                    ItemsBethune = false;
                    ItemsGayant = false;
                    ItemsDorignies = false;
                    ItemsEsquerchin = true;
                    ItemsFraisMarais = false;
                }
                if (userSelectionne.IdCentre == 6)
                {
                    ItemsSiege = false;
                    ItemsBethune = false;
                    ItemsGayant = false;
                    ItemsDorignies = false;
                    ItemsEsquerchin = false;
                    ItemsFraisMarais = true;
                }
                //active les buttons modifier et supprimer
                IsUserSelected = true;

                ColorBtnModifier = new SolidColorBrush(Color.FromRgb(238, 120, 30));
                ColorBtnSupprimer = new SolidColorBrush(Color.FromRgb(255, 0, 6));
            }
        }
        #endregion

        #region ActionCreateUserBdd            
        private void ActionCreateUserBdd(object obj)
        {                       
            if(Nom != null && Prenom != null && Email != null && Password != null)
            {
                // on récupérer l'id de profil en cherchant quel item du combobox IsSelected est en true
                if (ItemsAdmin == true)
                    Profil = 1;
                if (ItemsN1 == true)
                    Profil = 2;
                if (ItemsSalarie == true)
                    Profil = 3;

                if(ItemsSiege == true)                
                    Centre = 1;                
                if (itemsBethune == true)                
                    Centre = 2;
                if (itemsGayant == true)
                    Centre = 3;
                if (itemsDorignies == true)
                    Centre = 4;
                if (itemsEsquerchin == true)
                    Centre = 5;
                if (itemsFraisMarais == true)
                    Centre = 6;

                HachageMdp hachageMdp = new HachageMdp();
                mdpHache = hachageMdp.Hash(Password);

                Users newUser = new Users(Nom, Prenom, Email, mdpHache, CongesRestant,RecuperationRestante, Profil, Centre);
                CrudUsers verificationUser = new CrudUsers();
                resultat = verificationUser.LireUser(newUser);

                //on vérifie si l'email de l'utilisateur existe déjà en bdd
                if (resultat.Count == 0)
                {
                    //on enregistre en bdd
                    verificationUser.AjouterUser(newUser);
                }
                else                
                    MessageBox.Show("L'email saisi est déjà connu en base !");                
            }
            else
            {
                //un des champs est vide
                if(Nom == null)                
                    MessageBox.Show("Veuillez compléter le champ Nom !");                
                if (Prenom == null)                
                    MessageBox.Show("Veuillez compléter le champ Prenom !");                
                if (Email == null)                
                    MessageBox.Show("Veuillez compléter le champ Email !");                
                if (Password == null)                
                    MessageBox.Show("Veuillez compléter le champ Mot de passe !");                
            }
        }
        #endregion

        #region ActionDeleteUserBdd
        private void ActionDeleteUserBdd(object obj)
        {            
            if (ItemsAdmin == true)
                ProfilPopUpProfil = "Admin";
            if (ItemsN1 == true)
                ProfilPopUpProfil = "N+1";
            if (ItemsSalarie == true)
                ProfilPopUpProfil = "Salarié";

            if (ItemsSiege == true)
                ProfilPopUpCentre = "Siège";
            if (ItemsBethune == true)
                ProfilPopUpCentre = "Béthune";
            if (ItemsGayant == true)
                ProfilPopUpCentre = "Gayant";
            if (ItemsDorignies == true)
                ProfilPopUpCentre = "Dorignies";
            if (ItemsEsquerchin == true)
                ProfilPopUpCentre = "Esquerchin";
            if (ItemsFraisMarais == true)
                ProfilPopUpCentre = "Frais-Marais";
            string Message = "Souhaitez vous vraiment supprimer le compte dont voici le détail : \r\n\r\n" + 
                "- Nom : " + Nom + "\r\n" +
                "- Prenom : " + Prenom + "\r\n" +
                "- Email : " + Email + "\r\n" +
                "- Mot de passe : " + Password + "\r\n" +
                "- Conges restant : " + CongesRestant + "\r\n" +
                "- Récuperation restante : " + RecuperationRestante + "\r\n" +
                "- Profil : " + ProfilPopUpProfil + "\r\n" +
                "- Etablissement : " + ProfilPopUpCentre;
            string Titre = "Attention";
            MessageBoxResult result = MessageBox.Show(Message, Titre, MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                Users newUser = new Users(idUser);
                CrudUsers verificationUser = new CrudUsers();
                verificationUser.SupprimerUser(newUser);                                
            }
        }
        #endregion

        #region ActionUpdateUserBdd
        private void ActionUpdateUserBdd(object obj)
        {
            if (ItemsAdmin == true)
                Profil = 1;
            if (ItemsN1 == true)
                Profil = 2;
            if (ItemsSalarie == true)
                Profil = 3;

            Users newUser = new Users(idUser, Nom, Prenom, Email, Password, CongesRestant,RecuperationRestante, Profil, Centre);
            CrudUsers verificationUser = new CrudUsers();
            verificationUser.UpdateUser(newUser);
        }
        #endregion       
    }
}

