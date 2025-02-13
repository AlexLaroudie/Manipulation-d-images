using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using essaimyimage;
using static System.Net.Mime.MediaTypeNames;
using System;
using System.Runtime.InteropServices.ComTypes;

namespace essaimyimage
{
    public class MyImage
    {
        private string type;
        private int taille;
        private int taille_off;
        private int larg;
        private int haut;
        private int Nb_im;
        private Pixel[,] image;
        private string nomfichier;

        public static int Convertir_Endian_To_Int(byte[] tab)
        {
            int val = 0;
            double n = 0;
            for (int i = 0; i < tab.Length; i++)
            {
                n = Math.Pow(256, i);
                val += Convert.ToInt32(tab[i] * n);
            }
            return val;
        }

        public static string Convertir_ASCII_To_String(byte[] tab)
        {
            string s = "";
            for (int i = 0; i < tab.Length; i++)
            {
                switch (tab[i])
                {
                    case 65:
                        s += 'A';
                        break;
                    case 66:
                        s += 'B';
                        break;
                    case 67:
                        s += 'C';
                        break;
                    case 68:
                        s += 'D';
                        break;
                    case 69:
                        s += 'E';
                        break;
                    case 70:
                        s += 'F';
                        break;
                    case 71:
                        s += 'G';
                        break;
                    case 72:
                        s += 'H';
                        break;
                    case 73:
                        s += 'I';
                        break;
                    case 74:
                        s += 'J';
                        break;
                    case 75:
                        s += 'K';
                        break;
                    case 76:
                        s += 'L';
                        break;
                    case 77:
                        s += 'M';
                        break;
                    case 78:
                        s += 'N';
                        break;
                    case 79:
                        s += 'O';
                        break;

                    case 80:
                        s += 'P';
                        break;
                    case 81:
                        s += 'Q';
                        break;
                    case 82:
                        s += 'R';
                        break;
                    case 83:
                        s += 'S';
                        break;
                    case 84:
                        s += 'T';
                        break;
                    case 85:
                        s += 'U';
                        break;
                    case 86:
                        s += 'V';
                        break;
                    case 87:
                        s += 'W';
                        break;
                    case 88:
                        s += 'X';
                        break;
                    case 89:
                        s += 'Y';
                        break;
                    case 90:
                        s += 'Z';
                        break;

                    default:
                        break;
                }
            }
            return s;
        }
        public static byte[] Convertir_Int_To_Endian(int val, int x)
        {
            byte[] tab = new byte[x];
            int n = 0;
            for (int i = 0; i < tab.Length; i++)
            {
                n = val % 256;
                val /= 256;
                if ((n < 256) && (n >= 0))
                {
                    tab[i] = Convert.ToByte(n);
                }
                else
                {
                    tab[i] = 0;
                }
            }
            return tab;
        }



