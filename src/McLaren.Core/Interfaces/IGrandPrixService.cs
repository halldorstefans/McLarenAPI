using System.Collections.Generic;
using System.Threading.Tasks;
using McLaren.Core.Models;

namespace McLaren.Core.Interfaces
{
    public interface IGrandPrixService
    {
        Task<GrandPrixDto> GetById(int id);
        Task<IEnumerable<GrandPrixDto>> GetAll();
        Task<IEnumerable<GrandPrixDto>> GetByYear(int year);
    }
}