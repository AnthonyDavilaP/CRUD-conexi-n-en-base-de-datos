using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsEF6Demo.Data;
using WinFormsEF6Demo.Models;

namespace WinFormsEF6Demo.Forms
{
    public partial class FormBodega : Form
    {
        private int? _idSeleccionado = null;
        public FormBodega()
        {
            InitializeComponent();
        }

        private void FormBodega_Load(object sender, EventArgs e)
        {
            CargarDatos();
        }
        private void CargarDatos()
        {
            using (var db = new AppDb())
            {
                dgvBodega.DataSource = db.Bodegas
                    .OrderBy(c => c.Codigo)
                    .Select(c => new { c.Codigo,c.Descripcion, c.Ubicacion, c.Responsable })
                    .ToList();
            }
            dgvBodega.ClearSelection();
            _idSeleccionado = null;
        }
        private void LimpiarFormulario()
        {
            txtDescripcion.Text = "";
            txtUbicacion.Text = "";
            txtResponsable.Text = "";
            errorProvider1.SetError(txtDescripcion, "");
            errorProvider1.SetError(txtUbicacion, "");
            errorProvider1.SetError(txtResponsable, "");
            _idSeleccionado = null;
        }
        private bool ValidarFormulario()
        {
            bool ok = true;
            if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
            {
                errorProvider1.SetError(txtDescripcion, "Descripcion es obligatorio");
                ok = false;
            }
            else errorProvider1.SetError(txtDescripcion, "");

            if (string.IsNullOrWhiteSpace(txtUbicacion.Text))
            {
                errorProvider1.SetError(txtUbicacion, "Ubicacion es obligatorio");
                ok = false;
            }
            else errorProvider1.SetError(txtUbicacion, "");

            // Responsable es opcional en esta demo; 
            return ok;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            txtDescripcion.Focus();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarFormulario()) return;

            using (var db = new AppDb())
            {
                if (_idSeleccionado == null)
                {
                    var bdo = new Bodega
                    {
                  
                        Descripcion = txtDescripcion.Text.Trim(),
                        Ubicacion = txtUbicacion.Text.Trim(),
                        Responsable = txtResponsable.Text.Trim()
                    };
                    db.Bodegas.Add(bdo);
                }
                else
                {
                    var bdo = db.Bodegas.Find(_idSeleccionado.Value);
                    if (bdo == null) return;
                    bdo.Descripcion = txtDescripcion.Text.Trim();
                    bdo.Ubicacion = txtUbicacion.Text.Trim();
                    bdo.Responsable = txtResponsable.Text.Trim();
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
                MessageBox.Show("Seleccione una bodega de la lista.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
            txtDescripcion.Focus();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (_idSeleccionado == null)
            {
                MessageBox.Show("Seleccione una bodega para eliminar.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var r = MessageBox.Show("¿Eliminar la bodega seleccionada?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (r != DialogResult.Yes) return;

            using (var db = new AppDb())
            {
                var bdo = db.Bodegas.Find(_idSeleccionado.Value);
                if (bdo != null)
                {
                    db.Bodegas.Remove(bdo);
                    db.SaveChanges();
                }
            }
            CargarDatos();
            LimpiarFormulario();
        }
        private void dgvBodega_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvBodega.CurrentRow == null || dgvBodega.CurrentRow.Index < 0)
                return;

            var row = dgvBodega.CurrentRow;
            if (row.Cells["Codigo"] == null) return;

            _idSeleccionado = (int?)row.Cells["Codigo"].Value;
            if (_idSeleccionado == null) return;

           
            txtDescripcion.Text = row.Cells["Descripcion"].Value?.ToString() ?? "";
            txtUbicacion.Text = row.Cells["Ubicacion"].Value?.ToString() ?? "";
            txtResponsable.Text = row.Cells["Responsable"].Value?.ToString() ?? "";
        }
    }
}
