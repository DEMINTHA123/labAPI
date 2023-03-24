using labAPI.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace labAPI.Repos
{
    public class EquipmentRepository : IEquipmentRepository
    {
        protected readonly LabDBContext _context;
        public EquipmentRepository(LabDBContext context)
        {
            _context = context;
        }
        public async Task Add(Equipment equipment)
        {
            try
            {
                _context.Equipment.Add(equipment);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool Delete(Equipment equipment)
        {
            try
            {
                _context.Equipment.Remove(equipment);
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public async Task<IEnumerable<Equipment>> GetAll()
        {
            try
            {
                return await _context.Equipment.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Equipment> GetById(int id)
        {
            try
            {
                return await _context.Equipment.FindAsync(id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Update(Equipment equipment)
        {
            try
            {
                _context.Equipment.Update(equipment);
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public Photo AddPhoto(IFormFile formFile)
        {
            try
            {
                var photo = new Photo()
                {
                    ImageTitle = formFile.Name,
                    Blob = ConvertToBlob(formFile)
                };
                return photo;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private string ConvertToBlob(IFormFile file)
        {
            if (file.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    return Convert.ToBase64String(fileBytes);
                }
            }
            else
            {
                return "";
            }
        }
    }
}
