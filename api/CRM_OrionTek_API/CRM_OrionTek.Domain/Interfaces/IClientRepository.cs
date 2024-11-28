namespace CRM_OrionTek.Domain.Interfaces.ICommonRepository
{
    public class Dto_pagination
    {
        public object? Data { get; set; }
        public int QuantityRecords { get; set; }
    }

    public interface ITRepository<TEntity,TEntityInput,TEntityOutput>
    {
        Task<TEntity> Create(TEntityInput entity);
        Task<TEntity> Update(TEntityInput entity, int id);
        Task<Dto_pagination> GetAllPaginated(int page = 1, int size = 0, string? SearchData = "");
        Task<TEntity> GetOne(int id);
        Task<TEntity> Delete(int id);
        Task<List<TEntity>> GetByname(string name);
        Task<List<TEntity>> GetAll();
    }
}
