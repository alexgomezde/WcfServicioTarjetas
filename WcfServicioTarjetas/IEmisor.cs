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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IEmisor
    {

        [OperationContract]
        string GetData(int value);

        [OperationContract]
        IEnumerable<EMISOR> ConsultarEmisor();

        [OperationContract]
        string ObtenerEmisorTarjeta(string numeroTarjeta);

        [OperationContract]
        TARJETA ObtenerInformacionTarjeta(string numeroTarjeta);

        [OperationContract]
        string ValidarTarjeta(string numeroTarjeta);


        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        // TODO: Add your service operations here
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
