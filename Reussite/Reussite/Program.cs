using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reussite
{
    //Paul "Alentak" Guillon present : "La Réussite"

    class Program
    {
        static string[] valeurs = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K" };
        static string[] familles = new string[] { "♦", "♣", "♥", "♠" };
        static ConsoleColor[] couleurs = new ConsoleColor[] { ConsoleColor.Red, ConsoleColor.White, ConsoleColor.Red, ConsoleColor.White };
        static List<Carte> jeu = new List<Carte>();
        public static List<Carte> paquet = new List<Carte>();
        static int nbCartesEnJeu = 0;

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            CreationPaquet();
            Melange();

            //Début du jeu
            for (int i = 0; i < paquet.Count; i++)
            {
                //Pose une carte
                jeu.Add(paquet[i]);
                nbCartesEnJeu++;
                AfficherJeu();

                //On commence à vérifier les trio à partir du moment où trois cartes ont posées.
                //On verifie de gauche à droite à partir du moment où on a vérifié si la dernière carte posée est bonne
                if (jeu.Count > 2)
                    Verifier();
            }

            //Fin du jeu
            Console.WriteLine(jeu.Count + " Paquets");
            Console.ReadLine();
        }

        static void CreationPaquet()
        {
            //Préparation du jeu, création du paquet
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 13; j++)
                    paquet.Add(new Carte(valeurs[j], familles[i], couleurs[i]));
        }

        static void Melange()
        {
            int l = paquet.Count - 1;
            Random rdm = new Random();

            for (int i = l; i > 1; --i)
            {
                // tirage au sort d'un index entre 0 et la valeur courante de "i"
                int randomIndex = rdm.Next(0, i);
                // intervertion des éléments situés aux index "i" et "randomIndex"
                Carte temp = paquet[i];
                paquet[i] = paquet[randomIndex];
                paquet[randomIndex] = temp;
            }
        }

        static void AfficherJeu()
        {
            Console.Clear();

            //Affichage du jeu
            foreach (Carte carte in jeu)
            {
                Console.ForegroundColor = carte.couleur;
                Console.Write(carte.valeur + carte.famille + " ");
                Console.ForegroundColor = ConsoleColor.White;
            }

            Console.WriteLine("\nCartes restantes dans le paquet : " + (paquet.Count - nbCartesEnJeu));

            Console.ReadLine();
        }

        static void Verifier()
        {
            for (int i = 2; i < jeu.Count; i++)
            {
                if (jeu[i].famille == jeu[i - 2].famille || jeu[i].valeur == jeu[i - 2].valeur)
                {
                    jeu.Remove(jeu[i - 2]);

                    AfficherJeu();
                    //On a trouvé un triple, maintenant faut recommencer la vérification depuis la derniere carte
                    Verifier();
                }
            }
        }
    }

    class Carte
    {
        public string valeur;
        public string famille;
        public ConsoleColor couleur;

        public Carte(string valeur, string famille, ConsoleColor couleur)
        {
            this.valeur = valeur;
            this.famille = famille;
            this.couleur = couleur;
        }
    }
}