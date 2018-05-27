using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace classesMetierStagiaires
{
    /// <summary>
    /// form de visu / modif d'un stagiaire
    /// </summary>
    public partial class frmVisuStagiaire : frmStagiaire
    {

        /// <summary>
        /// le stagiaire à afficher / modifier
        /// (on n'affecte pas directement sur le stagiaire fourni
        /// et l'utilisateur pourra abandonner les saisies en cours par bouton Refaire)
        /// </summary>
        private MStagiaire leStagiaire;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="unStagiaire"></param>
        public frmVisuStagiaire(MStagiaire unStagiaire) 
        {
            InitializeComponent();

            this.leStagiaire = unStagiaire;

        }


        /// <summary>
        /// fermer sans incidence
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFermer_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        /// <summary>
        /// abandon de modif en cours ==> réafficher anciennes valeurs
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefaire_Click(object sender, EventArgs e)
        {
            // réafficher le stagiaire d'origine
            this.afficheStagiaire();

        }

        /// <summary>
        /// impacter les modifications saisies sur le form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnValider_Click(object sender, EventArgs e)
        {
            try
            {
                //TODO : ajouter contrôles de saisie
                // modifier les valeurs du stagiaire pointé par la ref temporaire
                // (sauf numéro OSIA non modifiable)
                this.leStagiaire.NomStagiaire = this.txtNom.Text;
                this.leStagiaire.PrenomStagiaire = this.txtPrenom.Text;
                this.leStagiaire.RueStagiaire = this.txtAdresse.Text;
                this.leStagiaire.VilleStagiaire = this.txtVille.Text;
                this.leStagiaire.CodePostalStagiaire = this.txtCodePostal.Text;

                // ******* ajouter l'impact en BDD ********
                // ----------------------------------------


                // fermer le form
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur : \n" + ex.Message, "Modification de stagiaire");

            }
        }


        /// <summary>
        /// afficher le stagiaire reçu sur le form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmVisuStagiaire_Load(object sender, EventArgs e)
        {
            // afficher le stagiaire temporaire
            this.afficheStagiaire();
        }


        /// <summary>
        /// affiche en textbox les données du stagiaire reçu par ce form
        /// </summary>
        private void afficheStagiaire()
        {
            // affecter les textbox
            this.txtOSIA.Text = this.leStagiaire.NumOsiaStagiaire.ToString();
            this.txtNom.Text = this.leStagiaire.NomStagiaire;
            this.txtPrenom.Text = this.leStagiaire.PrenomStagiaire;
            this.txtAdresse.Text = this.leStagiaire.RueStagiaire;
            this.txtVille.Text = this.leStagiaire.VilleStagiaire;
            this.txtCodePostal.Text = this.leStagiaire.CodePostalStagiaire;
            this.lblValeurMoyenne.Text = this.leStagiaire.DonnerMoyenne().ToString();
            // btn Radio
            if (this.leStagiaire is MStagiaireCIF)
            {
                this.rbtCIF.Checked = true;
            }
            else
            {
                this.rbtDE.Checked = true;
            }
            // placer le curseur sur le nom
            this.txtNom.Focus();

        }

        /// <summary>
        /// bouton saisir note : afficher le form complémentaire en modal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaisirNote_Click(object sender, EventArgs e)
        {
            // instancier et impacter la note saisie sur le stagiaire
            frmSaisieNote frmSaisie = new frmSaisieNote(this.leStagiaire);
            if(frmSaisie.ShowDialog() == DialogResult.OK)
            {
                // réafficher le stagiaire à jour de la nouvelle note
                this.afficheStagiaire();
            }
            
        }
    }
}