        public MyImage(string nomFichier)
        {
            this.nomfichier = nomFichier;
            byte[] myfile = File.ReadAllBytes(nomfichier);
            byte[] tab_type = new byte[2];
            byte[] tab_taille = new byte[4];
            byte[] tab_taille_off = new byte[4];
            byte[] tab_larg = new byte[4];
            byte[] tab_haut = new byte[4];
            byte[] Nb_im = new byte[2];
            int a = 0;

            for (int i = 0; i < 6; i++)
            {

                if (i < 2) { tab_type[i] = myfile[i]; }
                else { tab_taille[i - 2] = myfile[i]; }
            }
            for (int i = 14; a < 16; i++)
            {
                if (a < 4) { tab_taille_off[a] = myfile[i]; }
                else if (a < 8) { tab_larg[a - 4] = myfile[i]; }
                else if (a < 12) { tab_haut[a - 8] = myfile[i]; }
                else if (a >= 14)
                {
                    Nb_im[a - 14] = myfile[i];
                }
                a++;
            }

            this.type = Convertir_ASCII_To_String(tab_type);
            this.taille = Convertir_Endian_To_Int(tab_taille);
            this.taille_off = Convertir_Endian_To_Int(tab_taille_off);
            this.larg = Convertir_Endian_To_Int(tab_larg);
            this.haut = Convertir_Endian_To_Int(tab_haut);
            this.Nb_im = Convertir_Endian_To_Int(Nb_im);
            Pixel[] tab_p = new Pixel[myfile.Length - 54];
            int k = 0;
            for (int i = 0; i < myfile.Length - 54; i = i + 3)
            {
                tab_p[k] = new Pixel(myfile[i + 54], myfile[i + 1 + 54], myfile[i + 2 + 54]);
                k++;
            }
            Pixel[,] mat_p = new Pixel[haut, larg];
            k = 0;
            for (int i = 0; i < haut; i++)
            {
                for (int j = 0; j < larg; j++)
                {
                    mat_p[i, j] = tab_p[k];
                    k++;
                }
            }
            this.image = mat_p;
        }
        public string Ty
        {
            get { return type; }
            set { type = value; }
        }
        public int Ta
        {
            get { return taille; }

            set { taille = value; }
        }
        public int Ta_off
        {
            get { return taille_off; }

            set { taille_off = value; }
        }
        public int La
        {
            get { return larg; }

            set { larg = value; }
        }
        public int Ha
        {
            get { return haut; }

            set { haut = value; }
        }
        public int Nb
        {
            get { return Nb_im; }

            set { Nb_im = value; }
        }
        public Pixel[,] Im
        {
            get { return image; }

            set { image = value; }
        }

        public Pixel GetPixel(int x, int y)
        {
            return image[x, y];
        }
        public void From_Image_To_File(string fichier)
        {
            List<byte[]> list_tab_byte = new List<byte[]>();  /// Liste detableau de byte pour faciliter les manips qui servira  à creer le fichier final

            byte[] tab_vide = { 0, 0, 0, 0 };                               /// on aura besoin de remplir plusieurs par des 0 alors on met un tableau qu'on reutilisera
            byte[] typ = { 66, 77 };                                     ///format ASCII
            list_tab_byte.Add(typ);                            /// byte pour le type de fichier en fonction de l'instance myimage

            list_tab_byte.Add(Convertir_Int_To_Endian(taille, 4));            /// on convertit pour avoir la taille du fichier
            list_tab_byte.Add(tab_vide);                                   /// tableau vide pour obtenir les 14 octets
            list_tab_byte.Add(Convertir_Int_To_Endian(taille_off, 4));       /// taille offset
            byte[] header = { 40, 0, 0, 0 };
            list_tab_byte.Add(header);                                 /// on alloue la place de l'entete
            list_tab_byte.Add(Convertir_Int_To_Endian(larg, 4));       /// la largeur de l'image
            list_tab_byte.Add(Convertir_Int_To_Endian(haut, 4));           /// la haut de l'image

            byte[] remplissage = { 0, 0 };
            list_tab_byte.Add(remplissage);

            list_tab_byte.Add(Convertir_Int_To_Endian(Nb_im, 2));              /// le nombre de bit par pixel
            list_tab_byte.Add(tab_vide);
            list_tab_byte.Add(Convertir_Int_To_Endian(haut * larg * Nb_im, 4)); /// sur 4 octets la taille alloué pour les pixel de l'image
            list_tab_byte.Add(tab_vide);
            list_tab_byte.Add(tab_vide);
            list_tab_byte.Add(tab_vide);
            list_tab_byte.Add(tab_vide);

            for (int l = 0; l < haut; l++)
            {
                for (int c = 0; c < larg; c++)                    /// on remplit les pixels case par case avec les données RGB
                {
                    byte[] tab_p = { image[l, c].R, image[l, c].G, image[l, c].B };
                    list_tab_byte.Add(tab_p);
                }
            }
            List<byte> list_byte = new List<byte>();
            for (int l = 0; l < list_tab_byte.Count(); l++) /// pour acceder aux informations du tableau de byte plus facilement on le met sous la forme d'une liste de byte
            {
                for (int c = 0; c < list_tab_byte[l].Length; c++)
                {
                    list_byte.Add(list_tab_byte[l][c]);
                }
            }
            byte[] tab_byte = list_byte.ToArray();          /// on créé un fichier à partir de la liste
            File.WriteAllBytes(fichier, tab_byte);

        }

