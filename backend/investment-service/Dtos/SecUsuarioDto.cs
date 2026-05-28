using Swashbuckle.AspNetCore.Annotations;

namespace investment_service.Dtos
{
    public class SecUsuarioDto
    {
        [SwaggerSchema(ReadOnly = true)]
        public int nIdUsuario { get; set; }
        public int? nIdPersona { get; set; }
        public string cContrasena { get; set; } = string.Empty;
        public int nEstado { get; set; }
        public int nIdPerfil { get; set; }
        public string? cAuditoria { get; set; }
    }
}