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

        public override string ToString() //Sérialiser l'échiquier
        { 
            string serial = ""; //Chaine à retourner

            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    if (m_echiquier[x, y] == null) //Si la case est vide
                    {
                        serial += "x,";
                    }
                    else //Sinon sérialiser la pièce
                    {
                        serial += m_echiquier[x, y].ToString() + ",";
                    }
                }
            }

            return serial;
        }

        private void preparerEchiquier() //Préparer l'échiquier pour une partie
        {
            //Placer les pièces noires
            m_echiquier[0, 0] = new Tour(false, "Tour", true, false); //Noir, nom, possibilité de collisions, n'a pas bougé
            m_echiquier[1, 0] = new Cavalier(false, "Cavalier", false); //Noir, nom, pas de possibilité de collisions
            m_echiquier[2, 0] = new Fou(false, "Fou", true); //Noir, nom, possibilité de collisions
            m_echiquier[3, 0] = new Reine(false, "Reine", true); //Noir, nom, possibilité de collisions
            m_echiquier[4, 0] = new Roi(false, "Roi", true, false); //Noir, nom, possibilité de collisions, n'a pas bougé
            m_echiquier[5, 0] = new Fou(false, "Fou", true); //Noir, nom, possibilité de collisions
            m_echiquier[6, 0] = new Cavalier(false, "Cavalier", false); //Noir, nom, pas de possibilité de collisions
            m_echiquier[7, 0] = new Tour(false, "Tour", true, false); //Noir, nom, possibilité de collisions, n'a pas bougé
            //Placer les pions noirs
            for (int x = 0; x < 8; x++)
            {
                m_echiquier[x, 1] = new Pion(false, "Pion", true, false); //Noir, nom, possibilité de collisions, n'a pas bougé
            }
            //Placer les pions blancs
            for (int x = 0; x < 8; x++)
            {
                m_echiquier[x, 6] = new Pion(true, "Pion", true, false); //Blanc, nom, possibilité de collisions, n'a pas bougé
            }
            //Placer les pièces noires
            m_echiquier[0, 7] = new Tour(true, "Tour", true, false); //Blanc, nom, possibilité de collisions, n'a pas bougé
            m_echiquier[1, 7] = new Cavalier(true, "Cavalier", false); //Blanc, nom, pas de possibilité de collisions
            m_echiquier[2, 7] = new Fou(true, "Fou", true); //Blanc, nom, possibilité de collisions
            m_echiquier[3, 7] = new Reine(true, "Reine", true); //Blanc, nom, possibilité de collisions
            m_echiquier[4, 7] = new Roi(true, "Roi", true, false); //Blanc, nom, possibilité de collisions, n'a pas bougé
            m_echiquier[5, 7] = new Fou(true, "Fou", true); //Blanc, nom, possibilité de collisions
            m_echiquier[6, 7] = new Cavalier(true, "Cavalier", false); //Blanc, nom, pas de possibilité de collisions
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

            //Cas erreur 1: Il n'y a pas de pièce à jouer sur cette case 
            if (m_echiquier[x, y] == null)
            {
                return 1;
            }

            bool couleurPiece = m_echiquier[x, y].Couleur; //Couleur de la pièce
           
            //Cas erreur 2: La pièce à jouer n'est pas une pièce du joueur auquel c'est le tour
            if (!verifierTour(couleurPiece, p_tour))
            {
                return 2;
            }

            //Cas erreur 3: La case de départ et la case d'arrivée sont la même case
            if ((p_posPiece[0] == p_posCase[0]) && (p_posPiece[1] == p_posCase[1]))
            {
                return 3;
            }

            int etatCase = caseEtat(p_posCase, couleurPiece); //État de la case

            //Cas erreur 4: Il y a une pièce alliée sur la case visée
            if (etatCase == 1)
            {
                return 4;
            }

            //VÉRIFIER ROQUE (si roque sauter le cas 5, permettre)

            //Cas erreur 5: La pièce ne peut pas se déplacer de cette façon
            if (!m_echiquier[x, y].validerDeplacement(p_posPiece, p_posCase, (etatCase == 0)))
            {
                return 5;
            }

            //Cas erreur 6: Une pièce est dans le chemin et empêche le déplacement
            if (m_echiquier[x, y].Collision) //Si la pièce n'a pas la permission de passer par dessus d'autres pièces
            {
                if (collision(p_posPiece, p_posCase))
                {
                    return 6;
                }
            }

            //Cas erreur 7: Le joueur se met lui-même en échec avec ce coup
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
            int x = p_posPiece[0]; //x de la pièce
            int y = p_posPiece[1]; //y de la pièce

            //Stocker la liste des cases par laquelle la pièce devra se déplacer
            List<int[]> route = m_echiquier[x, y].routeDeplacement(p_posPiece, p_posCase);

            int xTest; //x à tester
            int yTest; //y à tester

            for (int i = 0; i < route.Count; i++) //Vérifier si chaque case est vide
            {
                xTest = (route[i])[0];
                yTest = (route[i])[1];
                if (m_echiquier[xTest, yTest] != null) //La case n'est pas vide
                {
                    return true;
                }
            }

            return false;
        }

        public void deplacerPiece(int[] p_posPiece, int[] p_posCase) //Déplacer la pièce sur l'échiquier
        {
            ajouterHistorique(); //Ajouter l'échiquier à l'historique avant de le changer

            //VÉRIFIER ROQUE (si roque sauter le reste, effectuer le roque)

            int xPiece = p_posPiece[0]; //x de la pièce
            int yPiece = p_posPiece[1]; //y de la pièce
            int xCase = p_posCase[0]; //x de la case
            int yCase = p_posCase[1]; //y de la case

            m_echiquier[xCase, yCase] = m_echiquier[xPiece, yPiece];
            m_echiquier[xPiece, yPiece] = null;
        }

        public bool echec(bool p_joueur) //Vérifier si le joueur passé en paramètre (sa couleur) est en état d'échec
        {
            int[] posRoi = new int[2]; //La case du roi du joueur

            for (int x = 0; x < 8; x++) //Trouver le roi
            {
                for (int y = 0; y < 8; y++)
                {
                    if ((m_echiquier[x, y] is Roi) && (m_echiquier[x, y].Couleur == p_joueur)) //Si c'est un roi de la même couleur
                    {
                        posRoi[0] = x;
                        posRoi[1] = y;
                    }
                }
            }

            int[] posDepart = new int[2]; //La case à tester

            for (int x = 0; x < 8; x++) //Tester si les pièces peuvent se rendre jusqu'au roi
            {
                for (int y = 0; y < 8; y++)
                {
                    posDepart[0] = x;
                    posDepart[1] = y;
                    if (validerCoup(posDepart, posRoi, !p_joueur) == 0)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool echecEtMat(bool p_joueur) //Vérifier si le joueur passé en paramètre (sa couleur) est en état d'échec et mat
        {
            return false;
        }

        public bool nulle() //Vérifier si le jeu se termine par une nulle
        {
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

        public bool verifierPromo(int[] p_posPiece, int[] p_posCase, bool p_joueur) //Vérifier si un pion du joueur atteint le fond de l'échiquier
        {
            bool promo = false; //Présence de promo de pion
            int xPiece = p_posPiece[0]; //x de la pièce
            int yPiece = p_posPiece[1]; //y de la pièce
            int yCase = p_posCase[1]; //y de la case

            if (m_echiquier[xPiece, yPiece] is Pion) //Vérifier si la pièce est un pion
            {
                if (p_joueur) //Si joueur blanc, le fond est la rangée 0
                {
                    if (yCase == 0)
                    {
                        promo = true;
                    }
                }
                else //Si joueur noir, le fond est la rangée 7
                {
                    if (yCase == 7)
                    {
                        promo = true;
                    }
                }
            }

            return promo;
        }

        public void promouvoirPion(int[] p_posCase, string p_pieceChoisie) //Changer le pion pour une pièce choisie
        {
            ajouterHistorique(); //Ajouter l'échiquier à l'historique avant de le changer
        }

        private bool verifierRoque(int[] p_posPiece, int[] p_posCase) //Vérifier si le joueur a voulu faire un roque
        {
            return false;
        }

        private void effectuerRoque(int[] p_posPiece, int[] p_posCase) //Effectuer un roque
        {

        }
    }
}
