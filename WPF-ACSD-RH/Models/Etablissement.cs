using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_ACSD_RH.Models
{
    class Etablissement
    {
        private string nom;

        public string Nom { get => nom; set => nom = value; }

        public Etablissement(string _nom)
        {
            nom = _nom;
        }
    }
}
