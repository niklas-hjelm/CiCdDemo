namespace DataAccess.Repositories.Interfaces;

public interface IRepository<TDto>
{
    Task Add(TDto dto);
    Task Delete(string id);
    Task<TDto> Get(string id);
    Task<TDto> Update(TDto dto, string id);
    Task<IEnumerable<TDto>> GetAll();
}