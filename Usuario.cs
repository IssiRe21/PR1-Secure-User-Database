using System;

namespace PR1___Secure_User_Database
{
    class Usuario
    {
        string nombre { get; set; }
        string numeroId { get; set; }
        int[] claveNum { get; set; }
        string validez { get; set; }


        public Usuario(string nombre, string numeroId, int[] claveNum, string validez)
        {
            this.nombre = nombre;
            this.numeroId = numeroId;
            this.claveNum = claveNum;
            this.validez = validez;

        } 


         public string TomarNombre()
        {
            return nombre;
        }

        public string TomarNumeroId()
        {
            return numeroId;
        }

        public int[] TomarClaveNum()
        {
            return claveNum;
        }

        public string TomarValidez()
        {
            return validez;
        }

    }
}