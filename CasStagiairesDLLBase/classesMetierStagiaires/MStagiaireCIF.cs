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
            String unFongecif, String unTypeCIF)
            : base(unNumosia,  unNom,  unPrenom,  uneRue,  uneVille,  UnCodePostal)
        {
            this.FongecifStagiaire = unFongecif;
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
    }
}
