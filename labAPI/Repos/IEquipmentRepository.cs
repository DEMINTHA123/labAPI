using labAPI.Entities;

namespace labAPI.Repos
{
    public interface IEquipmentRepository
    {
        Task<Equipment> GetById(int id);
        Task<IEnumerable<Equipment>> GetAll();
        Task<bool> Add(Equipment equipment);
        bool Update(Equipment equipment);
        bool Delete(Equipment equipment);
        Photo AddPhoto(IFormFile formFile);
    }
}
