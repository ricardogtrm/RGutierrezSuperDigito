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
            List<string> proceso = new List<string>();
            Console.WriteLine("Ingrese un numero: ");
            int digito = int.Parse(Console.ReadLine());
            Console.WriteLine("El numero es: " + digito);
            Console.WriteLine("El resultado es: " + Calcular(digito, proceso));
            Console.WriteLine("\n ------------- Proceso ------------- \n");
            foreach (var item in proceso)
            {
                Console.WriteLine(item);
            }

            Console.ReadKey();
        }

        public static int Calcular(int digito, List<string> proceso)
        {
            proceso.Add("SuperDigito(" + digito + ")");
            if (digito <= 9)
            {
                return digito;
            }
            else
            {
                int superDigito = CalcularDetalle(digito, proceso);
                return superDigito;
            }

        }

        public static int CalcularDetalle(int digito, List<string> proceso)
        {
            string valor = digito.ToString();
            char[] numeros = valor.ToCharArray();
            int superDigito = 0;
            string itemToList = "SuperDigito(";
            int cont = 0;
            foreach (char num in numeros)
            {
                cont++;
                int number = int.Parse(num.ToString());
                superDigito = superDigito + number;
                if (cont == 1)
                {
                    itemToList = itemToList + number + "+";
                }
                else if (cont < numeros.Length)
                {
                    itemToList = itemToList + number + "+";
                }
                else
                {
                    itemToList = itemToList + number;
                }
            }
            proceso.Add(itemToList + ") = "+superDigito);
            if (superDigito <= 9)
            {
                proceso.Add("SuperDigito(" + superDigito + ")");
                return superDigito;
            }
            else
            {
                return CalcularDetalle(superDigito, proceso);
            }
        }
    }
}
