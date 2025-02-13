using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace essaimyimage
{
    internal class Arbre
    {
        private Noeud racine;
        private List<Dictionnaire> dictionnaire;

        public Arbre (Noeud racine, List<Dictionnaire> dictionnaire)
        {
            this.racine = racine;
            this.dictionnaire = dictionnaire;
        }
        public List<Dictionnaire> Dico(MyImage image)
        {
            List<Dictionnaire> dictionnaire = null;
            Dictionnaire test= null;
            int compteur = 0;
            int h = 0;
            
            for (int i = 0;i<image.Ha;i++)
            {
                for (int j = 0;j<image.La;j++)
                {
                    for (int k=0;k<dictionnaire.Count;k++)
                    {
                        if (dictionnaire[k].Key == image.Im[i,j])
                        {
                            compteur++;
                            h = k;
                            
                        }
                        
                    }
                    if (compteur !=0)
                    {
                        dictionnaire[h].Occ++;
                    }
                    else
                    {
                        test.Key = image.Im[i, j];
                        test.Occ = 1;
                        dictionnaire.Add(test);
                    }
                    compteur = 0;
                    h = 0;
                }
            }
            return dictionnaire;
        }
    }
}