        public MyImage Quart()    /// le but est d'avoir le coin supérieur gauche d'une image en version plus petite
        {
            string fichier = "quartpivot.bmp";                /// fichier pivot pour les modifications
            From_Image_To_File(fichier);
            MyImage quart = new MyImage(fichier);
            quart.larg = larg / 2;
            quart.haut = haut / 2;
            quart.taille = 54 + 4 * quart.La * quart.Ha;
            quart.image = new Pixel[quart.Ha, quart.La];
            int k = haut / 2;                                 /// on remplit en partant de haut/2 pour avoir le coin supérieur droit
            for (int i = 0; i < quart.Ha; i++)
            {
                for (int j = 0; j < quart.La; j++)   
                {
                    quart.image[i, j] = image[k, j];
                }
                k++;
            }
            return quart;
        }
        public MyImage NoiretBlanc()   
        {
            string fichier = "quartpivot.bmp";                
            From_Image_To_File(fichier);
            MyImage quart = new MyImage(fichier);
            quart.larg = larg ;
            quart.haut = haut ;
            quart.taille = 54 + 4 * quart.La * quart.Ha;
            quart.image = new Pixel[quart.Ha, quart.La];
                                            
            for (int i = 0; i < quart.Ha; i++)
            {
                for (int j = 0; j < quart.La; j++)                                         /// si la moyenne des valeurs RGB dépasse une certaine valeur on lui associe une nuance de gris
                {
                    if ((image[i, j].B + image[i, j].R + image[i,j].G)/3>=220)
                    {
                        quart.image[i, j] = new Pixel (255, 255, 255);
                    }
                    else
                    {

                        if ((image[i, j].B + image[i, j].R + image[i, j].G) / 3 >= 180)
                        {
                            quart.image[i, j] = new Pixel(190, 190, 190);
                        }
                        else
                        {
                            if ((image[i, j].B + image[i, j].R + image[i, j].G) / 3 >= 140)
                            {
                                quart.image[i, j] = new Pixel(150, 150, 150);
                            }
                            else
                            {
                                if ((image[i, j].B + image[i, j].R + image[i, j].G) / 3 >= 100)
                                {
                                    quart.image[i, j] = new Pixel(105, 105, 105);
                                }
                                else
                                {
                                    if ((image[i, j].B + image[i, j].R + image[i, j].G) / 3 >= 60)
                                    {
                                        quart.image[i, j] = new Pixel(75, 75, 75);
                                    }
                                    else
                                    {
                                        if ((image[i, j].B + image[i, j].R + image[i, j].G) / 3 >= 20)
                                        {
                                            quart.image[i, j] = new Pixel(35, 35, 35);
                                        }
                                        else
                                        {
                                            quart.image[i, j] = new Pixel(0, 0, 0);
                                        }
                                    }
                                }
                            }
                        }







                    }
                    
                }
                
            }
            return quart;
        }

        public MyImage Aggrandissement(int coeff)
        {
            string fichier = "cocopivot.bmp";
            From_Image_To_File(fichier);
            MyImage agg = new MyImage(fichier);
            agg.larg = larg * coeff;
            agg.haut = haut * coeff;
            agg.taille = 54 + 4 * agg.La * agg.Ha;
            agg.image = new Pixel[agg.Ha, agg.La];
            for (int i = 0; i < agg.Ha; i++)
            {
                for (int j = 0; j < agg.La; j++)
                {
                    agg.image[i, j] = image[i / coeff, j / coeff];
                }
            }
            return agg;
        }
        public MyImage Rotation90(int degre)
        {
            string fichier = "rotapivot.bmp";
            From_Image_To_File(fichier);
            MyImage rota = new MyImage(fichier);         /// on change la taille en fonction de la rotation qu'on veut faire (format portrait ou paysage)
            if (degre == 90 || degre == 270)
            {
                rota.larg = haut;
                rota.haut = larg;
                rota.taille = taille;
                rota.image = new Pixel[rota.Ha, rota.La];

            }
            if (degre == 180)
            {
                rota.larg = larg;
                rota.haut = haut;
                rota.taille = taille;
                rota.image = new Pixel[rota.Ha, rota.La];
                for (int i =0;i<haut;i++)
                {
                    for (int j =0;j<larg;j++)
                    {
                        rota.image[i, j] = image[haut - i-1, larg-1 - j];
                    }
                }
            }
            if (degre == 90)
            {
                for (int i = 0; i < haut; i++)
                {
                    for (int j = 0; j < larg; j++)
                    {
                        rota.image[larg - 1 - j, i] = image[i, j];
                    }
                }
            }
            if (degre == 270)
            {
                for (int i = 0; i < haut; i++)
                {
                    for (int j = 0; j < larg; j++)
                    {
                        rota.image[j, haut-1-i] = image[i, j];
                    }
                }
            }
            return rota;
        }
        
