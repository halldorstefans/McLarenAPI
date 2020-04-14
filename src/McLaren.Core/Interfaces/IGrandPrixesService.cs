using System.Collections.Generic;
using System.Threading.Tasks;
using McLaren.Core.Models;
using McLaren.Core.ResourceParameters;

namespace McLaren.Core.Interfaces
{
    public interface IGrandPrixesService
    {
        Task<IEnumerable<GrandPrixDto>> GetGrandPrix(int raceId);
        Task<IEnumerable<GrandPrixDto>> GetGrandPrixes();
        Task<IEnumerable<GrandPrixDto>> GetGrandPrixes(GrandPrixesResourceParameters grandPrixesResourceParameters);
    }
}