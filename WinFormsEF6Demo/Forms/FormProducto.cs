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
    public partial class FormProducto : Form
    {
        private int? _idSeleccionado = null;
        public FormProducto()
        {
            InitializeComponent();
        }
        private void FormProducto_Load(object sender, EventArgs e)
        {
            var estados = new[]
            {
                new { Valor = "E", Texto = "Existente" },
                new { Valor = "A", Texto = "Agotado" }
            };
            cmbEstado.DataSource = estados;
            cmbEstado.ValueMember = "Valor";
            cmbEstado.DisplayMember = "Texto";
            CargarDatos();

            if (dgvProductos.Columns["Pvp"] != null)
                dgvProductos.Columns["Pvp"].DefaultCellStyle.Format = "N2"; 

        }
        private void CargarDatos()
        {
            using (var db = new AppDb())
            {
                dgvProductos.DataSource = db.Productos
                    .OrderBy(c => c.Codigo)
                    .Select(c => new { c.Codigo, c.Descripcion, c.Estado, c.pvp, c.iva })
                    .ToList();
            }
            dgvProductos.ClearSelection();
            _idSeleccionado = null;
        }
        private void LimpiarFormulario()
        {
            txtDescripcion.Text = "";
            cmbEstado.SelectedIndex = 0;
            txtPvp.Text = "";
            txtIva.Text = "";
            errorProvider1.SetError(txtDescripcion, "");
            errorProvider1.SetError(cmbEstado, "");
            errorProvider1.SetError(txtPvp, "");
            errorProvider1.SetError(txtIva, "");
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

            if (string.IsNullOrWhiteSpace(txtPvp.Text))
            {
                errorProvider1.SetError(txtPvp, "PVP es obligatorio");
                ok = false;
            }
            else if (!double.TryParse(txtPvp.Text.Trim(), out _))
            {
                errorProvider1.SetError(txtPvp, "PVP debe ser un número válido");
                ok = false;
            }
            else
            {
                errorProvider1.SetError(txtPvp, "");
            }

            if (string.IsNullOrWhiteSpace(txtIva.Text))
            {
                errorProvider1.SetError(txtIva, "IVA es obligatorio");
                ok = false;
            }
            else if (!int.TryParse(txtIva.Text.Trim(), out _))
            {
                errorProvider1.SetError(txtIva, "IVA debe ser un número entero");
                ok = false;
            }
            else
            {
                errorProvider1.SetError(txtIva, "");
            }
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
                    var pro = new Producto
                    {

                        Descripcion = txtDescripcion.Text.Trim(),
                        Estado = cmbEstado.SelectedValue?.ToString(),
                        pvp = decimal.Parse(txtPvp.Text.Trim()),
                        iva = int.Parse(txtIva.Text.Trim()),
                    };
                    db.Productos.Add(pro);
                }
                else
                {
                    var pro = db.Productos.Find(_idSeleccionado.Value);
                    if (pro == null) return;
                    pro.Descripcion = txtDescripcion.Text.Trim();
                    pro.Estado = cmbEstado.SelectedValue?.ToString();
                    pro.pvp = decimal.Parse(txtPvp.Text.Trim());
                    pro.iva = int.Parse(txtIva.Text.Trim());
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
                MessageBox.Show("Seleccione un producto de la lista.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
            txtDescripcion.Focus();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (_idSeleccionado == null)
            {
                MessageBox.Show("Seleccione un producto para eliminar.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var r = MessageBox.Show("¿Eliminar el producto seleccionada?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (r != DialogResult.Yes) return;

            using (var db = new AppDb())
            {
                var pro = db.Productos.Find(_idSeleccionado.Value);
                if (pro != null)
                {
                    db.Productos.Remove(pro);
                    db.SaveChanges();
                }
            }
            CargarDatos();
            LimpiarFormulario();
        }

        private void dgvProductos_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvProductos.CurrentRow == null || dgvProductos.CurrentRow.Index < 0)
                return;

            var row = dgvProductos.CurrentRow;
            if (row.Cells["Codigo"] == null) return;

            _idSeleccionado = (int?)row.Cells["Codigo"].Value;
            if (_idSeleccionado == null) return;

            
            txtDescripcion.Text = row.Cells["Descripcion"].Value?.ToString() ?? "";
            cmbEstado.Text = row.Cells["Estado"].Value?.ToString() ?? "";
            txtPvp.Text = row.Cells["pvp"].Value != null
                ? Convert.ToDouble(row.Cells["pvp"].Value).ToString("F2")
                : "";
            txtIva.Text = row.Cells["iva"].Value?.ToString() ?? "";
        }
    }
}