        public MyImage RotationQuelqonque(int degre)
        {
            string fichier = "cocopivot.bmp";
            From_Image_To_File(fichier);
            MyImage rota = new MyImage(fichier);         /// on creer une image plus grande pouvant porter l'image en rotation 
            
            double radian = degre * Math.PI / 180.0;
           
            double cos = Math.Cos(radian);
            double sin = Math.Sin(radian);
            rota.larg = Convert.ToInt32(larg * Math.Abs(cos) + haut * Math.Abs(sin));
            rota.haut = Convert.ToInt32(larg * Math.Abs(sin) + haut * Math.Abs(cos));
            rota.larg = rota.larg + 4 - rota.larg % 4;
            rota.haut = rota.haut + 4 - rota.haut % 4;
            rota.taille = 54 + 4 * rota.larg * rota.haut;
            rota.image = new Pixel[rota.Ha, rota.La];


            
            int milieu_l = haut / 2;                  
            int milieu_c = larg / 2;
            int milieu_L = rota.haut / 2;
            int milieu_C = rota.larg / 2;

            
            for (int l = 0; l < haut; l++)
            {
                for (int c = 0; c < larg; c++)
                {
                    
                    int new_l = Convert.ToInt32(Math.Round((l - milieu_l) * cos - (c - milieu_c) * sin)) + milieu_L;
                    int new_c = Convert.ToInt32(Math.Round((l - milieu_l) * sin + (c - milieu_c) * cos)) + milieu_C;
                   
                    if (new_l >= 0 && new_l < rota.haut && new_c >= 0 && new_c < rota.larg)
                    {
                        
                        rota.image[new_l, new_c] = image[l, c];
                    }
                }
            }
            Pixel p = new Pixel(0, 0, 0);            /// permet de remplir les pixels vide par des pixels noirs pour éviter les erreurs de compilation
            for (int i = 0; i < rota.haut; i++)
            {
                for (int j = 0; j < rota.larg; j++)
                {
                    if (rota.image[i, j] == null)
                    {
                        rota.image[i, j] = p;
                    }
                }
            }

            return rota;
        }


