using investment_service.Dtos;
using investment_service.Models;
using investment_service.Repositories;
using Microsoft.AspNetCore.Mvc;


namespace investment_service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SecUsuariosController : ControllerBase
    {
        private readonly Services.SecUsuarioService _service;
        private readonly Services.JwtService _jwtService;

        public SecUsuariosController(Services.SecUsuarioService service, Services.JwtService jwtService)
        {
            _service = service;
            _jwtService = jwtService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<SecUsuarioDto>>>> GetAll()
        {
            var dtos = await _service.GetAllAsync();
            return Ok(new ApiResponse<IEnumerable<SecUsuarioDto>>
            {
                Success = true,
                Message = "Usuarios obtenidos correctamente",
                Data = dtos
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<SecUsuarioDto>>> GetById(int id)
        {
            var dto = await _service.GetByIdAsync(id);
            if (dto == null)
                return NotFound(new ApiResponse<SecUsuarioDto>
                {
                    Success = false,
                    Message = "Usuario no encontrado",
                    Data = null
                });
            return Ok(new ApiResponse<SecUsuarioDto>
            {
                Success = true,
                Message = "Usuario obtenido correctamente",
                Data = dto
            });
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<object>>> Create(SecUsuarioDto dto)
        {
            await _service.AddAsync(dto);
            var token = _jwtService.GenerateToken(dto);
            var response = new ApiResponse<object>
            {
                Success = true,
                Message = "Usuario creado correctamente",
                Data = new {
                    Usuario = dto,
                    Token = token
                }
            };
            return CreatedAtAction(nameof(GetById), new { id = dto.nIdUsuario }, response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<object>>> Update(int id, SecUsuarioDto dto)
        {
            var usuario = await _service.GetByIdAsync(id);
            if (usuario == null)
                return NotFound(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Usuario no encontrado",
                    Data = null
                });
            await _service.UpdateAsync(id, dto);
            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Usuario actualizado correctamente",
                Data = null
            });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<object>>> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Usuario eliminado correctamente",
                Data = null
            });
        }
    // ...
    }
}