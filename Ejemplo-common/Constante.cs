namespace prueba.ejemplo.common
{
    public static class Constante
    {
        public const string Desarrollo = "Development";
        public const string Produccion = "Production";

        public const string EX_ESPECIALIDADES_NOT_FOUND = "No se creo la especialidad...";
        public const string EX_ESPECIALIDADES_NO_EXISTE = "No se encontro la información del registro por el código de especialidad";

        public enum ListadoEstados {
            Activo = 1,
            Desactivado = 0
        }
    }
}
