using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_ACSD_RH.Models
{
    class Decision
    {
        private string nom;

        public string Nom { get => nom; set => nom = value; }

        public Decision(string _nom)
        {
            nom = _nom;
        }
    }
}
