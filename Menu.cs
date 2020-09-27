using System;
using System.Collections.Generic;

namespace PR1___Secure_User_Database
{
    class Menu
    {
        List<int> OpcionesMenu = new List<int>(new int[] { 1, 2, 9});
        private const int opcionSalir = 9;
        private List<Usuario> usuariosValidos = new List<Usuario>();
        private List<Usuario> usuariosNoValidos = new List<Usuario>();
        private List<string> clavesOrdenadas = new List<string>();

        private void MostrarOpciones()
        {
            System.Console.WriteLine("1) Registrar usuario");
            System.Console.WriteLine("2) Consultar resumen de usuarios");
            System.Console.WriteLine();
            System.Console.WriteLine("9) Salir");
        }

        private int OpcionIngresada(List<int> opcionesValidas)
        {
            int eleccionComoInt = 0;
            bool laOpcionEsValida = false;

            //Mientras no haya una respuesta válida
            while (!laOpcionEsValida)
            {
                System.Console.WriteLine("Selecciona una opción:");
                string userInput = System.Console.ReadLine();


                try
                {
                    eleccionComoInt = Convert.ToInt32(userInput);
                    laOpcionEsValida = opcionesValidas.Contains(eleccionComoInt);
                }
                catch (System.Exception)
                {
                    laOpcionEsValida = false;
                }


                if (!laOpcionEsValida)
                {
                    System.Console.WriteLine("La opción seleccionada no es válida.");
                }
            }

            return eleccionComoInt;
        }
        public string ComprobarId()
        {
            bool laOpcionEsValida = false;
            int opcionEnInt = 0;
            string numIdIngresado = "";
            while (!laOpcionEsValida)
            {
                System.Console.WriteLine("Escribe el número identificador");
                string seleccion = System.Console.ReadLine();
                
                try
                {
                    opcionEnInt = Convert.ToInt32(seleccion);
                    numIdIngresado = seleccion;
                    laOpcionEsValida = true;
                }
                catch (System.Exception)
                {
                 laOpcionEsValida = false;
                }

                if (!laOpcionEsValida)
                {
                    System.Console.WriteLine("Datos no validos, inténtalo de nuevo");
                }
            }
    
            return numIdIngresado;
        }

        public int[] ComprobarClave()
        {
            bool laOpcionEsValida = false;
            int[] claveIngresada = new int[] {};
            int[] claveOrdenada = new int[] {};
            string claveOrdenadaString = "";
            while (!laOpcionEsValida)
            {
                System.Console.WriteLine("Escribe la clave numérica separando cada numero con una coma (ejemplo: 1,2,3,4)");
                string seleccion = System.Console.ReadLine();

                 try
                {
                    string[] seleccionArrayString = seleccion.Split(",");
                    claveIngresada = Array.ConvertAll(seleccionArrayString, s => int.Parse(s));
                    claveOrdenada = Array.ConvertAll(seleccionArrayString, s => int.Parse(s));
                    laOpcionEsValida = true;
                }
                catch (System.Exception)
                {
                    laOpcionEsValida = false;
                }

                if (!laOpcionEsValida)
                {
                    System.Console.WriteLine("Datos no validos, inténtalo de nuevo");
                }
            }

            for (int i = 1; i < claveOrdenada.Length; i++)
                {
                    int x = claveOrdenada[i];
                    int j = i - 1;
               
                    while (j >= 0 && claveOrdenada[j] > x)
                    {
                        claveOrdenada[j + 1] = claveOrdenada[j];
                        j = j - 1;
                    }

                    claveOrdenada[j + 1] = x;
                }
                claveOrdenadaString =  string.Join("", claveOrdenada);
                

                clavesOrdenadas.Add(claveOrdenadaString);


            return claveIngresada;
        }

