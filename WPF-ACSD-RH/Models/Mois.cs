using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_ACSD_RH.Models
{
    class Mois
    {
        private string nom;

        public string Nom { get => nom; set => nom = value; }

        public Mois(string _nom)
        {
            nom = _nom;
        }
    }
}
