using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_ACSD_RH.Models
{
    public class Users
    {
        private int idUser;
        private string nom;
        private string prenom;
        private string email;
        private string password;
        private int congesRestant;
        private int recuperationRestante;
        private int profil;
        private int idCentre;

        public int IdUser { get => idUser; set => idUser = value; }
        public string Nom { get => nom; set => nom = value; }
        public string Prenom { get => prenom; set => prenom = value; }
        public string Email { get => email; set => email = value; }
        public string Password { get => password; set => password = value; }
        public int CongesRestant { get => congesRestant; set => congesRestant = value; }
        public int Profil { get => profil; set => profil = value; }
        public int IdCentre { get => idCentre; set => idCentre = value; }
        public int RecuperationRestante { get => recuperationRestante; set => recuperationRestante = value; }

        public Users()
        {

        }

        //constructeur utilisé pour envoyé en paramétre vers la view Accueil
        public Users(int id, string _nom, string _prenom, string mail, string pass,int _congesRestant, int _recuperationRestante, int _profil, int _idCentre)
        {
            idUser = id;
            nom = _nom;
            prenom = _prenom;
            email = mail;
            password = pass;
            congesRestant = _congesRestant;
            recuperationRestante = _recuperationRestante;
            profil = _profil;
            idCentre = _idCentre;
        }

        //constructeur utilisé view accueil code behind pour envoyer en paramétre a CongesViewModel
        public Users(int id, string _nom, string _prenom, string mail, int _profil, int _idCentre)
        {
            idUser = id;
            nom = _nom;
            prenom = _prenom;
            email = mail;            
            profil = _profil;
            idCentre = _idCentre;
        }


        //constructeur utilisé sur AdminViewModel pour remplir la liste pour le binding du combobox des utilisateurs enregistré
        //constructeur utilisé sur AdminViewModel pour la mise a jour d'un utilisateur en bdd
        public Users(int id, string _nom, string _prenom, string mail, string pass, int _profil)
        {
            idUser = id;
            nom = _nom;
            prenom = _prenom;
            email = mail;
            password = pass;
            profil = _profil;
        }

        
        public Users(int id, string _nom, string _prenom, string mail, int _profil)
        {
            idUser = id;
            nom = _nom;
            prenom = _prenom;
            email = mail;
            profil = _profil;
        }

        //Constructeur utilisé pour la création d'un salarié en bdd sur AdminViewModel
        public Users(string _nom, string _prenom, string mail, string pass, int _congesRestant, int _recuperationRestante, int _profil, int _idCentre)
        {            
            nom = _nom;
            prenom = _prenom;
            email = mail;            
            password = pass;
            congesRestant = _congesRestant;
            recuperationRestante = _recuperationRestante;
            profil = _profil;
            idCentre = _idCentre;
            
        }

        public Users(string _nom, string _prenom, string mail, string pass)
        {
            nom = _nom;
            prenom = _prenom;
            email = mail;
            password = pass;
        }

        public Users(string _nom, string _prenom, string mail)
        {
            nom = _nom;
            prenom = _prenom;
            email = mail;
        }

        //constructeur utilisé pour l'authentification
        public Users(string pass, string mail)
        {
            Password = pass;
            Email = mail;
        }

        //constructeur utilisé pour la recherche du n+1 du centre de la personne qui envoi une demande de congés
        public Users(int _profil, int _idCentre)
        {           
            profil = _profil;
            idCentre = _idCentre;
        }

        //constructeur utilisé sur AdminViewModel pour la suppression d'un utilisateur enregistré
        public Users(int id)
        {
            idUser = id; 
        }

        public Users(string mail)
        {
            Email = mail;
        }
    }
}
