using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPF_ACSD_RH.Models
{
    class BddMysql
    {
        #region Constantes et variable de connexion
        // connexion bdd sur OVH
        //private string servername = "centressappli762.mysql.db";
        //private string nomBase = "centressappli762";
        //private string user = "centressappli762";
        //private string pass = "APPLI2021douai";
        //private int port = 3306;

        // connexion bdd sur wampserver
        private string servername = "localhost";
        private string nomBase = "centressappli762";
        private string user = "root";
        private string pass = "";
        private int port = 3306;

        private MySqlConnection Connexion;                
        private String ChaineConnexion;
        private string _Erreur;
        private bool EstConnecte;        
        #endregion        

        #region BddMysql()
        public BddMysql()
        {            
            ChaineConnexion = "Server=" + servername +
                              ";Database=" + nomBase +
                              ";port=" + port +
                              ";User Id=" + user +
                              ";password=" + pass +
                              ";Charset=utf8";

            Connexion = new MySqlConnection(ChaineConnexion); // Création de l'objet Connexion
            EstConnecte = false;                              // Connexion fermée par défaut
        }
        #endregion       

        #region bool OuvrirConnexion()
        public bool OuvrirConnexion()
        {
            try
            {
                Connexion.Open();
                EstConnecte = true;
            }
            catch (Exception Er)
            {
                _Erreur = Er.Message;
            }
            return EstConnecte;
        }
        #endregion

        #region bool FermerConnexion()
        public bool FermerConnexion()
        {
            try
            {
                Connexion.Close();
                EstConnecte = false;
            }
            catch (Exception Er)
            {
                _Erreur = Er.Message;
            }
            return EstConnecte;
        }
        #endregion        

        #region MySqlDataReader RequeteSql(String Requete)
        public MySqlDataReader RequeteSql(String Requete)
        {
            MySqlCommand CmdSql = new MySqlCommand(Requete, Connexion);
            MySqlDataReader Reader = null;

            if (EstConnecte == true)
            {
                try
                {
                    Reader = CmdSql.ExecuteReader();
                }
                catch (Exception Er)
                {
                    _Erreur = Er.Message;
                }
            }
            return Reader;
        }
        #endregion

        #region int RequeteNoData(String Requete)
        public int RequeteNoData(String Requete)
        {
            MySqlCommand CmdSqlNoData = new MySqlCommand(Requete, Connexion);
            int NbLignesModifiees = 0;

            if (EstConnecte == true)
            {
                try
                {
                    NbLignesModifiees = CmdSqlNoData.ExecuteNonQuery();
                }
                catch (MySqlException Er)
                {
                    _Erreur = Er.Message;
                }
            }
            return NbLignesModifiees;
        }
        #endregion                       
    }
}
