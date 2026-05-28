namespace investment_service.Repositories
{
    public interface ISecUsuarioRepository
    {
        Task<List<Models.SecUsuario>> GetAllAsync();
        Task<Models.SecUsuario?> GetByIdAsync(int id);
        Task AddAsync(Models.SecUsuario usuario);
        Task UpdateAsync(Models.SecUsuario usuario);
        Task DeleteAsync(int id);
    }
}