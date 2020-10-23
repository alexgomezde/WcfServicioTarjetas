using AplicacionTarjetas.ServicioTarjetas;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AplicacionTarjetas
{
    public partial class frmPrincipal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnObtenerEmisores_Click(object sender, EventArgs e)
        {
            using (ServicioTarjetas.EmisorClient cliente = new ServicioTarjetas.EmisorClient())
            {
                dgvEmisores.DataSource = cliente.ConsultarEmisor();
                dgvEmisores.DataBind();
            }

        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNumeroTarjeta.Text))
            {
                using (ServicioTarjetas.EmisorClient cliente = new ServicioTarjetas.EmisorClient())
                {
                    lblResultado.Text = "";

                    int index = cbxOpciones.SelectedIndex;

                    if (index == 0)
                        lblResultado.Text = "El emisor de la tarjeta es: " + cliente.ObtenerEmisorTarjeta(txtNumeroTarjeta.Text);

                    if (index == 1)
                    {
                        var tarjeta = cliente.ObtenerInformacionTarjeta(txtNumeroTarjeta.Text);

                        var infoTarjeta = new[] { tarjeta };

                        dgvTarjeta.DataSource = infoTarjeta;
                        dgvTarjeta.DataBind();
                    }

                    if (index == 2)
                        lblResultado.Text = cliente.ValidarTarjeta(txtNumeroTarjeta.Text);
                }
            }
            else
                lblResultado.Text = "Por favor ingrese un número de tarjeta para continuar";
        }
    }
}