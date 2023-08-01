using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class UsuarioController : Controller
    {
        [HttpGet]
        public ActionResult LoginUsuario()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetByUserName(string userName, string password)
        {
            ML.Result result = BL.Usuario.GetByUserName(userName);
            if (result.Object != null)
            {
                ML.Usuario usuario = new ML.Usuario();
                usuario = ((ML.Usuario)result.Object);
                if (usuario.Password == password)
                {
                    return RedirectToAction("/SuperDigitoGetByIdUsuario", usuario);
                }
                else
                {
                    ViewBag.Titulo = "¡Error de credenciales!";
                    ViewBag.Message = "Contraseña incorrecta";
                    return View("Modal");
                }
            }
            else
            {
                ViewBag.Titulo = "¡Usuario no encontrado!";
                ViewBag.Message = "El usuario no existe, ingrese un usuario existente o registrese";
                return View("Modal");
            }
        }

        [HttpGet]
        public ActionResult Form()
        {
            ML.Usuario usuario = new ML.Usuario();
            ViewBag.Titulo = "Registrar nuevo usuario";
            ViewBag.Accion = "Registrar";
            return View(usuario);
        }

        [HttpPost]
        public ActionResult Form(ML.Usuario usuario)
        {
            ML.Result result = BL.Usuario.Add(usuario);
            if (result.Correct)
            {
                ViewBag.Titulo = "Registro Exitoso";
                ViewBag.Message = result.Message;
                return View("Modal");
            }
            else
            {
                ViewBag.Titulo = "No se pudo registrar";
                ViewBag.Message = result.Message;
                return View("Modal");
            }
        }

        [HttpGet]
        public ActionResult SuperDigitoGetByIdUsuario(ML.Usuario usuario)
        {
            ML.Result result = BL.SuperDigito.GetByIdUsuario(usuario.IdUsuario);
            ML.SuperDigito superDigito = new ML.SuperDigito();
            superDigito.Usuario = new ML.Usuario();
            superDigito.Usuario.IdUsuario = usuario.IdUsuario;
            if (result.Correct)
            {
                superDigito.SuperDigitos = new List<object>();
                superDigito.SuperDigitos = result.Objects;
                return View(superDigito);
            }
            else
            {
                ViewBag.Message = result.Message;
                return View();
            }
        }

        [HttpGet]
        public ActionResult Delete(int idSuperDigito)
        {
            ML.Result result = BL.SuperDigito.Delete(idSuperDigito);
            if (result.Correct)
            {
                ViewBag.Titulo = "Historial eliminado";
                ViewBag.Message = result.Message;
                ML.Usuario usuario = new ML.Usuario();
                usuario.IdUsuario = idSuperDigito;
                return View("ModalGlobal", usuario);
            }
            else
            {
                ViewBag.Titulo = "No se elimino el historial";
                ViewBag.Message = result.Message;
                ML.Usuario usuario = new ML.Usuario();
                usuario.IdUsuario = idSuperDigito;
                return View("ModalGlobal", usuario);
            }
        }

        [HttpPost]
        public ActionResult CalcularSuperDigito(ML.SuperDigito superDigito)
        {
            ML.Result resultConsulta = BL.SuperDigito.GetByNumero(superDigito.Digito);
            if (resultConsulta.Correct)
            {
                superDigito = ((ML.SuperDigito)resultConsulta.Object);
                ML.Result resultUpdate = BL.SuperDigito.Update(superDigito.IdSuperDigito);
                return RedirectToAction("SuperDigitoGetByIdUsuario", superDigito.Usuario);
            }
            else
            {
                ML.Result result = BL.SuperDigito.Add(superDigito);
                return RedirectToAction("SuperDigitoGetByIdUsuario", superDigito.Usuario);
            }
        }

        public JsonResult CalcularSuper(int digito)
        {
            ML.SuperDigito superDigito = new ML.SuperDigito();
            superDigito.Resultado = Calcular(digito);
            return Json(superDigito.Resultado, JsonRequestBehavior.AllowGet);

        }

        public static int Calcular(int digito)
        {
            if (digito <= 9)
            {
                return digito;
            }
            else
            {
                int superDigito = CalcularDetalle(digito);
                return superDigito;
            }

        }

        public static int CalcularDetalle(int digito)
        {
            string valor = digito.ToString();
            char[] numeros = valor.ToCharArray();
            int superDigito = 0;
            foreach (char num in numeros)
            {
                int number = int.Parse(num.ToString());
                superDigito = superDigito + number;
            }
            if (superDigito <= 9)
            {
                return superDigito;
            }
            else
            {
                return CalcularDetalle(superDigito);
            }
        }
    }
}