        public bool IdValido(bool valido, string opcion)
        {

            int mitad = opcion.Length/2;
            
            // Comprobar si la primera mitas son 0s
            if ((opcion.Length % 2) != 0)
            {
                valido = false;
            }
            else
            {
                for (int i = 0; i < mitad; i++)
                {
                    string cero = "0";
                    if(opcion.Substring(i,1) != cero)
                    {
                        valido = false;
                    }  
                }
            }

            // Comprobar si la segunda mitad no son 0s

            for (int i = mitad; i < opcion.Length; i++)
            {
                string cero = "0";
                if(opcion.Substring(i,1) == cero)
                {
                    valido = false;
                }  
            }


            return valido;

        }

        public bool ClaveValida(bool valido, int[] arr)
        {
            if(arr.Length < 5)
            {
                valido = false;
            }
            else
            {
                for (int i = 0; i < arr.Length-1; i++)
                {
                    if(arr[i] == arr[i+1])
                    {
                        valido = false;
                    }

                }   
            }


            if(clavesOrdenadas.Count > 1)
            {
                for (int i = 0; i < clavesOrdenadas.Count-1; i++)
                {
                    if(clavesOrdenadas[clavesOrdenadas.Count-1] == clavesOrdenadas[i])
                    {
                        valido = false;
                    }
                }
            }
            return valido;
        }

        public void RegistrarUsuario()
        {
            bool valido = true;
            string validez = "";

            System.Console.WriteLine("Escribe el nombre a ingresar");
            string nombreIngresado = Convert.ToString(Console.ReadLine());

            string numIdIngresado = ComprobarId();
            valido = IdValido(valido, numIdIngresado);
            
            int[] claveIngresada = ComprobarClave();
            valido = ClaveValida(valido, claveIngresada);
            
        
                if (valido == true)
                {
                    validez = "Autorizado";
                    usuariosValidos.Add(new Usuario(nombreIngresado,numIdIngresado,claveIngresada, validez));
                }
                else
                {
                    validez = "Rechazado";
                    usuariosNoValidos.Add(new Usuario(nombreIngresado,numIdIngresado,claveIngresada, validez));
                }
            
        }

        static void ImprimirArray(int[] nums)
        {
            for (int i = 0; i < nums.Length; i++)
            {
                 if (i == nums.Length-1)
                {
                 System.Console.Write($" {nums[i]}");
                }
                else
                {
                 System.Console.Write($" {nums[i]},");
                }
            }
        }


        public void ConsultarResumen()
        {
            if (usuariosValidos.Count <= 0 && usuariosNoValidos.Count <= 0)
            {
                System.Console.WriteLine("\nNo hay ningún usuario registrado\n");
            }
            else
            {
                System.Console.WriteLine("Usuarios registrados:");


                for (int i = usuariosValidos.Count - 1; i >= 0; i--)
                {   
                    System.Console.Write($"\n{usuariosValidos[i].TomarNombre()} - {usuariosValidos[i].TomarNumeroId()} - ");
                    ImprimirArray(usuariosValidos[i].TomarClaveNum());
                    System.Console.Write($" - {usuariosValidos[i].TomarValidez()}\n");
                }

                for (int i = usuariosNoValidos.Count - 1; i >= 0; i--)
                {

                    System.Console.Write($"\n{usuariosNoValidos[i].TomarNombre()} - {usuariosNoValidos[i].TomarNumeroId()} - ");
                    ImprimirArray(usuariosNoValidos[i].TomarClaveNum()); 
                    System.Console.Write($" - {usuariosNoValidos[i].TomarValidez()}\n");
                }
            }
        }


        public void Iniciar()
        {
            int opcionSeleccionada = 0;

            while (opcionSeleccionada != opcionSalir)
            {
                MostrarOpciones(); 
                opcionSeleccionada = OpcionIngresada(OpcionesMenu);

                switch (opcionSeleccionada)
                {
                    case 1:
                        RegistrarUsuario();
                        break;

                    case 2:
                        ConsultarResumen();
                        break;
                }
            }
            System.Console.WriteLine("Adiós");
        }
    }
}