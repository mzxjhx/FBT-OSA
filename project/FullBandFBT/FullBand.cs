using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CWDM1To4
{
    public class FullBand
    {
        int ch = 0;

        public int Ch
        {
            get { return ch; }
            set { ch = value; }
        }

        List<FBTBand> band = new List<FBTBand>();

        public List<FBTBand> Band
        {
            get { return band; }
            set { band = value; }
        }

        List<BandPar> par = new List<BandPar>();

        public List<BandPar> Par
        {
            get { return par; }
            set { par = value; }
        }


    }
}
