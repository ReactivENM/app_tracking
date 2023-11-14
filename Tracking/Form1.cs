using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Windows.Forms;
using Controllers.PackageController;
using Newtonsoft.Json;

namespace Tracking
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            input_id.KeyPress += new KeyPressEventHandler(input_id_KeyPress);
        }

        private async void btnSubmit_Click(object sender, EventArgs e)
        {
            bool fieldsValid = validateFields();
            if (!fieldsValid) return;

            Dictionary<string, string> formattedStatus = new Dictionary<string, string>();
            formattedStatus.Add("en_espera", "En espera");
            formattedStatus.Add("en_viaje", "En viaje");
            formattedStatus.Add("entregado", "Entregado");

            string packageId = input_id.Text;
            var pkgStatus = await GetPackageStatus(packageId);
            Responses.PackageStatus status = JsonConvert.DeserializeObject<Responses.PackageStatus>(pkgStatus);
            if (status == null)
            {
                MessageBox.Show("No pudimos encontrar este paquete", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            MessageBox.Show("El paquete se encuentra " + formattedStatus[status.Estado] + ".", "Estado del paquete", MessageBoxButtons.OK);
        }

        private async Task<string> GetPackageStatus(string packageId)
        {
            PackageController controller = new PackageController();
            string estado = await controller.GetStatus(packageId);

            return estado;
        }

        private bool validateFields()
        {
            string input = input_id.Text;
            if(input.Length == 0)
            {
                MessageBox.Show("Debes introducir un ID", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private void input_id_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
