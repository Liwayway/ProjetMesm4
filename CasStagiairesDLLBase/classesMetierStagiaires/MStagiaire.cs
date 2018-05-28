using System;

namespace classesMetierStagiaires
{
    /// <summary>
    /// classe abstraite des stagiaires
    /// </summary>
    abstract public class MStagiaire
    {

        /// <summary>
        /// constructeur d'initialisation
        /// </summary>
        /// <param name="unNumosia">Numéro OSIA</param>
        /// <param name="unNom">nom</param>
        /// <param name="unPrenom">prénom</param>
        /// <param name="uneRue">adresse</param>
        /// <param name="uneVille">ville</param>
        /// <param name="UnCodePostal">code postal</param>
        public MStagiaire(Int32 unNumosia, String unNom, String unPrenom, String uneRue, String uneVille, String UnCodePostal, String codeSection)
        {
            // identifiant en lecture seule ==> affectation directe attribut
            this.numOsiaStagiaire = unNumosia;
            // utilise les accesseurs set
            this.NomStagiaire = unNom;
            this.PrenomStagiaire = unPrenom;
            this.RueStagiaire = uneRue;
            this.VilleStagiaire = uneVille;
            this.CodePostalStagiaire = UnCodePostal;
            this.CodeSection = codeSection;
            // attributs sans accesseurs set
            this.nbreNotesStagiaire = 0;
            this.pointsNotesStagiaire = 0;

        }
        /// <summary>
        /// numéro du stagiaire,
        /// l'appelant devra prendre garde à passer un entier valide
        /// </summary>
        protected Int32 numOsiaStagiaire;

        /// <summary>
        /// obtient le numéro du stagiaire
        /// </summary>
        public Int32 NumOsiaStagiaire
        {
            get { return this.numOsiaStagiaire; }
        }

        /// <summary>
        /// le nom du stagiaire
        /// </summary>
        protected String nomStagiaire;

        /// <summary>
        /// obtient ou définit le nom du stagiaire (forcé en majuscules)
        /// </summary>
        public String NomStagiaire
        {
            get { return this.nomStagiaire; } // en lecture, retourne la var privée
            // cette portion de code sert à l’affectation d’une nouvelle valeur ;
            // c’est ici que l’on effectue des contrôles de saisie ou de format
            // ici : mettre le nom en majuscules :
            set { this.nomStagiaire = value.Trim().ToUpper(); } // mettre le nom en majuscules 
        }

        /// <summary>
        /// le prénom de stagiaire
        /// </summary>
        protected String prenomStagiaire;

        /// <summary>
        /// obtient ou définit le prénom de stagiaire (forcé en minuscules)
        /// </summary>
        public String PrenomStagiaire
        {
            get { return this.prenomStagiaire; } // en lecture, retourne la var privée
            set { this.prenomStagiaire = value.Trim().ToLower(); } // mettre le prénom en minuscules 
        }

        /// <summary>
        /// immeuble, rue et numéro, le format est libre
        /// </summary>
        protected String rueStagiaire;

        /// <summary>
        /// obtient ou définit immeuble, rue et numéro, le format est libre
        /// </summary>
        public String RueStagiaire
        {
            get { return this.rueStagiaire; }
            set { this.rueStagiaire = value; }
        } 
        
        /// <summary>
        /// le nom de la ville
        /// </summary>
        protected String villeStagiaire;

        /// <summary>
        /// obtient ou définit le nom de la ville (forcé en majuscules)
        /// </summary>
        public String VilleStagiaire
        {
            get { return this.villeStagiaire; } // en lecture, retourne la var privée
            set { this.villeStagiaire = value.Trim().ToUpper(); } // mettre la ville en majuscules 
        }

        /// <summary>
        /// le code postal, l'appelant devra prendre garde à passer 
        /// un code postal valide à 5 chiffres
        /// </summary>
        protected String codePostalStagiaire;     

        /// <summary>
        /// obtient ou définit le code postal (contrôle : 5 car tous chiffres)
        /// </summary>
        /// <exception cref="Exception">le code postal n'est pas constitué de 5 chiffres</exception>
        public  String CodePostalStagiaire          
        {
            get { return this.codePostalStagiaire; } // en lecture, retourne la var privée
            set 
            {
                // l'appelant doit fournir un code postal valide à 5 chiffres
                Int32 i ;               // variable  de boucle
                Boolean erreur = false; // indicateur erreur
                if (value.Length == 5 ) // 5 car. attendus : OK ==> contrôler un à un
                {
                    for (i = 0; i< value.Length; i++)  // controle caractères par boucle
                    {
                        if (! (Char.IsDigit(value[i]))) // ce n'est pas un chiffre
                            {erreur = true;}
                        
                    } // fin de boucle controle chiffres
                    if (erreur) //on a rencontre un non-chiffre
                    {
                        // levée d'exception
                        throw new Exception(value.ToString() + "\n" + "n'est pas un code postal valide : uniquement des chiffres");
                    }
                    else
                    {
                        this.codePostalStagiaire = value;  // tout est bon, on affecte l'attribut
                    }
                }
                else // il n'y a pas 5 caractères
                {
                    // levée d'exception
                    throw new Exception(value.ToString() + "\n" + "n'est pas un code postal valide : 5 chiffres, pas plus, pas moins");
                }

            }
          
        }

        public String CodeSection;

        public abstract Boolean Save(ConnexionBDD o);

        /// <summary>
        /// nombre de notes obtenues
        /// </summary>
        protected Int32 nbreNotesStagiaire;

        /// <summary>
        /// cumul des points obtenus
        /// </summary>
        protected Double pointsNotesStagiaire; 
        
        /// <summary>
        /// permet d'alimenter NbreNotes et PointsNotes
        /// </summary>
        /// <param name="laNote">la nouvelle note à prendre en compte</param>
        public void RecevoirNote(float laNote)
        {
            this.nbreNotesStagiaire++;
            this.pointsNotesStagiaire += laNote;
        }

        /// <summary>
        /// obtient la moyenne des notes reçues
        /// </summary>
        /// <returns>nouvelle moyenne des notes (0 si pas de note reçue)</returns>
        public Double DonnerMoyenne()            
        {
            // division par zéro impossible
            if (this.nbreNotesStagiaire > 0)
            {
                return this.pointsNotesStagiaire / this.nbreNotesStagiaire;
            }
            else // pas de note ==> retourner zéro
            {
                return 0;
            }
        }

        /// <summary>
        /// obtient un libellé en clair (numosia + nom et prénom)
        /// </summary>
        /// <returns></returns>
        public override String ToString()
        {
            return "Stagiaire " + this.NumOsiaStagiaire + " : " + this.NomStagiaire.Trim() + " " + this.PrenomStagiaire.Trim();
        }
        
    }

}
