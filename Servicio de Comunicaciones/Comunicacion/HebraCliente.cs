﻿using MensajeroModel.DAL;
using MensajeroModel.DTO;
using ServicioDeComunicaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimuladorMedidorElectrico
{
    
    public class HebraCliente
    {
        private ClienteCom clienteCom;
        private IMensajesDAL mensajesDAL = MensajesDALArchivos.GetInstancia();
        public HebraCliente(ClienteCom clienteCom)
        {
            this.clienteCom = clienteCom;
        }

        public void ejecutar()
        {
          
            // traemos el codigo que atiende a los clientes
            clienteCom.Escribir("Ingrese numero de medidor: ");
           
            string nroMedidorString = clienteCom.Leer();
          
            //Tomamos la fecha del sistema
            string fecha = DateTime.Now.ToString("yyyy-MM-dd--HH:mm:ss");

            // solicitamos el consumo del medidor
            clienteCom.Escribir(" ¿Cual es el valor de consumo de su medidor? " +
                "\nIngrese el valor en kilowats");
            //Leemos los datos de consumo
            string valorConsumoString = clienteCom.Leer();

            Mensaje mensaje = new Mensaje()
            {
                NroMedidorInt = Int32.Parse(nroMedidorString),
                Fecha = fecha,
                ValorConsumoFloat = float.Parse(valorConsumoString)
            };
            lock (mensajesDAL)
            { 
            mensajesDAL.AgregarMensaje(mensaje);
            }
            clienteCom.Desconectar();
        }

    }
}
