using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Collections.Generic;

namespace CI2.PruebasUnitariasSW
{
    public class Token
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
        public string userName { get; set; }

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


    public class ResultadoWSActualizar : ResultadoWSCrear
    {

    }

    public class ResultadoWSBorrar
    {

    }

    [TestClass]
    public class PruebasUnitariasWS
    {
        [TestMethod]
        public string TestWSObtenerToken(string nombreUsuario, string contraseña)
        {
            string token = "";
            using (var cliente = new WebClient())
            {
                if (!string.IsNullOrEmpty(nombreUsuario))
                {
                    var url = "http://localhost:52788/token";
                    //nombreUsuario = "manuelmorenotarazona91@gmail.com";
                    //contraseña = "Mmoreno.91";
                    cliente.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                    string tokenComoString = cliente.UploadString(url, "POST", $"grant_type=password&username={nombreUsuario}&password={contraseña}");
                    var valores = Newtonsoft.Json.JsonConvert.DeserializeObject<Token>(tokenComoString);
                    if (valores != null)
                    {
                        token = valores.access_token;
                    }
                }

                return token;
                Assert.IsTrue(token != "");
            }
        }

        public string WSObtenerToken()
        {
            string token = "";
            try
            {
                using (var cliente = new WebClient())
                {
                    var url = "http://localhost:52788/token";
                    string nombreUsuario = "manuelmorenotarazona91@gmail.com";
                    string contraseña = "Mmoreno.91";
                    cliente.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                    string tokenComoString = cliente.UploadString(url, "POST", $"grant_type=password&username={nombreUsuario}&password={contraseña}");
                    var valores = Newtonsoft.Json.JsonConvert.DeserializeObject<Token>(tokenComoString);
                    if (valores != null)
                    {
                        token = valores.access_token;
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }
            return token;
        }

        [TestMethod]
        public void TestWSConsultarTareas_SinCredenciales()
        {
            try
            {
                using (var cliente = new WebClient())
                {
                    var url = "http://localhost:52788/api/tareas/consultar";
                    var resultado = cliente.DownloadString(url);
                    Assert.IsTrue(!string.IsNullOrWhiteSpace(resultado) && resultado.Contains("<html>"));
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [TestMethod]
        public void TestWSConsultarTareas_ConCredenciales()
        {
            try
            {
                string token = WSObtenerToken();
                //Llamar Ws con token
                using (var cliente = new WebClient())
                {
                    var url = "http://localhost:52788/api/tareas/consultar?IdUsuario='9be6e696-0596-46e3-8f58-41926228c1ab'";
                    cliente.Headers.Add("Authorization", "Bearer " + token);
                    var resultado = cliente.DownloadString(url);
                    Assert.IsTrue(!string.IsNullOrWhiteSpace(resultado) && !resultado.Contains("<html>"));
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [TestMethod]
        public void TestWSCrearTareas_ConCredenciales()
        {
            try
            {
                string token = WSObtenerToken();
                //Llamar Ws con token
                using (var cliente = new WebClient())
                {
                    var url = "http://localhost:52788/api/tareas/crear";
                    cliente.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                    cliente.Headers.Add("Authorization", "Bearer " + token);
                    string Descripcion = "Ejemplo nueva tarea";
                    DateTime FechaVencimiento = DateTime.Now;
                    int Estado = 1;
                    string resultado = cliente.UploadString(url, "POST", $"descripcion={Descripcion}&estado={Estado}&FechaVencimiento={FechaVencimiento}");
                    var valores = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultadoWSCrear>(resultado);
                    Assert.AreNotEqual(valores, null);
                    Assert.IsTrue(valores.IdTarea > 0);
                }

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [TestMethod]
        public void TestWSActualizarTareas_ConCredenciales()
        {
            try
            {
                string token = WSObtenerToken();
                //Llamar Ws con token
                using (var cliente = new WebClient())
                {
                    var url = "http://localhost:52788/api/tareas/actualizar";
                    cliente.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                    cliente.Headers.Add("Authorization", "Bearer " + token);
                    string Descripcion = "Ejemplo descripción nueva";
                    DateTime FechaVencimiento = DateTime.Now;
                    int Estado = 1;
                    Int64 IdTarea = 5;
                    string resultado = cliente.UploadString(url, "POST", $"IdTarea={IdTarea}&descripcion={Descripcion}&estado={Estado}&FechaVencimiento={FechaVencimiento}");
                    var valores = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultadoWSActualizar>(resultado);
                    Assert.AreNotEqual(valores, null);
                    Assert.IsTrue(valores.IdTarea > 0);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        [TestMethod]
        public void TestWSBorrarTareas_ConCredenciales()
        {
            try
            {
                string token = WSObtenerToken();
                //Llamar Ws con token
                using (var cliente = new WebClient())
                {
                    var url = "http://localhost:52788/api/tareas/borrar";
                    cliente.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                    cliente.Headers.Add("Authorization", "Bearer " + token);
                    Int64 IdTarea = 6;
                    string resultado = cliente.UploadString(url, "POST", $"IdTarea={IdTarea}");
                    var valores = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultadoWSBorrar>(resultado);
                    Assert.AreEqual(valores, null);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
