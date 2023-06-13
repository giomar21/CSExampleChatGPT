using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CS.Example.Services
{
    /// <summary>
    /// Cliente de peticiones REST
    /// </summary>
    public static class HttpClient
    {
        /// <summary>
        /// Realiza una petición REST
        /// </summary>
        /// <typeparam name="T">Tipo de dato de retorno</typeparam>
        /// <typeparam name="X">Tipo de dato de entrada</typeparam>
        /// <param name="args"> Objeto de entrada a enviar en el servicio</param>
        /// <param name="url">URL de petición</param>
        /// <param name="contentType">tipo de contenido</param>
        /// <param name="method">Metodo de peticion REST <see cref="HttpMethods"/></param>
        /// <param name="headers">Encabezados adicionales</param>
        /// <param name="timeout">Tiempo límite en milisegundos que puede durar la petición</param>
        /// <returns></returns>
        public static async Task<T> JsonRequest<T, X>(X args, string url, string contentType, string method, Dictionary<string, string> headers = null, int timeout = 60000)
        {
            string webAddr = url;
            var Json = JsonConvert.SerializeObject(args, Formatting.Indented);
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(webAddr);

            httpWebRequest.ContentType = contentType;
            httpWebRequest.Method = method;
            httpWebRequest.Timeout = timeout;

            if (headers != null)
            {
                foreach (var item in headers)
                {
                    httpWebRequest.Headers.Add(item.Key, item.Value);
                }
            }

            if (Json.Length > 0 && method.ToUpper() != HttpMethods.Get)
            {
                using (var streamWriter = new StreamWriter(await httpWebRequest.GetRequestStreamAsync()))
                {
                    var json = Json;
                    streamWriter.Write(json);
                    streamWriter.Flush();
                }
            }

            var responseText = await GetResponse(httpWebRequest);

            if (typeof(T) == typeof(string)) return (T)Convert.ChangeType(responseText, typeof(T));
            return JsonConvert.DeserializeObject<T>(responseText);
        }


        /// <summary>
        /// Recupera el response de una peticion REST
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private static async Task<string> GetResponse(HttpWebRequest request)
        {
            HttpWebResponse httpWebResponse;
            string encodedData = string.Empty;

            try
            {
                httpWebResponse = (HttpWebResponse)request.GetResponseAsync().Result;

                using (var srStr = new StreamReader(httpWebResponse.GetResponseStream()))
                {
                    encodedData = await srStr.ReadToEndAsync();
                }
            }

            catch (WebException ex)
            {
                if (ex.Response != null)
                {
                    using (var sr = new StreamReader(ex.Response.GetResponseStream()))
                    {
                        encodedData = await sr.ReadToEndAsync();
                    }
                }
                else
                    encodedData = "{\"errors\":\"" + $"{ex.Message}" + "\"}";
            }
            catch (Exception e)
            {
                encodedData = "{\"errors\":\"" + e.Message + "\"}";
            }

            return encodedData;
        }
    }
}
