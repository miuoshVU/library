using Library.API.Models;
using Library.CodeFirstDatabase.Entities;

namespace Library.API.Interface;

public interface ISpotService
{
    public Task<Spot> CreateSpot(SpotDto spotDto);
    public Task<List<SpotDto>> ReadAllSpots();
    public Task<bool> UpdateSpot(int id, SpotDto spotDto);
    public Task<bool> DeleteSpot(int id);
    public Task<Spot> FindSpotByQr(string spotQrCode);
}