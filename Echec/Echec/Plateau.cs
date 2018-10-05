using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Echec
{
    public class Plateau
    {
        private Piece[,] m_echiquier; //Tableau qui représente l'échiquier avec ses pièces
        private Stack<Piece[,]> m_historique; //Historique des échiquiers

        public Plateau() //Constructeur
        {
            m_echiquier = new Piece[8, 8]; //Créer l'échiquier
            preparerEchiquier();
        }

        public override string ToString() { //Sérialiser l'échiquier
            string serial = ""; //Chaine à retourner

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (m_echiquier[i, j] == null) //Si la case est vide
                    {
                        serial += "x,";
                    }
                    else //Sinon sérialiser la pièce
                    {
                        serial += m_echiquier[i, j].ToString() + ",";
                    }
                }
            }

            return serial;
        }

        private void preparerEchiquier() //Préparer l'échiquier pour une partie
        {
            //Placer les pièces noires
            m_echiquier[0, 0] = new Tour(false, "Tour", true, false); //Noir, nom, possibilité de collisions, n'a pas bougé
            m_echiquier[0, 1] = new Cavalier(false, "Cavalier", false); //Noir, nom, pas de possibilité de collisions
            m_echiquier[0, 2] = new Fou(false, "Fou", true); //Noir, nom, possibilité de collisions
            m_echiquier[0, 3] = new Reine(false, "Reine", true); //Noir, nom, possibilité de collisions
            m_echiquier[0, 4] = new Roi(false, "Roi", true, false); //Noir, nom, possibilité de collisions, n'a pas bougé
            m_echiquier[0, 5] = new Fou(false, "Fou", true); //Noir, nom, possibilité de collisions
            m_echiquier[0, 6] = new Cavalier(false, "Cavalier", false); //Noir, nom, pas de possibilité de collisions
            m_echiquier[0, 7] = new Tour(false, "Tour", true, false); //Noir, nom, possibilité de collisions, n'a pas bougé
            //Placer les pions noirs
            for (int i = 0; i < 8; i++)
            {
                m_echiquier[1, i] = new Pion(false, "Pion", true, false); //Noir, nom, possibilité de collisions, n'a pas bougé
            }
            //Placer les pions blancs
            for (int i = 0; i < 8; i++)
            {
                m_echiquier[6, i] = new Pion(true, "Pion", true, false); //Blanc, nom, possibilité de collisions, n'a pas bougé
            }
            //Placer les pièces noires
            m_echiquier[7, 0] = new Tour(true, "Tour", true, false); //Blanc, nom, possibilité de collisions, n'a pas bougé
            m_echiquier[7, 1] = new Cavalier(true, "Cavalier", false); //Blanc, nom, pas de possibilité de collisions
            m_echiquier[7, 2] = new Fou(true, "Fou", true); //Blanc, nom, possibilité de collisions
            m_echiquier[7, 3] = new Reine(true, "Reine", true); //Blanc, nom, possibilité de collisions
            m_echiquier[7, 4] = new Roi(true, "Roi", true, false); //Blanc, nom, possibilité de collisions, n'a pas bougé
            m_echiquier[7, 5] = new Fou(true, "Fou", true); //Blanc, nom, possibilité de collisions
            m_echiquier[7, 6] = new Cavalier(true, "Cavalier", false); //Blanc, nom, pas de possibilité de collisions
            m_echiquier[7, 7] = new Tour(true, "Tour", true, false); //Blanc, nom, possibilité de collisions, n'a pas bougé
        }

        private void ajouterHistorique() //Ajouter l'échiquier à l'historique avant de le modifier
        {
            m_historique.Push(m_echiquier);
        }

        private void retourHistoriquePrec() //Revenir à l'échiquier précédent
        {
            m_echiquier = m_historique.Peek();
            m_historique.Pop();
        }

        public int validerCoup(int[] p_posPiece, int[] p_posCase, bool p_tour) //Valider si le coup est légal et sinon renvoyer un code d'erreur
        {
            int x = p_posPiece[0]; //x de la pièce
            int y = p_posPiece[1]; //y de la pièce

            //Vérifier si la case de départ est vide
            if (m_echiquier[x, y] == null)
            {
                return 1; //Il n'y a pas de pièce à jouer sur cette case
            }
            bool couleurPiece = m_echiquier[x, y].Couleur; //Couleur de la pièce
            //Vérifier si la pièce est celle du joueur auquel c'est le tour
            if (!verifierTour(couleurPiece, p_tour))
            {
                return 2; //Ce n'est pas le tour du joueur
            }
            //Vérifier si le joueur essaie de déplacer une pièce sur sa propre case
            if ((p_posPiece[0] == p_posCase[0]) && (p_posPiece[1] == p_posCase[1])) //Si les cases sont les mêmes
            {
                return 3; //Le joueur essaie de déplacer la pièce sur sa propre case
            }
            //Vérifier si la case a un pion allié
            int etatCase = caseEtat(p_posCase, couleurPiece); //État de la case
            if (etatCase == 1)
            {
                return 4; //Il y a une pièce alliée sur la case
            }
            //Vérifier si le déplacement est possible pour la pièce
            if (!m_echiquier[x, y].validerDeplacement(p_posPiece, p_posCase, (etatCase == 0)))
            {
                return 5; //La pièce ne peut pas se déplacer de cette façon
            }
            //Vérifier si le déplacement cause des collisions en route
            if (m_echiquier[x, y].Collision) //Si la pièce n'a pas la permission de passer par dessus d'autres pièces
            {
                if (collision(p_posPiece, p_posCase))
                {
                    return 6; //Une pièce est dans le chemin et empêche le déplacement
                }
            }
            //Vérifier si le joueur se met lui-même en état d'échec avec ce mouvement
            if (miseEnEchec(p_posPiece, p_posCase, couleurPiece)) 
            {
                return 7; //Le joueur se met lui-même en échec avec ce coup
            }

            return 0; //Le coup est accepté
        }

        private bool verifierTour(bool p_couleurPiece, bool p_tour) //Vérifier si la pièce jouée est celle du joueur auquel c'est le tour
        {
            if (p_couleurPiece == p_tour)
            {
                return true;
            }
            return false;
        }

        private int caseEtat(int[] p_posCase, bool p_couleur) //Vérifier si la case est vide ou occupée par une pièce ennemie ou alliée
        {
            int x = p_posCase[0]; //x de la case
            int y = p_posCase[1]; //y de la case
            int code; //Code de renvoi

            if (m_echiquier[x, y] == null)
            {
                code = 0; //Case vide
            }
            else if (m_echiquier[x, y].Couleur == p_couleur)
            {
                code = 1; //Case occupée par un allié
            }
            else
            {
                code = 2; //Case occupée par un ennemi
            }

            return code;
        }

        private bool collision(int[] p_posPiece, int[] p_posCase) //Vérifier s'il y a une collision lors du déplacement
        {
            //À FAIRE
            return false;
        }

        public void deplacerPiece(int[] p_posPiece, int[] p_posCase) //Déplacer la pièce sur l'échiquier
        {
            int xPiece = p_posPiece[0]; //x de la pièce
            int yPiece = p_posPiece[1]; //y de la pièce
            int xCase = p_posCase[0]; //x de la case
            int yCase = p_posCase[1]; //y de la case

            ajouterHistorique(); //Ajouter l'échiquier à l'historique avant de le changer

            m_echiquier[xCase, yCase] = m_echiquier[xPiece, yPiece];
            m_echiquier[xPiece, yPiece] = null;
        }

        public bool echec(bool p_joueur) //Vérifier si le joueur passé en paramètre (sa couleur) est en état d'échec
        {
            int[] posRoi = new int[2]; //La case du roi du joueur

            for (int i = 0; i < 8; i++) //Trouver le roi
            {
                for (int j = 0; j < 8; j++)
                {
                    if ((m_echiquier[i, j] is Roi) && (m_echiquier[i, j].Couleur == p_joueur)) //Si c'est un roi de la même couleur
                    {
                        posRoi[0] = i;
                        posRoi[1] = j;
                    }
                }
            }

            for (int i = 0; i < 8; i++) //Tester si les pièces peuvent se rendre jusqu'au roi
            {
                for (int j = 0; j < 8; j++)
                {
                    int[] posDepart = new int[2]; //La case à tester
                    if (validerCoup(posDepart, posRoi, !p_joueur) == 0)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool miseEnEchec(int[] p_posPiece, int[] p_posCase, bool p_joueur) //Vérifier si le joueur se met en échec avec le déplacement voulu
        {
            bool miseEnEchec; //Si le joueur se met en échec
            int xCase = p_posCase[0]; //x de la case
            int yCase = p_posCase[1]; //y de la case

            deplacerPiece(p_posPiece, p_posCase); //Déplacer la pièce temporairement pour le test

            miseEnEchec = echec(p_joueur); //Tester la mise en échec

            retourHistoriquePrec(); //Récupérer l'historique précédent pour remettre les pièces à leur place

            return miseEnEchec;
        }
    }
}
