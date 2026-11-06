using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace _21_gewinnt
{
    internal class Spieler
    {
        public string sName;
        public int iAlter;
        public string sUser;

        public Spieler(string name, int alter, string user)
        {
            sName = name;
            iAlter = alter;
            sUser = user;
        }
        public Spieler() {           }

    }
}

