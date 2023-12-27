namespace AcademiaFs.ProyectoInventario.WebApi._Common
{
    public static class MensajesCamposInvalidos
    {
        public static string CampoVacio(string propiedad)
        {
            return $"Advertencia! El campo {propiedad} es requerido.";
        }

        public static string IdNoEncontrado(string propiedad)
        {
            return $"Advertencia! No se encontro el {propiedad} ingresado.";
        }
    }
}
