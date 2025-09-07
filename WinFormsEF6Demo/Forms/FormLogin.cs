using System;
using System.Windows.Forms;
using System.Linq;
using WinFormsEF6Demo.Data;
using WinFormsEF6Demo.Utils;

namespace WinFormsEF6Demo.Forms
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            bool valido = true;
            if (string.IsNullOrWhiteSpace(txtUsuario.Text))
            {
                errorProvider1.SetError(txtUsuario, "Ingrese el usuario");
                valido = false;
            }
            else errorProvider1.SetError(txtUsuario, "");

            if (string.IsNullOrWhiteSpace(txtClave.Text))
            {
                errorProvider1.SetError(txtClave, "Ingrese la clave");
                valido = false;
            }
            else errorProvider1.SetError(txtClave, "");

            if (!valido) return;

            using (var db = new AppDb())
            {
                var nombre = txtUsuario.Text.Trim();
                var user = db.Usuarios.FirstOrDefault(u => u.Login == nombre && u.Estado == "A");
                if (user == null || !PasswordHasher.Verify(txtClave.Text, user.PasswordHash))
                {
                    MessageBox.Show("Usuario y/o clave incorrectos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Credenciales.Usuario = user.Login;
                var mdi = new frmMenuPrincipal();
                mdi.FormClosed += (s, args) => this.Close();
                mdi.Show();
                this.Hide();
            }
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
