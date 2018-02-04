using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CI2.Web.Controllers;
using CI2.Persistencia;

namespace CI2.PruebasUnitarias
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void pruebaUnitariaCrearTarea()
        {
            TareasController tareasController = new TareasController();
            TabTareaUsuario tareaUsuario = new TabTareaUsuario();
            //var ejemplo = tareasController.PostTabTareaUsuario(tareaUsuario);
        }
    }
}
