using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS.Example.Services
{
    /// <summary>
    /// Permite seleccionar los más comunes tipos de métodos o verbos HTTP para peticiones REST
    /// </summary>
    public static class HttpMethods
    {
        public static string Get => "GET";
        public static string Post => "POST";
        public static string Put => "PUT";
        public static string Delete => "DELETE";
    }
}
