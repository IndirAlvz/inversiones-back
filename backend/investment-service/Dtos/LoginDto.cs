namespace investment_service.Dtos
{
    public class LoginDto
    {
        public int nIdUsuario { get; set; }
        public string cContrasena { get; set; } = string.Empty;
    }
}