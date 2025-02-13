using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace essaimyimage
{
    public class Program
    {
        

        static void Main(string[] args)
        {
            

            Console.WriteLine("              Bienvenue dans le menu de selection des opérations sur Image ");
            string reponse = "";
            MyImage coco = new MyImage("coco.bmp");
            MyImage test = new MyImage("Test.bmp");
            MyImage lac = new MyImage("lac.bmp");
            MyImage lena = new MyImage("lena.bmp");
            MyImage hasbulla = new MyImage("hasbulla.bmp");
            MyImage montgolfiere = new MyImage("montgolfiere.bmp");
            MyImage modele_fractale = new MyImage("rectangle.bmp");
            /*MyImage fractale = new MyImage("fractale.bmp");*/
            MyImage choixdef = null;

            while (reponse != "STOP")
            {

                
                Console.WriteLine("\nVous avez pour l'instant acces aux méthode suivante : \n -  (1)  Aggranissement à partir d'une image initiale et d'un coefficient    \n -  (2)  Découpe d'une quart supérieur gauche d'une image    \n -  (3)  Rotation d'une image à partir d'un angle quelquonque ");
                
                Console.WriteLine("\n -  (4)  Accentuation des contrastes  \n -  (5)  Detection de contour saturation noire \n -  (6)  Detection de Contour saturation blanche \n -  (7)  Flou \n -  (8)  Exposition élevé");
                Console.WriteLine("\n -  (9)  Rotation pour un angle de 90 degre et ses multiples \n -  (10)  Filtre de Sobel \n -  (11)  Fractale\n -  (12)  Sténographie\n -  (13)  Miroir coin inferieur gauche\n -  (14)  Miroir coin supérieur gauche \n -  (15)  Nuance de gris\n -  (16)  Miroir coin supérieur droit\n -  (17)  Miroir coin inférieur droit");
                for (int i = 0; i < 5; i++)
                {
                    Console.WriteLine(" ");
                }
                Console.WriteLine("Quel methode voulez vous utiliser : (Ecrivez le numero de la méthode)");
                int r = Convert.ToInt32(Console.ReadLine());
                while (r !=1 && r!=2 && r!=3 && r!=4 && r!=5 && r!= 6 && r!=7 && r!=8 && r!=9 && r!=10 && r!= 11 && r!=12 && r!=13 && r!= 14 && r!= 15 && r!=16 && r!=17)
                {
                    Console.WriteLine("Erreur Veuillez rentrer un entier entre 1 et 11");
                    r = Convert.ToInt32(Console.ReadLine());
                }
                Console.Clear();
                Console.WriteLine("              Bienvenue dans le menu de selection des opérations sur Image \n");
                if (r == 11)
                {
                    string na = "fractale.bmp";
                    MyImage fractal = modele_fractale.Fractale();
                    fractal.From_Image_To_File(na);
                    Process.Start(na);
                }
                else
                {



                    Console.WriteLine(" Quelle image de départ voulez vous choisir : ");
                    Console.WriteLine("\n a) coco \n b) test  \n c) Lac    \n d) Lena\n e) Hasbulla \n f) Montgolfière\n");
                    Console.WriteLine("Entrer la lettre correspondante.");
                    char choix = Convert.ToChar(Console.ReadLine());
                    choix = char.ToUpper(choix);
                    string fichier = "";
                    while (choix != 'A' && choix != 'B' && choix != 'C' && choix != 'D' && choix != 'E' && choix != 'F')
                    {
                        Console.WriteLine(" Erreur Veuillez rentrer une lettre parmi le choix suivant : A, B, C ,D, E, F");
                        choix = Convert.ToChar(Console.ReadLine());
                        choix = char.ToUpper(choix);
                    }
                    switch (choix)
                    {
                        case 'A':
                            Console.WriteLine(" Vous avez choisi COCO");
                            fichier = "coco.bmp";
                            Process.Start("coco.bmp");
                            choixdef = coco;

                            break;
                        case 'B':
                            Console.WriteLine(" Vous avez choisi TEST");
                            fichier = "Test.bmp";
                            Process.Start("Test.bmp");
                            choixdef = test;
                            break;
                        case 'C':
                            Console.WriteLine(" Vous avez choisi LAC");
                            fichier = "lac.bmp";
                            Process.Start("lac.bmp");
                            choixdef = lac;
                            break;
                        case 'D':
                            Console.WriteLine(" Vous avez choisi LENA");
                            fichier = "lena.bmp";
                            Process.Start("lena.bmp");
                            choixdef = lena;
                            break;
                        case 'E':
                            Console.WriteLine("Vous avez choisi Hasbulla");
                            fichier = "hasbulla.bmp";
                            Process.Start("hasbulla.bmp");
                            choixdef = hasbulla;
                            break;
                        case 'F':
                            Console.WriteLine("Vous avez choisi Montgolfiere");
                            fichier = "montgolfiere.bmp";
                            Process.Start("montgolfiere.bmp");
                            choixdef = montgolfiere;
                            break;
                       
                        default:

                            break;

                    }
                    switch (r)
                    {
                        case 1:
                            string aggr = "agg.bmp";
                            Console.WriteLine("Quel coefficient voulez vous appliquer pour l'aggrandissement : ");
                            int a = Convert.ToInt32(Console.ReadLine());
                            while (a < 0)
                            {
                                Console.WriteLine("Erreur veuillez inserer un coefficient entier positif supérieur à 1");
                                a = Convert.ToInt32(Console.ReadLine());
                            }
                            MyImage agg = choixdef.Aggrandissement(a);
                            agg.From_Image_To_File(aggr);
                            Process.Start(aggr);
                            break;
                        case 2:
                            string name = "quart.bmp";
                            MyImage quart = choixdef.Quart();
                            quart.From_Image_To_File(name);
                            Process.Start(name);
                            break;
                        case 3:
                            string rota = "rota.bmp";
                            Console.WriteLine("Ecrivez l'angle de la rotation : ");
                            int angle = Convert.ToInt32(Console.ReadLine());
                            MyImage rotat = choixdef.RotationQuelqonque(angle);
                            rotat.From_Image_To_File(rota);
                            Process.Start(rota);
                            break;
                        case 4:
                            int[,] kernel = new int[,]
                {
                { 0, -1, 0 },
                { -1, 5, -1 },
                { 0, -1, 0 }
                };
                            string convo_ = "neteté.bmp";
                            MyImage convo = choixdef.Convolution(kernel);
                            convo.From_Image_To_File(convo_);
                            Process.Start(convo_);
                            break;
                        case 5:
                            kernel = new int[,]
               {
                { 0, -1, 0 },
                { -1, 4, -1 },
                { 0, -1, 0 }
               };
                            string contourfaible_ = "cococonvo.bmp";
                            MyImage contourfaible = choixdef.Convolution(kernel);
                            contourfaible.From_Image_To_File(contourfaible_);
                            Process.Start(contourfaible_);
                            break;
                        case 6:
                            kernel = new int[,]
               {
                { -1, -1, -1 },
                { -1, 8, -1 },
                { -1, -1, -1 }
               };
                            string contourfort_ = "cococonvo.bmp";
                            MyImage contourfort = choixdef.Convolution(kernel);
                            contourfort.From_Image_To_File(contourfort_);
                            Process.Start(contourfort_);
                            break;
                        case 7:

                            double[,] kernell = new double[,]
                {

                { 1/9.0, 1/9.0, 1/9.0 },
                { 1/9.0,1 / 9.0, 1/9.0 },
                {1/9.0, 1 / 9.0, 1/ 9.0}
                 };
                            string nom_flou = "cocoflou.bmp";
                            MyImage flou = choixdef.Flou(kernell);
                            flou.From_Image_To_File(nom_flou);
                            Process.Start(nom_flou);

                            break;
                        case 8:
                            kernel = new int[,]
                {
                { 0, -1, 0 },
                { -1, 8, -1 },
                { 0, -1, 0 }
                };
                            string luminosite_ = "cococonvo.bmp";
                            MyImage luminosite = choixdef.Convolution(kernel);
                            luminosite.From_Image_To_File(luminosite_);
                            Process.Start(luminosite_);
                            break;
                        case 9:
                            string rotation = "rota.bmp";
                            Console.WriteLine("Ecrivez l'angle de la rotation entre 90 , 180 et 270 : ");
                            int degre = Convert.ToInt32(Console.ReadLine());
                            MyImage rotatio = choixdef.Rotation90(degre);
                            rotatio.From_Image_To_File(rotation);
                            Process.Start(rotation);
                            break;
                        case 10:
                            kernel = new int[,]
                {
                { -1, 0, 1 },
                { -2, 0, 2 },
                { -1, 0, 0 }
                };
                            string repoussage_ = "cococonvo.bmp";
                            MyImage repoussage = choixdef.Convolution(kernel);
                            repoussage.From_Image_To_File(repoussage_);
                            Process.Start(repoussage_);
                            break;
                        case 12:
                            string steno = "stenographie.bmp";
                            Console.WriteLine("\n Veuillez choisir l'image à cacher parmi :\n - (A) coco \n - (B) lena \n - (C) test  "); 
                            char c = Convert.ToChar(Console.ReadLine());
                            c = char.ToUpper(c);
                            MyImage stenographie = null;
                            switch (c)
                                {
                                case 'A':
                                     stenographie = choixdef.Steno(coco);
                                    break;
                                case 'B':
                                     stenographie = choixdef.Steno(lena);
                                    break;
                                    case 'C':
                                    stenographie = choixdef.Steno(test);
                                    break;
                            }
                            
                            stenographie.From_Image_To_File(steno);
                            Process.Start(steno);
                            break;
                        case 13:
                            string mir = "miroir.bmp";
                            MyImage miroir = choixdef.MirroirBasGauche();
                            miroir.From_Image_To_File(mir);
                            Process.Start(mir);
                            break;
                        case 14:
                            string miro = "miroir.bmp";
                            MyImage miroirHaut = choixdef.MirroirHautGauche();
                            miroirHaut.From_Image_To_File(miro);
                            Process.Start(miro);
                            break;
                        case 15:
                            string noir = "noiretblanc.bmp";
                            MyImage noiretblanc = choixdef.NoiretBlanc();
                            noiretblanc.From_Image_To_File(noir);
                            Process.Start(noir);
                            break;
                        case 16:
                            string miroi = "miroir.bmp";
                            MyImage miroirHautD = choixdef.MirroirHautDroite();
                            miroirHautD.From_Image_To_File(miroi);
                            Process.Start(miroi);
                            break;
                        case 17:
                            string miroirr = "miroir.bmp";
                            MyImage miroirbasD = choixdef.MirroirBasDroite();
                            miroirbasD.From_Image_To_File(miroirr);
                            Process.Start(miroirr);
                            break;

                        default:
                            break;
                    }
                }

                Console.Clear();
                Console.WriteLine("              Bienvenue dans le menu de selection des opérations sur Image ");
                Console.WriteLine(" \n Souhaitez vous poursuivre ? \n \n ");
                Console.WriteLine(" Ecrivez STOP si vous souhaitez arreter sinon ecrivez n'importe quel autre caractère");
                string repo = Console.ReadLine();
                reponse = null;
                for (int i =0; i < repo.Length;i++)
                {
                    reponse = reponse + char.ToUpper(repo[i]);
                }
                Console.Clear();
            }

           

            /*Pixel[,] mat_p = Fractale(coco.Ha, coco.La);
            MyImage fract = MatP_to_MyImage(mat_p);
            string nom_fractale = "fractale.bmp";
            fract.From_Image_To_File(nom_fractale);
            Process.Start(nom_fractale);*/

            /*Process.Start("Test.bmp");
            MyImage Test = new MyImage("coco.bmp");
            string nom_fractale = "fractale.bmp";
            MyImage fract = Test.Fractale();
            fract.From_Image_To_File(nom_fractale);
            Process.Start(nom_fractale);*/


        }


    }
}
