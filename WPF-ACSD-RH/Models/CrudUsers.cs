using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPF_ACSD_RH.Models
{
    class CrudUsers
    {
        #region public void AjouterUser(Users userAAjouter)
        public void AjouterUser(Users userAAjouter)
        {            
            BddMysql Bdd = new BddMysql();
            bool OuvertureOk = Bdd.OuvrirConnexion();

            if (OuvertureOk == true)
            {
                // préparation de la requête SQL
                string requete = "INSERT INTO user(Nom, Prenom, Email, Password, congesRestant,recuperationRestante, " +
                    "idProfil, idCentre) VALUES('" + userAAjouter.Nom + "', '" + userAAjouter.Prenom + "', '" + 
                    userAAjouter.Email + "', '" + userAAjouter.Password + "', '" + userAAjouter.CongesRestant + 
                    "', '" + userAAjouter.RecuperationRestante + "', '" + userAAjouter.Profil + "', '" 
                    + userAAjouter.IdCentre + "')";

                int NbrLignes = Bdd.RequeteNoData(requete);

                if (NbrLignes == 0)                
                    MessageBox.Show("L'écriture n'a pas fonctionné !");                
                else                
                    MessageBox.Show("L'écriture est bien effective !");                
            }
            else
            {
                MessageBox.Show("Veuillez vérifier votre connexion !");
            }
            Bdd.FermerConnexion();            
        }
        #endregion

        #region public void SupprimerUser(Users userASupprimer)
        public void SupprimerUser(Users userASupprimer)
        {            
            BddMysql Bdd = new BddMysql();
            bool OuvertureOk = Bdd.OuvrirConnexion();

            // préparation de la requête SQL
            string requete = "DELETE FROM user WHERE idUser ='" + userASupprimer.IdUser + "' ";

            if (OuvertureOk == true)
            {
                int NbrLignes = Bdd.RequeteNoData(requete);                

                if (NbrLignes == 0)                
                    MessageBox.Show("La suppression d'un compte n'est pas autorisée si une demande de congés est en cours !");                
                else                
                    MessageBox.Show("La suppression a bien fonctionné !");                
            }
            else
                MessageBox.Show("Veuillez vérifier votre connexion internet !");
            Bdd.FermerConnexion();
        }
        #endregion

        #region public void UpdateUser(Users userAUpdate)
        public void UpdateUser(Users userAUpdate)
        {            
            BddMysql Bdd = new BddMysql();
            bool OuvertureOk = Bdd.OuvrirConnexion();

            // préparation de la requête SQL
            string requete = "UPDATE `user` SET `Nom` = '" + userAUpdate.Nom + "', `Prenom` = '" + userAUpdate.Prenom + "', `Email` = '" +
            userAUpdate.Email + "', `congesRestant` = '" + userAUpdate.CongesRestant + "', `recuperationRestante` = '" + userAUpdate.RecuperationRestante + 
            "', `idprofil` = '" + userAUpdate.Profil + "' WHERE `user`.`idUser` = '" + userAUpdate.IdUser + "' ";

            if (OuvertureOk == true)
            {
                int NbrLignes = Bdd.RequeteNoData(requete);
                if (NbrLignes == 0)                
                    MessageBox.Show("La mise à jour a échoué !");                
                else                
                    MessageBox.Show("Mise à jour effectuée avec succès !");                
            }
            else
                MessageBox.Show("Veuillez vérifier votre connexion !");
            Bdd.FermerConnexion();
        }
        #endregion

        #region public List<string> LireUser(Users userALire)
        //utilisé pour la page d'authentification
        public List<string> LireUser(Users userALire)
        {
            List<string> resultat = new List<string>();
            BddMysql Bdd = new BddMysql();
            bool OuvertureOk = Bdd.OuvrirConnexion();

            if (OuvertureOk == true)
            {
                MySqlDataReader reader = Bdd.RequeteSql("SELECT * FROM user WHERE email = '" + userALire.Email + "' ");

                if (reader != null)
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            int nbrColonnes = reader.FieldCount;
                            string valeur = "";
                            for (int i = 0; i < nbrColonnes; i++)
                            {
                                valeur += reader.GetString(i) + "/";
                            }
                            resultat.Add(valeur);
                        }
                    }                    
                }
                reader.Close();
            }
            else            
                MessageBox.Show("La connexion a échoué, Veuillez vérifier votre connexion !");                      
            return resultat;
        }
        #endregion

        #region public List<string> LireUserId(Users userALire)
        public List<string> LireUserId(Users userALire)
        {
            List<string> resultat = new List<string>();
            BddMysql Bdd = new BddMysql();
            bool OuvertureOk = Bdd.OuvrirConnexion();

            if (OuvertureOk == true)
            {
                MySqlDataReader reader = Bdd.RequeteSql("SELECT * from user WHERE idUser = '" + userALire.IdUser + "' ");
                if (reader != null)
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            int nbrColonnes = reader.FieldCount;
                            string valeur = "";
                            for (int i = 0; i < nbrColonnes; i++)
                            {
                                valeur += reader.GetString(i) + "/";
                            }
                            resultat.Add(valeur);
                        }
                    }
                }
                reader.Close();
            }else
                MessageBox.Show("La connexion a échoué, veuillez vérifier votre connexion !");
            return resultat;
        }
        #endregion

        #region public List<string> LireUserAll()
        //utilisé pour la page admin
        public List<string> LireUserAll()
        {
            List<string> resultat = new List<string>();
            BddMysql Bdd = new BddMysql();
            bool OuvertureOk = Bdd.OuvrirConnexion();

            if (OuvertureOk == true)
            {
                MySqlDataReader reader = Bdd.RequeteSql("SELECT * FROM user ORDER BY Nom");
                if (reader != null)
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            int nbrColonnes = reader.FieldCount;
                            string valeur = "";
                            for (int i = 0; i < nbrColonnes; i++)
                            {
                                valeur += reader.GetString(i) + "/";
                            }
                            resultat.Add(valeur);
                        }
                    }
                }
                reader.Close();
            }
            else
                MessageBox.Show("La connexion a échoué, veuillez vérifier votre connexion !");
            return resultat;
        }
        #endregion

        #region public List<string> LireEmailN1(Users userALire)
        public List<string> LireEmailN1(Users userALire)
        {
            List<string> resultat = new List<string>();
            BddMysql Bdd = new BddMysql();
            bool OuvertureOk = Bdd.OuvrirConnexion();

            if (OuvertureOk == true)
            {
                MySqlDataReader reader = Bdd.RequeteSql("SELECT * from user WHERE idCentre = '" + userALire.IdCentre + "' AND idProfil = '" + userALire.Profil + "'");
                if (reader != null)
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            int nbrColonnes = reader.FieldCount;
                            string valeur = "";
                            for (int i = 0; i < nbrColonnes; i++)
                            {
                                valeur += reader.GetString(i) + "/";
                            }
                            resultat.Add(valeur);
                        }
                    }
                }
                reader.Close();
            }
            else
                MessageBox.Show("La connexion a échoué, veuillez vérifier votre connexion !");
            return resultat;
        }
        #endregion
    }
}
