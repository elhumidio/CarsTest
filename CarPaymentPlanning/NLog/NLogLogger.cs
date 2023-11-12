using System.Web;
using NLog;

namespace CarPaymentPlanning.NLog
{
    public class NLogLogger : ILogger
    {
        private Logger _logger;

        public NLogLogger()
        {
            _logger = LogManager.GetCurrentClassLogger();
        }

        /// <summary>
        /// Metodo para los errores de Pedidos Excel. ubicación: /logs/PedidosExcel
        /// </summary>
        /// <param name="message"></param>
        public void Info(string message)
        {
            _logger.Info(message);
        }

        /// <summary>
        /// Metodo para los logs del tunel de afiliación online
        /// </summary>
        /// <param name="message"></param>
        public void Trace(string message)
        {
            _logger.Trace(message);
        }

        /// <summary>
        /// Metodo para los logs de la web de clientes
        /// </summary>
        /// <param name="message"></param>
        public void Warn(string message)
        {
            _logger.Warn(message);
        }

        /// <summary>
        /// Metodo para los logs de la web de Afiliados
        /// </summary>
        /// <param name="message"></param>
        public void Debug(string message)
        {
            _logger.Debug(message);
        }

        /// <summary>
        /// Método para los logs de la web de usuarios
        /// </summary>
        /// <param name="message"></param>
        public void Error(string message)
        {
            _logger.Error(message);
        }

        /// <summary>
        /// Método para los logs del tunel de compra
        /// </summary>
        /// <param name="message"></param>
        public void Fatal(string message)
        {
            _logger.Fatal(message);
        }

    }
}