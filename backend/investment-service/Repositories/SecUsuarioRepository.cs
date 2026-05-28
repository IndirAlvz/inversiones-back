using investment_service.Models;
using Microsoft.EntityFrameworkCore;

namespace investment_service.Repositories
{
    public class SecUsuarioRepository : ISecUsuarioRepository
    {
        private readonly AppDbContext _context;

        public SecUsuarioRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<SecUsuario>> GetAllAsync()
        {
            return await _context.Set<SecUsuario>().ToListAsync();
        }

        public async Task<SecUsuario?> GetByIdAsync(int id)
        {
            return await _context.Set<SecUsuario>().FindAsync(id);
        }

        public async Task AddAsync(SecUsuario usuario)
        {
            _context.Set<SecUsuario>().Add(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(SecUsuario usuario)
        {
            _context.Set<SecUsuario>().Update(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var usuario = await GetByIdAsync(id);
            if (usuario != null)
            {
                _context.Set<SecUsuario>().Remove(usuario);
                await _context.SaveChangesAsync();
            }
        }
    }
}