        public MyImage Convolution(int[,] kernel)
        {
            string fichier = "cococonvo.bmp";                /// fichier pivot pour les modifications
            From_Image_To_File(fichier);
            MyImage convo = new MyImage(fichier);
            convo.larg = larg;
            convo.haut = haut;
            convo.taille = taille;
            convo.image = new Pixel[convo.Ha, convo.La];

            
            for (int x = 1; x < haut - 1; x++)
            {
                for (int y = 1; y < larg - 1; y++)
                {
                    
                    int sumR = 0;
                    int sumG = 0;
                    int sumB = 0;
                    for (int i = -1; i <= 1; i++)
                    {
                        for (int j = -1; j <= 1; j++)
                        {
                            sumR += (kernel[i + 1, j + 1] * image[x + i, y + j].R);
                            sumG += (kernel[i + 1, j + 1] * image[x + i, y + j].G);
                            sumB += (kernel[i + 1, j + 1] * image[x + i, y + j].B);
                        }
                    }
                    if (sumR < 0) { sumR = 0; }
                    if (sumG < 0) { sumG = 0; }
                    if (sumB < 0) { sumB = 0; }
                    if (sumR > 255) { sumR = 255; }
                    if (sumG > 255) { sumG = 255; }
                    if (sumB > 255) { sumB = 255; }
                    
                    convo.image[x, y] = new Pixel(Convert.ToByte(sumR), Convert.ToByte(sumG), Convert.ToByte(sumB));

                }
            }
            Pixel pi = new Pixel(0, 0, 0);
            for (int i = 0; i < convo.haut; i++)
            {
                for (int j = 0; j < convo.larg; j++)
                {
                    if (convo.image[i, j] == null)
                    {
                        convo.image[i, j] = pi;
                    }
                }
            }
            return convo;
        }
        public MyImage Flou(double[,] kernel)
        {
            string fichier = "cocoflou.bmp";                // fichier pivot pour les modifications
            From_Image_To_File(fichier);
            MyImage convo = new MyImage(fichier);
            convo.larg = larg;
            convo.haut = haut;
            convo.taille = taille;
            convo.image = new Pixel[convo.Ha, convo.La];

            
            for (int l = 1; l < haut - 1; l++)
            {
                for (int c = 1; c < larg - 1; c++)
                {
                    
                    double R = 0;
                    double G = 0;
                    double B = 0;
                    for (int i = -1; i <= 1; i++)
                    {
                        for (int j = -1; j <= 1; j++)
                        {
                            R += (kernel[i + 1, j + 1] * image[l + i, c + j].R);
                            G += (kernel[i + 1, j + 1] * image[l + i, c + j].G);
                            B += (kernel[i + 1, j + 1] * image[l + i, c + j].B);
                        }
                    }
                    if (R < 0) { R = 0; }
                    if (G < 0) { G = 0; }
                    if (B < 0) { B = 0; }
                    if (R > 255) { R = 255; }
                    if (G > 255) { G = 255; }
                    if (B > 255) { B = 255; }

                    convo.image[l, c] = new Pixel(Convert.ToByte(R), Convert.ToByte(G), Convert.ToByte(B));

                }
            }
            Pixel pi = new Pixel(0, 0, 0);
            for (int i = 0; i < convo.haut; i++)
            {
                for (int j = 0; j < convo.larg; j++)
                {
                    if (convo.image[i, j] == null)
                    {
                        convo.image[i, j] = pi;
                    }
                }
            }
            return convo;
        }
        public MyImage Fractale()
        {
            string fichier = "fractale.bmp";
            From_Image_To_File(fichier);
            MyImage fractale = new MyImage(fichier);
            fractale.larg = larg;
            fractale.haut = haut;
            fractale.taille = taille;
            fractale.image = new Pixel[fractale.Ha, fractale.La];

            
            double x_min = -2;                     /// borne du repere
            double x_max = 0.5;
            double y_min = -1.25;
            double y_max = 1.25;
                                                     /// nb d'itérations
            int max_i = 50;

            
            for (int x = 0; x <haut; x++)
            {
                for (int y = 0; y < larg; y++)
                {
                    double cx = (x * (x_max - x_min) / haut + x_min);
                    double cy = (y * (y_min - y_max) / larg + y_max);
                    double xn = 0;
                    double yn = 0;
                    int n = 0;

                    while (((xn * xn + yn * yn) < 4) && (n < max_i))
                    {
                        double x_n_moins_1 = xn;
                        double y_n_moins_1 = yn;
                      
                        xn = x_n_moins_1 * x_n_moins_1 - y_n_moins_1 * y_n_moins_1 + cx;
                       
                        yn = 2 * x_n_moins_1 * y_n_moins_1 + cy;

                        n++;
                    }

                    if (n == max_i)
                    {
                        
                        fractale.image[x, y] = new Pixel(0, 0, 0);
                    }
                    else
                    {
                      
                        fractale.image[x, y] = new Pixel(Convert.ToByte((3 * n) % 256), Convert.ToByte((1 * n) % 256), Convert.ToByte((10 * n) % 256));
                    }
                }
            }
            return fractale;
        }
        public static int[] Convertir_Byte_To_OcetBinaire(byte b)
        {
            int[] res_binaire = new int[8];

            for (int i = 7; i >= 0; i--)
            {
                res_binaire[i] = (b & 1);      //recupère le bit de poid faible et le met tout à droite dans le tableau
                b >>= 1;                       //decale d'une case vers la droite
            }

            return res_binaire;
        }

