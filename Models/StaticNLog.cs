using System;
using NLog;

namespace webapi_EF_mysql.Models
{
    public static class StaticNLog
    {
        private static Logger logger;

        static StaticNLog()
        {
            logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();            
        }
        
        public static void GerarLog(string log)
        {
            try
            {
                logger.Debug(log);                        
            }
            catch (Exception exception)
            {                        
                logger.Error(exception, "ALGUMA EXCEÇÃO ACONTECEU E NÃO FOI POSSIVEL GERAR LOG.");
                throw;
            }
            finally
            {
                NLog.LogManager.Shutdown();
            }
        }
    }
}