using System;
using System.Data;
using System.Windows.Forms;
using CapaNegocio;

namespace CapaPresentacion
{
    public partial class Form1 : Form
    {
        CN_Productos objetoCN = new CN_Productos();
        private string idProducto = null;
        private bool Editar = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MostrarProductos();
        }

        private void MostrarProductos()
        {
            try
            {
                dataGridView1.DataSource = objetoCN.MostrarProd();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudieron mostrar los productos: " + ex.Message);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNombre.Text) || string.IsNullOrEmpty(txtPrecio.Text) || string.IsNullOrEmpty(txtStock.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos.");
                return;
            }

            try
            {
                if (Editar == false)
                {
                    objetoCN.InsertarProd(txtNombre.Text, txtDesc.Text, txtMarca.Text, txtPrecio.Text, txtStock.Text);
                    MessageBox.Show("Se insertó correctamente.");
                }
                else
                {
                    objetoCN.EditarProd(txtNombre.Text, txtDesc.Text, txtMarca.Text, txtPrecio.Text, txtStock.Text, idProducto);
                    MessageBox.Show("Se editó correctamente.");
                    Editar = false;
                }
                MostrarProductos();
                limpiarForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo guardar los datos por: " + ex.Message);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                Editar = true;
                txtNombre.Text = dataGridView1.CurrentRow.Cells["Nombre"].Value.ToString();
                txtMarca.Text = dataGridView1.CurrentRow.Cells["Marca"].Value.ToString();
                txtDesc.Text = dataGridView1.CurrentRow.Cells["Descripcion"].Value.ToString();
                txtPrecio.Text = dataGridView1.CurrentRow.Cells["Precio"].Value.ToString();
                txtStock.Text = dataGridView1.CurrentRow.Cells["Stock"].Value.ToString();
                idProducto = dataGridView1.CurrentRow.Cells["Id"].Value.ToString();
            }
            else
            {
                MessageBox.Show("Seleccione una fila por favor.");
            }
        }

        private void limpiarForm()
        {
            txtDesc.Clear();
            txtMarca.Clear();
            txtPrecio.Clear();
            txtStock.Clear();
            txtNombre.Clear();
            idProducto = null; // Asegúrate de limpiar también el idProducto
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                idProducto = dataGridView1.CurrentRow.Cells["Id"].Value.ToString();
                try
                {
                    objetoCN.EliminarProd(idProducto);
                    MessageBox.Show("Eliminado correctamente.");
                    MostrarProductos();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("No se pudo eliminar el producto por: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Seleccione una fila por favor.");
            }
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            filtrar();
        }
        private void filtrar()
        {
            objetoCN = new CN_Productos();
            string nombre = txtBuscar.Text;
            dataGridView1.DataSource = objetoCN.BuscarPorNombre(nombre);
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}


