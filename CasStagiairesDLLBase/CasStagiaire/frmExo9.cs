using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;



namespace classesMetierStagiaires
{
    /// <summary>
    /// form de démarrage : datagridview des stagiaires (de la section CDI pour commencer)
    /// </summary>
    public partial class frmExo9 : Form
    {

        /// <summary>
        /// la section de stagiaires gérée par ce form
        /// </summary>
        private MSection laSection; 
        
        /// <summary>
        /// Constructeur 
        /// (initialise la collection de sections et insère en dur la section CDI)
        /// </summary>
        public frmExo9()
        {
            InitializeComponent();
            // initialisation de la collection de sections
            Donnees.Sections = new MSections();

            // TODO : initialisation du jeu d'essai ==> récupérer depuis BDD
            this.init();
            
            // afficher la liste des stagiaires de la section
            this.afficheStagiaires();
        }

        /// <summary>
        /// initialisation du jeu d'essai 
        /// </summary>
        private void init()
        {
            // pour commencer, une seule section référencée "en dur" dans ce programme
            // instancie la section
            this.laSection = new MSection("CDI1", "Concepteur Développeur Informatique");
            // l'ajoute dans la collection des sections gérée par la classe de collection
            Donnees.Sections.Ajouter(this.laSection);
            
            // ********** partie à modifier pour récupérer les données depuis la BDD ***********
            //----------------------------------------------------------------------------------
            // instancie en dur un stagiaire 
            MStagiaire unStagiaire;
            unStagiaire = new MStagiaireDE(11111, "DUPOND", "Albert", "12 rue des Fleurs", "NICE", "06300", false);

            // ajoute le stagiaire instancié à la collection de la section CDI1
            this.laSection.Ajouter(unStagiaire);
        }

        /// <summary>
        /// rétablit la source de données de la dataGridView
        /// et rafraîchit son affichage
        /// </summary>
        private void afficheStagiaires()
        {
            // déterminer l'origine des données à afficher : 
            // appel de la méthode de la classe MSection 
            // qui alimente et retourne copie de sa 
            // collection de stagiaires sous forme de datatable
            this.grdStagiaires.DataSource = laSection.ListerStagiaires(); 
            // refraîchir l'affichage
            this.grdStagiaires.Refresh();
        }

        /// <summary>
        /// Bouton  ajouter : instancie un form de saisie stagiaire
        /// et lui passe la référence à la section en cours
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAjouter_Click(object sender, EventArgs e)
        {
            // instancier un form de saisie de stagiaire et l'afficher en modal
            // il faut préciser la référence à la section que l'on traite
            frmAjoutStagiaire frmAjout = new frmAjoutStagiaire(this.laSection);
            // si on sort de la saisie par OK
            if (frmAjout.ShowDialog() == DialogResult.OK)
            {
                // régénèrer l'affichage du dataGridView 
                afficheStagiaires();
            }
        }

        /// <summary>
        /// Double-clic sur le datagridview :
        /// ouvrir la feuille détail en y affichant
        /// le stagiaire correspondant à la ligne double-cliquée
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdStagiaires_DoubleClick(object sender, EventArgs e)
        {
            // ouvrir la feuille détail en y affichant 
            // le stagiaire correspondant à la ligne double-cliquée
            MStagiaire leStagiaire;
            Int32 laCle; // clé (=numOSIA) du stagiaire dans la collection

            // récupérer clé du stagiaire cliqué en DataGridView
            laCle = (Int32)this.grdStagiaires.CurrentRow.Cells[0].Value;
            // instancier un objet stagiaire pointant vers 
            // le stagiaire d'origine dans la collection
            leStagiaire = this.laSection.RestituerStagiaire(laCle);
            // instancier un form détail pour ce stagiaire
            frmVisuStagiaire frmVisu = new frmVisuStagiaire(leStagiaire);
            // personnaliser le titre du form
            frmVisu.Text = leStagiaire.ToString();
            // afficher le form détail en modal
            frmVisu.ShowDialog();

            // en sortie du form détail, refraichir la datagridview
            this.afficheStagiaires();

        }

        /// <summary>
        /// bouton fermer : fermer le form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFermer_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// bouton supprimer : gérer la suppression du stagiaire pointé dans la datagridview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSupprimer_Click(object sender, EventArgs e)
        {
            // si un stagiaire est pointé dans la datagridview
            if (this.grdStagiaires.CurrentRow != null)
            {
                // récupérer la clé du stagiaire pointé
                Int32 cleStagiaire;
                cleStagiaire = (Int32)this.grdStagiaires.CurrentRow.Cells[0].Value;
                // demander confirmation de la suppression
                // NB: messagebox retourne une valeur exploitable !
                if (MessageBox.Show("Voulez-vous supprimer le stagiaire numéro :" + cleStagiaire.ToString(), "Suppression", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    // supprimer et compacter la collection
                    this.laSection.Supprimer(cleStagiaire);

                    // ******* ajouter l'impact en BDD ********
                    // ----------------------------------------


                    // réafficher la datagridview
                    afficheStagiaires();
                }
            }

        }

        /// <summary>
        /// gestion bouton supprimer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdStagiaires_SelectionChanged(object sender, EventArgs e)
        {
            // admirer la syntaxe optimisée...
            this.btnSupprimer.Enabled = (this.grdStagiaires.CurrentRow == null ? false : true);

        }

        private void frmExo9_Load(object sender, EventArgs e)
        {

        }
    }
    
}