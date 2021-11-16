using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WPF_ACSD_RH.Models
{
    class HachageMdp
    {
        public string Hash(string password)
        {            
            return BitConverter.ToString(new SHA512CryptoServiceProvider().ComputeHash(Encoding.Default.GetBytes(password))).Replace("-", String.Empty).ToLower();            
        }        
    }
}
