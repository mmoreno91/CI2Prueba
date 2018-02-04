using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Collections.Generic;

namespace CI2.PruebasUnitariasSW
{
    public class RootObject
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
        public string userName { get; set; }
        //public string __invalid_name__.issued { get; set; }
        //public string __invalid_name__.expires { get; set; }
    }

    public class ResultadoWSCrear
    {
        public Int64 IdTarea { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaActualizacion { get; set; }
        public string IdUsuario { get; set; }
    }

    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestWSObtenerToken()
        {
            using (var cliente = new WebClient())
            {
                var url = "http://localhost:52788/token";
                var nombreUsuario = "manuelmorenotarazona91@gmail.com";
                var contraseña = "Mmoreno.91";
                string token = "";

                cliente.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

                string tokenComoString = cliente.UploadString(url, "POST", $"grant_type=password&username={nombreUsuario}&password={contraseña}");

                //var valores = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(tokenComoString);


                //if (valores.ContainsKey("access_token"))
                //{
                //     token = valores["access_token"];
                //}

                var valores = Newtonsoft.Json.JsonConvert.DeserializeObject<RootObject>(tokenComoString);

                if (valores != null)
                {
                    token = valores.access_token;
                }

                Assert.IsTrue(token != "");
            }
        }

        [TestMethod]
        public void TestWSConsultarTareas_SinCredenciales()
        {
            using (var cliente = new WebClient())
            {
                var url = "http://localhost:52788/api/tareas/consultar";

                var resultado = cliente.DownloadString(url);

                Assert.IsTrue(!string.IsNullOrWhiteSpace(resultado) && resultado.Contains("<html>"));
            }
        }

        [TestMethod]
        public void TestWSConsultarTareas_ConCredenciales()
        {
            var urlToken = "http://localhost:52788/token";
            var nombreUsuario = "manuelmorenotarazona91@gmail.com";
            var contraseña = "Mmoreno.91";
            string token = "";

            //Obtener token
            using (var cliente = new WebClient())
            {
                cliente.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

                string tokenComoString = cliente.UploadString(urlToken, "POST", $"grant_type=password&username={nombreUsuario}&password={contraseña}");

                var valores = Newtonsoft.Json.JsonConvert.DeserializeObject<RootObject>(tokenComoString);

                if (valores != null)
                {
                    token = valores.access_token;
                }
            }

            //Llamar Ws con token
            using (var cliente = new WebClient())
            {
                var url = "http://localhost:52788/api/tareas/consultar";

                //cliente.Headers.Add("Content-Type", "application/json; charset=utf-8");
                cliente.Headers.Add("Authorization", "Bearer " + token);

                var resultado = cliente.DownloadString(url);

                Assert.IsTrue(!string.IsNullOrWhiteSpace(resultado) && !resultado.Contains("<html>"));
            }
        }

        [TestMethod]
        public void TestWSCrearTareas_ConCredenciales()
        {
            var urlToken = "http://localhost:52788/token";
            var nombreUsuario = "manuelmorenotarazona91@gmail.com";
            var contraseña = "Mmoreno.91";
            string token = "";

            //Obtener token
            using (var cliente = new WebClient())
            {
                cliente.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

                string tokenComoString = cliente.UploadString(urlToken, "POST", $"grant_type=password&username={nombreUsuario}&password={contraseña}");

                var valores = Newtonsoft.Json.JsonConvert.DeserializeObject<RootObject>(tokenComoString);

                if (valores != null)
                {
                    token = valores.access_token;
                }
            }

            //Llamar Ws con token
            using (var cliente = new WebClient())
            {
                var url = "http://localhost:52788/api/tareas/crear";

                //cliente.Headers.Add("Content-Type", "application/json; charset=utf-8");
                cliente.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                cliente.Headers.Add("Authorization", "Bearer " + token);
                string Descripcion = "Ejemplo descripción";
                DateTime FechaVencimiento = DateTime.Now;
                bool Estado = true;
                string resultado = cliente.UploadString(url, "POST", $"descripcion={Descripcion}&estado={Estado}&FechaVencimiento={FechaVencimiento}");

                var valores = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultadoWSCrear>(resultado);

                Assert.AreNotEqual(valores, null);
                Assert.IsTrue(valores.IdTarea > 0);
            }
        }
    }
}