        public static byte Convert_OctetBinaire_To_Byte(int[] octet)
        {
            byte x = 0;
            int j = 0;
            for (int i = 7; i >= 0; i--)
            {
                x = (byte)(x + octet[i] * (Math.Pow(2, j)));
                j++;
            }
            return x;
        }

        public MyImage Steno(MyImage imagecaché)
        {
            string fichier = "steno.bmp";
            From_Image_To_File(fichier);
            MyImage steno = new MyImage(fichier);
            steno.larg = larg;
            steno.haut = haut;
            steno.taille = taille;
            steno.image = new Pixel[steno.Ha, steno.La];

            int[] aR = new int[8];
            int[] aG = new int[8];
            int[] aB = new int[8];
            int[] bR = new int[8];
            int[] bG = new int[8];
            int[] bB = new int[8];
            int[] cR = new int[8];
            int[] cG = new int[8];
            int[] cB = new int[8];

            int n = 0;

            for (int i = 0; i < imagecaché.Ha; i++)
            {
                for (int j = 0; j < imagecaché.La; j++)
                {
                    aR = Convertir_Byte_To_OcetBinaire(image[i, j].R);
                    bR = Convertir_Byte_To_OcetBinaire(imagecaché.image[i, j].R);
                    aG = Convertir_Byte_To_OcetBinaire(image[i, j].G);
                    bG = Convertir_Byte_To_OcetBinaire(imagecaché.image[i, j].G);
                    aB = Convertir_Byte_To_OcetBinaire(image[i, j].B);
                    bB = Convertir_Byte_To_OcetBinaire(imagecaché.image[i, j].B);
                    for (int k = 0; k < 4; k++)
                    {
                        cR[k] = aR[k];
                        cG[k] = aG[k];
                        cB[k] = aB[k];
                    }
                    for (int p = 4; p < 8; p++)
                    {
                        cR[p] = bR[n];
                        cG[p] = bG[n];
                        cB[p] = bB[n];
                        n++;
                    }
                    n = 0;
                    steno.image[i, j] = new Pixel(Convert_OctetBinaire_To_Byte(cR), Convert_OctetBinaire_To_Byte(cG), Convert_OctetBinaire_To_Byte(cB));
                }
            }
          
            for (int i = 0; i < steno.haut; i++)
            {
                for (int j = 0; j < steno.larg; j++)
                {
                    if (steno.image[i, j] == null)
                    {
                        steno.image[i, j] = image[i,j];
                    }
                }
            }
            return steno;
        }

