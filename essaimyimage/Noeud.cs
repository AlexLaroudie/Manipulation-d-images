using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace essaimyimage
{
    internal class Noeud
    {
        private Pixel pixel;
        private Noeud noeudG;
        private Noeud noeudD;
        private int frequence;

        public Noeud(Pixel pixel, Noeud noeudG, Noeud noeudD, int frequence)
        {
            this.pixel = pixel;
            this.noeudG = noeudG;
            this.noeudD = noeudD;
            this.frequence = frequence;
        }
        public Pixel Pixel {
            get { return pixel; }
            set
            {
                pixel = value;
            } }
        public Noeud NoeudG
        {
            get { return noeudG; }
            set
            {
                noeudG = value;
            }
        }
        public Noeud NoeudD { get { return noeudD;} 
            set { noeudD = value; } }
        public int Frequence { get { return frequence;  }set { frequence = value; } }  
    }
}
