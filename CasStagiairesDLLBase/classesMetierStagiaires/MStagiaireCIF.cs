using System;
using System.Collections.Generic;
using System.Text;

namespace classesMetierStagiaires
{
    /// <summary>
    /// stagiaire spécialisé statut CIF
    /// </summary>
    public class MStagiaireCIF : MStagiaire
    {

        /// <summary>
        /// constructeur d'initialisation
        /// (appelle le constructeur d'initialisation de la classe de base)
        /// </summary>
        /// <param name="unNumosia">numéro OSIA</param>
        /// <param name="unNom">Nom</param>
        /// <param name="unPrenom">Prénom</param>
        /// <param name="uneRue">Adresse'</param>
        /// <param name="uneVille">Ville</param>
        /// <param name="UnCodePostal">Code Postal</param>
        /// <param name="unFongecif">Nom fongecif</param>
        /// <param name="unTypeCIF">type de CIF</param>
        public MStagiaireCIF(Int32 unNumosia, String unNom, String unPrenom, String uneRue, String uneVille, String UnCodePostal,
            String unFongecif, String unTypeCIF, String codeSection)
            : base(unNumosia,  unNom,  unPrenom,  uneRue,  uneVille,  UnCodePostal, codeSection)
        {
            this.FongecifStagiaire = unFongecif;
            this.CodeSection = codeSection;
            this.TypeCifStagiaire = unTypeCIF;
        }
        /// <summary>
        /// nom Fongecif
        /// </summary>
        private String fongecifStagiaire;

        /// <summary>
        /// obtient ou définit nom Fongecif
        /// </summary>
        public String FongecifStagiaire
        {
            get
            {
                return this.fongecifStagiaire;
            }
            set
            {
                this.fongecifStagiaire = value;
            }
        }

        /// <summary>
        /// type CIF (CDD, CDI, TT)
        /// </summary>
        private String typeCifStagiaire;

        /// <summary>
        /// obtient ou définit type CIF (CDD, CDI, TT)
        /// </summary>
        public String TypeCifStagiaire
        {
            get
            {
                return this.typeCifStagiaire;
            }
            set
            {
                if (value == "CDI" || value == "CDD" || value == "TT")
                {
                    this.typeCifStagiaire = value;
                }
                else
                {
                    throw new Exception("Erreur de type CIF");
                }
            }
        }

        /// <summary>
        /// 
        /// A MODIFIER CAR copier/Coller de la fonction de la classe DE (update: ok)
        /// </summary>
        /// <param name="connexion"></param>
        /// <returns></returns>
        // ecrit la requete en bdd à faire executer par l'object de connexion
        public override Boolean Save(ConnexionBDD connexion)
        {
            string query = "INSERT INTO stagiaire (numOsia, prenom, nom, rue, ville, cp, de, remuafpa, typecif, fondgestion, codesection) VALUES('', '"
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
        "','"
        + false +
        "','"
        + false +
        "','"
        + this.typeCifStagiaire +
        "','"
        + this.FongecifStagiaire +
        "','"
        + this.CodeSection +
        "')";

            int result = connexion.executeQuery(query);

            if (result == 1) { return true; }

            // mieux : lever une exception
            return false;
        }
    }
}
