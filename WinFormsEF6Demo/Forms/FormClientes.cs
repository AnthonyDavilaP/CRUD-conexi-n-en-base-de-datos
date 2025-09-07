using System;
using System.Linq;
using System.Windows.Forms;
using WinFormsEF6Demo.Data;
using WinFormsEF6Demo.Models;

namespace WinFormsEF6Demo.Forms
{
    public partial class FormClientes : Form
    {
        private int? _idSeleccionado = null;

        public FormClientes()
        {
            InitializeComponent();
        }

        private void FormClientes_Load(object sender, EventArgs e)
        {
            CargarDatos();
        }

        private void CargarDatos()
        {
            using (var db = new AppDb())
            {
                dgvClientes.DataSource = db.Clientes
                    .OrderBy(c => c.Nombre)
                    .Select(c => new { c.ClienteId, c.Nombre, c.Email })
                    .ToList();
            }
            dgvClientes.ClearSelection();
            _idSeleccionado = null;
        }

        private void LimpiarFormulario()
        {
            txtNombre.Text = "";
            txtEmail.Text = "";
            errorProvider1.SetError(txtNombre, "");
            errorProvider1.SetError(txtEmail, "");
            _idSeleccionado = null;
        }

        private bool ValidarFormulario()
        {
            bool ok = true;
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                errorProvider1.SetError(txtNombre, "Nombre es obligatorio");
                ok = false;
            }
            else errorProvider1.SetError(txtNombre, "");

            
            return ok;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            txtNombre.Focus();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarFormulario()) return;

            using (var db = new AppDb())
            {
                if (_idSeleccionado == null)
                {
                    var cli = new Cliente
                    {
                        Nombre = txtNombre.Text.Trim(),
                        Email = txtEmail.Text.Trim()
                    };
                    db.Clientes.Add(cli);
                }
                else
                {
                    var cli = db.Clientes.Find(_idSeleccionado.Value);
                    if (cli == null) return;
                    cli.Nombre = txtNombre.Text.Trim();
                    cli.Email = txtEmail.Text.Trim();
                }
                db.SaveChanges();
            }

            CargarDatos();
            LimpiarFormulario();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (_idSeleccionado == null)
            {
                MessageBox.Show("Seleccione un cliente de la lista.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
            txtNombre.Focus();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (_idSeleccionado == null)
            {
                MessageBox.Show("Seleccione un cliente para eliminar.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var r = MessageBox.Show("¿Eliminar el cliente seleccionado?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (r != DialogResult.Yes) return;

            using (var db = new AppDb())
            {
                var cli = db.Clientes.Find(_idSeleccionado.Value);
                if (cli != null)
                {
                    db.Clientes.Remove(cli);
                    db.SaveChanges();
                }
            }
            CargarDatos();
            LimpiarFormulario();
        }

        private void dgvClientes_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvClientes.CurrentRow == null || dgvClientes.CurrentRow.Index < 0)
                return;

            var row = dgvClientes.CurrentRow;
            if (row.Cells["ClienteId"] == null) return;

            _idSeleccionado = (int?)row.Cells["ClienteId"].Value;
            if (_idSeleccionado == null) return;

            
            txtNombre.Text = row.Cells["Nombre"].Value?.ToString() ?? "";
            txtEmail.Text = row.Cells["Email"].Value?.ToString() ?? "";
        }
    }
}
