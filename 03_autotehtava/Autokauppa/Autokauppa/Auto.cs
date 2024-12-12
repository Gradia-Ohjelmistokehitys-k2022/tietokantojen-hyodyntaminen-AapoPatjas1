using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace Auto
{
    internal class Auto
    {
        
        public int Id { get; set; }           // Taulun "Id" sarake
        public string Merkki { get; set; }    // Taulun "Merkki" sarake
        public string Malli { get; set; }     // Taulun "Malli" sarake
        public int ValmistusVuosi { get; set; } // Taulun "ValmistusVuosi" sarake
        public decimal Hinta { get; set; }    // Taulun "Hinta" sarake
    }
}
