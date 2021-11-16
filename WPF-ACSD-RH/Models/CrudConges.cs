using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPF_ACSD_RH.Models
{
    class CrudConges
    {
        #region public void ajouterConges(Conges congesAAjouter)
        public void ajouterConges(Conges congesAAjouter)
        {            
            BddMysql Bdd = new BddMysql();
            bool OuvertureOk = Bdd.OuvrirConnexion();

            if (OuvertureOk == true)
            {
                // préparation de la requête SQL
                string requete = "INSERT INTO conges(emetteur, dateDebut, dateFin, nbJoursConges, nbJoursRecuperations, decision, motifRefus) VALUES('" + congesAAjouter.Emetteur + "', '" + congesAAjouter.DateDebut +
                    "', '" + congesAAjouter.DateFin + "' , '" + congesAAjouter.NbJoursConges + "' , '" + congesAAjouter.NbJoursRecuperations + "' , '" + congesAAjouter.Decision + "' , '" + congesAAjouter.MotifRefus + "')";

                int NbrLignes = Bdd.RequeteNoData(requete);

                if (NbrLignes == 0)                
                    MessageBox.Show("L'envoie de votre demande a échoué, vérifiez votre connexion !");                
                else                
                    MessageBox.Show("Votre demande est envoyée !");                
            }
            else            
                MessageBox.Show("La connexion à la base de données n'a pas fonctionné");                     
        }
        #endregion

        #region public List<string> LireCongesSalarie(Conges congesALire)
        public List<string> LireCongesSalarie(Conges congesALire)
        {
            List<string> resultat = new List<string>();
            BddMysql Bdd = new BddMysql();            
            bool OuvertureOk = Bdd.OuvrirConnexion();
            String requete = string.Format("SELECT user.idCentre, user.Nom, user.Prenom, conges.* FROM user INNER JOIN conges WHERE " +
                "user.idUser = conges.emetteur AND user.idUser = {0}", congesALire.Emetteur);

            if (OuvertureOk == true)
            {
                MySqlDataReader reader = Bdd.RequeteSql(requete);
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

        #region public List<string> LireCongesN1(int _idCentre)
        public List<string> LireCongesN1(int _idCentre)
        {
            List<string> resultat = new List<string>();
            BddMysql Bdd = new BddMysql();
            bool OuvertureOk = Bdd.OuvrirConnexion();
            String requete = string.Format("SELECT user.idCentre, user.Nom, user.Prenom, conges.* FROM user INNER JOIN conges WHERE " +
                "user.idUser = conges.emetteur AND user.idCentre = {0} ORDER BY user.Nom", _idCentre);

            if (OuvertureOk == true)
            {
                MySqlDataReader reader = Bdd.RequeteSql(requete);
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

        #region public List<string> LireCongesAdmin()
        public List<string> LireCongesAdmin()
        {
            List<string> resultat = new List<string>();
            BddMysql Bdd = new BddMysql();
            bool OuvertureOk = Bdd.OuvrirConnexion();
            String requete = string.Format("SELECT user.idCentre, user.Nom, user.Prenom, conges.* FROM user INNER JOIN conges WHERE user.idUser = conges.emetteur ORDER BY user.Nom");

            if (OuvertureOk == true)
            {
                MySqlDataReader reader = Bdd.RequeteSql(requete);
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

        #region public void supprimerConges(int userASupprimer)
        public void supprimerConges(int userASupprimer)
        {            
            BddMysql Bdd = new BddMysql();
            bool OuvertureOk = Bdd.OuvrirConnexion();

            // préparation de la requête SQL
            string requete = "DELETE FROM conges WHERE idConges =" + userASupprimer;

            if (OuvertureOk == true)
            {
                int NbrLignes = Bdd.RequeteNoData(requete);                

                if (NbrLignes == 0)                
                    MessageBox.Show("La suppression n'a pas fonctionné !");
                else               
                    MessageBox.Show("Congés supprimé avec succès !");                                                  
            }
            else
                MessageBox.Show("La connexion a échoué, veuillez vérifier votre connexion !");
        }
        #endregion

        #region public void validerConges(int userAValider)
        public void validerConges(int userAValider)
        {            
            BddMysql Bdd = new BddMysql();
            bool OuvertureOk = Bdd.OuvrirConnexion();

            // préparation de la requête SQL
            string requete = "UPDATE `conges` SET `decision` = 'Validé', `motifRefus` = 'Aucun' WHERE `conges`.`idConges` ='" + userAValider + "' ";

            if (OuvertureOk == true)
            {
                int NbrLignes = Bdd.RequeteNoData(requete);                

                if (NbrLignes == 0)                
                    MessageBox.Show("La validation n'a pas fonctionné !");               
                else                
                    MessageBox.Show("Congés validé avec succès !");               
            }            
        }
        #endregion

        #region public void refuserConges(int userARefuser)
        public void refuserConges(int userARefuser)
        {           
            BddMysql Bdd = new BddMysql();
            bool OuvertureOk = Bdd.OuvrirConnexion();

            // préparation de la requête SQL
            string requete = "UPDATE `conges` SET `decision` = 'Refusé', `motifRefus` = 'Voir un supérieur' WHERE `conges`.`idConges` ='" + userARefuser + "' ";

            if (OuvertureOk == true)
            {
                int NbrLignes = Bdd.RequeteNoData(requete);                

                if (NbrLignes == 0)                
                    MessageBox.Show("Le refus de la demande de congés n'a pas fonctionné !");                
                else                
                    MessageBox.Show("Congés refusé avec succès !");                
            }            
        }
        #endregion

        #region public void AnnulerConges(int userAAnnuler)
        public void AnnulerConges(int userAAnnuler)
        {
            BddMysql Bdd = new BddMysql();
            bool OuvertureOk = Bdd.OuvrirConnexion();

            // préparation de la requête SQL
            string requete = "UPDATE conges SET decision = 'Annulé' WHERE conges.idConges ='" + userAAnnuler + "' ";

            if (OuvertureOk == true)
            {
                int NbrLignes = Bdd.RequeteNoData(requete);

                if (NbrLignes == 0)
                    MessageBox.Show("La demande d'annulation de la demande de congés n'a pas fonctionné !");
                else
                    MessageBox.Show("Demande d'annulation envoyée avec succès !");
            }
        }
        #endregion

        #region public List<string> FiltrageLireCongesAdmin()
        public List<string> FiltrageLireCongesAdmin(string _requete)
        {
            List<string> resultat = new List<string>();
            BddMysql Bdd = new BddMysql();
            bool OuvertureOk = Bdd.OuvrirConnexion();

            if (OuvertureOk == true)
            {
                MySqlDataReader reader = Bdd.RequeteSql(_requete);
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
