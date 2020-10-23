using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using WcfServicioTarjetas.Modelo;

namespace WcfServicioTarjetas
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class ServiceEmisor : IEmisor
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public IEnumerable<EMISOR> ConsultarEmisor()
        {
            using (TarjetasEntities contexto = new TarjetasEntities())
                return contexto.EMISORs.ToList();
        }

        public string ObtenerEmisorTarjeta(string numeroTarjeta)
        {
            using (TarjetasEntities contexto = new TarjetasEntities())
            {

                string substring = numeroTarjeta.Substring(0, 4);

                var listaEmisores = (from EMISORs in contexto.EMISORs select EMISORs).OrderByDescending(emisor => emisor.EMI_PREFIJO);

                foreach (EMISOR emisor in listaEmisores)
                {
                    if (substring.StartsWith(emisor.EMI_PREFIJO))
                        return emisor.EMI_DESCRIPCION;
                }

                return "Emisor Desconocido";

            }
        }

        public TARJETA ObtenerInformacionTarjeta(string numeroTarjeta)
        {
            using (TarjetasEntities contexto = new TarjetasEntities())
            {
                var tarjeta = (from TARJETAs in contexto.TARJETAs where TARJETAs.TAR_NUMERO == numeroTarjeta select TARJETAs).FirstOrDefault<TARJETA>();
                return tarjeta;
            }
        }

        public string ValidarTarjeta(string numeroTarjeta)
        {
            using (TarjetasEntities contexto = new TarjetasEntities())
            {
                var tarjeta = (from TARJETAs in contexto.TARJETAs where TARJETAs.TAR_NUMERO == numeroTarjeta select TARJETAs).FirstOrDefault<TARJETA>();

                DateTime fechaActual = DateTime.Now;
                string estado = tarjeta.TAR_ESTADO;

                if (estado.ToLower().Equals("activa") && tarjeta.TAR_FECHA_VENCIMIENTO >= fechaActual)
                    return "Tarjeta Válida";

                return "Tarjeta Inválida";
            }
        }


        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
    }
}
