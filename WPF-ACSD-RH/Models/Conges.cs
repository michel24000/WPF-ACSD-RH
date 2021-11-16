using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_ACSD_RH.Models
{
    class Conges
    {
        #region VARIABLES
        private int idConges;
        private int emetteur;
        private string dateDebut;
        private string dateFin;
        private int nbJoursConges;
        private int nbJoursRecuperations;
        private string decision;
        private string motifRefus;
        private string nom;
        private string prenom;
        private string etablissement;
        #endregion

        #region SETTER_GETTER
        public int IdConges { get => idConges; set => idConges = value; }
        public int Emetteur { get => emetteur; set => emetteur = value; }
        public string DateDebut { get => dateDebut; set => dateDebut = value; }
        public string DateFin { get => dateFin; set => dateFin = value; }
        public int NbJoursConges { get => nbJoursConges; set => nbJoursConges = value; }
        public int NbJoursRecuperations { get => nbJoursRecuperations; set => nbJoursRecuperations = value; }
        public string Decision { get => decision; set => decision = value; }
        public string MotifRefus { get => motifRefus; set => motifRefus = value; }
        public string Nom { get => nom; set => nom = value; }
        public string Prenom { get => prenom; set => prenom = value; }
        public string Etablissement { get => etablissement; set => etablissement = value; }
        #endregion

        public Conges(string _etablissement,string _nom, string _prenom, int _idConges, int _emetteur, string _dateDebut, string _dateFin, int _nbJoursConges, int _nbJoursRecuperations, string _decision, string _motifRefus)
        {
            etablissement = _etablissement;
            nom = _nom;
            prenom = _prenom;
            idConges = _idConges;
            emetteur = _emetteur;
            dateDebut = _dateDebut;
            dateFin = _dateFin;
            nbJoursConges = _nbJoursConges;
            nbJoursRecuperations = _nbJoursRecuperations;
            decision = _decision;
            motifRefus = _motifRefus;
            
        }

        public Conges(int _emetteur, string _dateDebut, string _dateFin, int _nbJoursConges, int _nbJoursRecuperations, string _decision, string _motifRefus)
        {
            emetteur = _emetteur;
            dateDebut = _dateDebut;
            dateFin = _dateFin;
            nbJoursConges = _nbJoursConges;
            nbJoursRecuperations = _nbJoursRecuperations;
            decision = _decision;
            motifRefus = _motifRefus;            
        }

        public Conges(int _emetteur)
        {
            emetteur = _emetteur;            
        }
    }
}
