using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Usuario
    {
        public static ML.Result GetByUserName(string userName)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RGutierrezSuperDigitoEntities context = new DL.RGutierrezSuperDigitoEntities())
                {
                    var query = context.UsuarioGetByUserName(userName).AsEnumerable().FirstOrDefault();
                    if (query != null)
                    {
                        ML.Usuario usuario = new ML.Usuario();
                        usuario.IdUsuario = query.IdUsuario;
                        usuario.UserName = query.UserName;
                        usuario.Password = query.Password;
                        usuario.Nombre = query.Nombre;
                        usuario.ApellidoPaterno = query.ApellidoPaterno;
                        usuario.ApellidoMaterno = query.ApellidoMaterno;

                        result.Object = usuario;
                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Message = ex.Message;
            }
            return result;
        }

        public static ML.Result Add(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RGutierrezSuperDigitoEntities context = new DL.RGutierrezSuperDigitoEntities())
                {
                    int query = context.UsuarioAdd(usuario.UserName, usuario.Password, usuario.Nombre,
                        usuario.ApellidoPaterno, usuario.ApellidoMaterno);
                    if (query > 0)
                    {
                        result.Correct = true;
                        result.Message = "Usuario registrado";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Message = ex.Message;
            }
            return result;
        }

    }
}
