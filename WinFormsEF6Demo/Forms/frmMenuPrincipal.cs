using System;
using System.Windows.Forms;

namespace WinFormsEF6Demo.Forms
{
    public partial class frmMenuPrincipal : Form
    {
        private bool _permitirCerrar = false;

        public frmMenuPrincipal()
        {
            InitializeComponent();
        }

        private void frmMenuPrincipal_Load(object sender, EventArgs e)
        {
            toolStatusUsuario.Text = "Usuario Conectado: " + (WinFormsEF6Demo.Credenciales.Usuario ?? "(desconocido)");
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _permitirCerrar = true;
            this.Close();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (!_permitirCerrar && e.CloseReason == CloseReason.UserClosing)
            {
                // cancelar cierre por X/Alt+F4 si quieres
                var r = MessageBox.Show("¿Desea salir de la aplicación?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (r == DialogResult.Yes)
                {
                    _permitirCerrar = true;
                }
                else
                {
                    e.Cancel = true;
                }
            }
            base.OnFormClosing(e);
        }

        private void clientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new FormClientes();
            frm.MdiParent = this;
            frm.Show();
        }

        private void bodegasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new FormBodega();
            frm.MdiParent = this;
            frm.Show();
        }

        private void productosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new FormProducto();
            frm.MdiParent = this;
            frm.Show();
        }
    }
}