        public MyImage MirroirBasGauche()
        {
            string fichier = "mirroir.bmp";
            From_Image_To_File(fichier);
            MyImage agg = new MyImage(fichier);
            agg.larg = larg ;
            agg.haut = haut ;
            agg.taille = 54 + 4 * agg.La * agg.Ha;
            agg.image = new Pixel[agg.Ha, agg.La];
            
            for (int i = 0; i < agg.Ha/2; i++)
            {
                for (int j = 0; j < agg.La / 2; j++)
                {
                    agg.image[i, j] = image[i, j];
                }
            }
            for (int i = 0; i < agg.Ha/2; i++)
            {
                for (int j = agg.La / 2; j < agg.La; j++)
                {
                    agg.image[i, j] = image[i, agg.La - j];
                }
            }
            for (int i = agg.Ha/2; i < agg.Ha ; i++)
            {
                for (int j = 0; j < agg.La / 2; j++)
                {
                    agg.image[i, j] = image[agg.Ha-i, j];
                }
            }
            for (int i = agg.Ha / 2; i < agg.Ha; i++)
            {
                for (int j = agg.La/2; j < agg.La ; j++)
                {
                    agg.image[i, j] = image[agg.Ha - i, agg.La-j];
                }
            }
            return agg;
        }
        public MyImage MirroirHautGauche()
        {
            string fichier = "mirroir.bmp";
            From_Image_To_File(fichier);
            MyImage agg = new MyImage(fichier);
            agg.larg = larg;
            agg.haut = haut;
            agg.taille = 54 + 4 * agg.La * agg.Ha;
            agg.image = new Pixel[agg.Ha, agg.La];

            for (int i = agg.Ha/2; i < agg.Ha ; i++)
            {
                for (int j = 0; j < agg.La / 2; j++)
                {
                    agg.image[i, j] = image[i, j];
                }
            }
            for (int i = agg.Ha/2; i < agg.Ha ; i++)
            {
                for (int j = agg.La / 2; j < agg.La; j++)
                {
                    agg.image[i, j] = image[i, agg.La - j];
                }
            }
            for (int i = agg.Ha/2; i >= 0; i--)
            {
                for (int j = 0; j < agg.La / 2; j++)
                {
                    agg.image[i, j] = image[agg.Ha - i-1, j];
                }
            }
            for (int i = agg.Ha / 2; i >= 0; i--)
            {
                for (int j = agg.La / 2; j < agg.La; j++)
                {
                    agg.image[i, j] = image[agg.Ha - i-1, agg.La - j];
                }
            }
            return agg;
        }
        public MyImage MirroirHautDroite()
        {
            string fichier = "mirroir.bmp";
            From_Image_To_File(fichier);
            MyImage agg = new MyImage(fichier);
            agg.larg = larg;
            agg.haut = haut;
            agg.taille = 54 + 4 * agg.La * agg.Ha;
            agg.image = new Pixel[agg.Ha, agg.La];

            for (int i = agg.Ha / 2; i < agg.Ha; i++)
            {
                for (int j = agg.La/2; j < agg.La ; j++)
                {
                    agg.image[i, j] = image[i, j];
                }
            }
            for (int i = agg.Ha / 2; i < agg.Ha; i++)
            {
                for (int j = agg.La/2; j >= 0; j--)
                {
                    agg.image[i, j] = image[i, agg.La - j-1];
                }
            }
            for (int i = agg.Ha / 2; i >= 0; i--)
            {
                for (int j = agg.La/2; j < agg.La ; j++)
                {
                    agg.image[i, j] = image[agg.Ha - i - 1, j];
                }
            }
            for (int i = agg.Ha / 2; i >= 0; i--)
            {
                for (int j = agg.La / 2; j >= 00; j--)
                {
                    agg.image[i, j] = image[agg.Ha - i - 1, agg.La - j-1];
                }
            }
            return agg;
        }
        public MyImage MirroirBasDroite()
        {
            string fichier = "mirroir.bmp";
            From_Image_To_File(fichier);
            MyImage agg = new MyImage(fichier);
            agg.larg = larg;
            agg.haut = haut;
            agg.taille = 54 + 4 * agg.La * agg.Ha;
            agg.image = new Pixel[agg.Ha, agg.La];

            for (int i = 0; i < agg.Ha/2; i++)
            {
                for (int j = agg.La / 2; j < agg.La; j++)
                {
                    agg.image[i, j] = image[i, j];
                }
            }
            for (int i = agg.Ha / 2; i < agg.Ha; i++)
            {
                for (int j = agg.La / 2; j <agg.La; j++)
                {
                    agg.image[i, j] = image[agg.Ha-i,  j ];
                }
            }
            for (int i = 0; i <agg.Ha/2; i++)
            {
                for (int j = agg.La / 2; j >= 0; j--)
                {
                    agg.image[i, j] = image[i , agg.La-j-1];
                }
            }
            for (int i = agg.Ha / 2; i <agg.Ha; i++)
            {
                for (int j = agg.La / 2; j >= 0; j--)
                {
                    agg.image[i, j] = image[agg.Ha - i , agg.La - j - 1];
                }
            }
            return agg;
        }





    }
}




