using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL_ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Ingrese un numero: ");
            int digito = int.Parse(Console.ReadLine());
            Console.WriteLine("El numero es: " + digito);
            Console.WriteLine("El resultado es: " + Calcular(digito));

            Console.ReadKey();
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
