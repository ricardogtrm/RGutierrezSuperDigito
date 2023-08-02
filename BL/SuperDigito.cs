using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class SuperDigito
    {
        public static ML.Result GetByIdUsuario(int idUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RGutierrezSuperDigitoEntities context = new DL.RGutierrezSuperDigitoEntities())
                {
                    var query = context.SuperDigitoGetByIdUsuario(idUsuario).ToList();
                    if (query != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var item in query)
                        {
                            ML.SuperDigito superDigito = new ML.SuperDigito();
                            superDigito.IdSuperDigito = item.IdSuperDigito;
                            superDigito.Digito = item.Digito;
                            superDigito.Resultado = item.Resultado;
                            superDigito.FechaConsulta = item.FechaConsulta.Value.ToString();
                            superDigito.Usuario = new ML.Usuario();
                            superDigito.Usuario.IdUsuario = item.IdUsuario.Value;

                            result.Objects.Add(superDigito);
                        }
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

        public static ML.Result Delete(int idUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RGutierrezSuperDigitoEntities context = new DL.RGutierrezSuperDigitoEntities())
                {
                    int query = context.SuperDigitoDelete(idUsuario);
                    if (query > 0)
                    {
                        result.Correct = true;
                        result.Message = "Historial eliminado";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = true;
                result.Message = ex.Message;
            }
            return result;
        }

        public static ML.Result DeleteById(int idSuperDigito)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RGutierrezSuperDigitoEntities context = new DL.RGutierrezSuperDigitoEntities())
                {
                    int query = context.SuperDigitoDeleteById(idSuperDigito);
                    if (query > 0)
                    {
                        result.Correct = true;
                        result.Message = "Registro eliminado";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = true;
                result.Message = ex.Message;
            }
            return result;
        }

        public static ML.Result Add(ML.SuperDigito superDigito)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RGutierrezSuperDigitoEntities context = new DL.RGutierrezSuperDigitoEntities())
                {
                    int query = context.SuperDigitoAdd(superDigito.Digito, superDigito.Resultado, superDigito.Usuario.IdUsuario);
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Message = ex.Message;
            }
            return result;
        }

        public static ML.Result GetByNumero(int digito)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RGutierrezSuperDigitoEntities context = new DL.RGutierrezSuperDigitoEntities())
                {
                    var query = context.SuperDigitoGetByNumero(digito).AsEnumerable().FirstOrDefault();
                    if (query != null)
                    {
                        ML.SuperDigito superDigito = new ML.SuperDigito();
                        superDigito.IdSuperDigito = query.IdSuperDigito;
                        superDigito.Digito = query.Digito;
                        superDigito.Resultado = query.Resultado;
                        superDigito.FechaConsulta = query.FechaConsulta.Value.ToString();
                        superDigito.Usuario = new ML.Usuario();
                        superDigito.Usuario.IdUsuario = query.IdSuperDigito;

                        result.Object = superDigito;
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

        public static ML.Result Update(int idSuperDigito)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RGutierrezSuperDigitoEntities context = new DL.RGutierrezSuperDigitoEntities())
                {
                    int query = context.SuperDigitoUpdate(idSuperDigito);
                    if (query > 0)
                    {
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
    }
}
