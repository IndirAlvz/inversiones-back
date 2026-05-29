

using investment_service.Models;
using investment_service.Repositories;
using investment_service.Dtos;

namespace investment_service.Services
{
    public class SecUsuarioService
    {
        private readonly ISecUsuarioRepository _repository;

        public SecUsuarioService(ISecUsuarioRepository repository)
        {
            _repository = repository;
        }

        public async Task<SecUsuarioDto?> ValidateLoginAsync(int nIdUsuario, string cContrasena)
        {
            var usuario = await _repository.GetByIdAsync(nIdUsuario);
            if (usuario == null)
                return null;
            if (!PasswordHasher.VerifyPassword(cContrasena, usuario.cContrasena))
                return null;
            return ToDto(usuario);
        }


        private SecUsuarioDto ToDto(SecUsuario usuario)
        {
            return new SecUsuarioDto
            {
                nIdUsuario = usuario.nIdUsuario,
                nIdPersona = usuario.nIdPersona,
                cContrasena = usuario.cContrasena,
                nEstado = usuario.nEstado,
                nIdPerfil = usuario.nIdPerfil,
                cAuditoria = usuario.cAuditoria
            };
        }


        private SecUsuario ToEntity(SecUsuarioDto dto)
        {
            return new SecUsuario
            {
                nIdUsuario = dto.nIdUsuario,
                nIdPersona = dto.nIdPersona,
                cContrasena = dto.cContrasena,
                nEstado = dto.nEstado,
                nIdPerfil = dto.nIdPerfil,
                cAuditoria = dto.cAuditoria
            };
        }

        public async Task<List<SecUsuarioDto>> GetAllAsync()
        {
            var usuarios = await _repository.GetAllAsync();
            return usuarios.Select(ToDto).ToList();
        }

        public async Task<SecUsuarioDto?> GetByIdAsync(int id)
        {
            var usuario = await _repository.GetByIdAsync(id);
            return usuario == null ? null : ToDto(usuario);
        }

        public async Task AddAsync(SecUsuarioDto dto)
        {
            // Hashear la contraseña antes de guardar
            dto.cContrasena = investment_service.Services.PasswordHasher.HashPassword(dto.cContrasena);
            var usuario = ToEntity(dto);
            await _repository.AddAsync(usuario);
            dto.nIdUsuario = usuario.nIdUsuario;
        }

        public async Task UpdateAsync(int id, SecUsuarioDto dto)
        {
            var usuario = await _repository.GetByIdAsync(id);
            if (usuario == null) return;
            usuario.nIdPersona = dto.nIdPersona;
            // Hashear la contraseña antes de actualizar
            usuario.cContrasena = investment_service.Services.PasswordHasher.HashPassword(dto.cContrasena);
            usuario.nEstado = dto.nEstado;
            usuario.nIdPerfil = dto.nIdPerfil;
            usuario.cAuditoria = dto.cAuditoria;
            await _repository.UpdateAsync(usuario);
        }

        public Task DeleteAsync(int id) => _repository.DeleteAsync(id);
    }
}