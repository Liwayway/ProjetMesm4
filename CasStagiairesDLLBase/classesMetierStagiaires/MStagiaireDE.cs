using System;


namespace classesMetierStagiaires
{
    /// <summary>
    /// stagiaire spécialisé statut Demandeur d'emploi
    /// </summary>
    public class MStagiaireDE : MStagiaire
    {

        /// <summary>
        /// constructeur d'initialisation
        /// (appelle le constructeur d'initialisation de la classe de base)
        /// </summary>
        /// <param name="unNumosia">numéro OSIA</param>
        /// <param name="unNom">Nom</param>
        /// <param name="unPrenom">Prénom</param>
        /// <param name="uneRue">Adresse</param>
        /// <param name="uneVille">Ville</param>
        /// <param name="UnCodePostal">Code Postal</param>
        /// <param name="unRemuAfpa">Rémunaration par l'Afpa</param>

        // constante qui indique le fait qu'il s'agit d'un objet StagiaireDE
        protected const Boolean DE = true;

        // relation avec la table section
        protected String codeSection;
        
        public MStagiaireDE(Int32 unNumosia, String unNom, String unPrenom, String uneRue, String uneVille, String UnCodePostal,
            Boolean unRemuAfpa, String codeSection)
            : base( unNumosia,  unNom,  unPrenom,  uneRue,  uneVille,  UnCodePostal, codeSection)
        {
            this.RemuAfpaStagiaire = unRemuAfpa;
            this.codeSection = codeSection;
        }

        /// <summary>
        /// Rémuneration par l'Afpa
        /// </summary>
        private Boolean remuAfpaStagiaire;

        /// <summary>
        /// obtient ou définit Rémunération par l'Afpa
        /// </summary>
        public Boolean RemuAfpaStagiaire
        {
            get
            {
                return this.remuAfpaStagiaire;
            }
            set
            {
                this.remuAfpaStagiaire = value;
            }
        }

        public String CodeSection {
            get { return this.codeSection; }
            set {
                this.codeSection = value;
            }
        }

        // ecrit la requete en bdd à faire executer par l'object de connexion
        public override Boolean Save(ConnexionBDD connexion)
        {
            //INSERT INTO
            string query = "INSERT INTO stagiaire (numOsia, prenom, nom, rue, ville, cp, de, remuafpa, codesection) VALUES('"
        + this.numOsiaStagiaire +
        "','"
        + this.PrenomStagiaire +
        "','"
        + this.NomStagiaire +
        "','"
        + this.RueStagiaire +
        "','"
        + this.VilleStagiaire +
        "','"
        + this.CodePostalStagiaire +
        "',"
        + true +
        ","
        //Pour la rému afpa, comment faire ? si la checkbox est check = true
        + this.RemuAfpaStagiaire +
        ",'"
        + this.CodeSection +
        "')";


            int result = connexion.executeQuery(query);

            if (result == 1) { return true; }

            // mieux : lever une exception
            return false;
        }

    }
}
