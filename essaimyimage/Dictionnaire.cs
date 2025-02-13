using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace essaimyimage
{
    internal class Dictionnaire
    {
        private Pixel clé;
        private int occurence;
        
        public Dictionnaire (Pixel cle,int occurence)
        {
            this.clé = cle;
            this.occurence = occurence;
        }
        public Pixel Key
        {
            get { return clé; }
            set
            {
                clé = value;
            }
        }
        public int Occ
        {
            get { return occurence; }
            set
            {
                occurence = value;
            }
        }
    }
}
