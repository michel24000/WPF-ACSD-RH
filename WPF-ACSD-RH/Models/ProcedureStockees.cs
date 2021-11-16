using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPF_ACSD_RH.Models
{
    class ProcedureStockees
    {
        string resultat;

        
        public string Synthese(string _requete)
        {            
            BddMysql Bdd = new BddMysql();
            bool OuvertureOk = Bdd.OuvrirConnexion();

            if (OuvertureOk == true)
            {
                MySqlDataReader reader = Bdd.RequeteSql(_requete);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int nbrColonnes = reader.FieldCount;

                        resultat = reader.GetString(0);
                    }
                }
            }
            return resultat;
        }
    }
}
