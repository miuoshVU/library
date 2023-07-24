using AutoMapper;
using Library.API.Controllers;
using Library.API.Interface;
using Library.API.Models;
using Library.CodeFirstDatabase.Entities;
using Library.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.API.Service
{
    public class SpotService : ISpotService
    {
        private readonly LibraryDbContext _dbContext;
        private readonly IMapper _mapper;

        public SpotService(LibraryDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        //Create Spot
        public async Task<Spot> CreateSpot(SpotDto spotDto)
        {
            var spot = new Spot();
            spot = _mapper.Map<Spot>(spotDto);

            await _dbContext.Spots.AddAsync(spot);
            await _dbContext.SaveChangesAsync();

            return spot;
        }

        //Read Spot
        public async Task<List<SpotDto>> ReadAllSpots()
        {
            var spots = await _dbContext
                .Spots
                .Include(s => s.BookInstances)
                .ToListAsync();

            if (spots is null)
                spots = new List<Spot>();

            var SpotsDto = new List<SpotDto>(); // = _mapper.Map<List<SpotDto>>(spots);
            foreach(var spot in spots)
            {
                var s = _mapper.Map<SpotDto>(spot);
                s.bookCount = spot.BookInstances.Count();
                SpotsDto.Add(s);
            }
            return SpotsDto;
        }

        //Update Spot
        public async Task<bool> UpdateSpot(int id, SpotDto spotDto)
        {
            var spot = await _dbContext
                .Spots
                .FirstOrDefaultAsync(s => s.Id == id);

            if (spot is null)
            {
                return false;
            }

            spot.Name = spotDto.Name;
            spot.Building = spotDto.Building;
            spot.Floor = spotDto.Floor;
            spot.Description = spotDto.Description;
            spot.QR = spotDto.Qr;

            await _dbContext.SaveChangesAsync();
            return true;
        }

        //Delete Spot
        public async Task<bool> DeleteSpot(int id)
        {
            var spot = await _dbContext
                .Spots
                .FirstOrDefaultAsync(s => s.Id == id);
            if (spot is null)
                return false;
            _dbContext.Spots.Remove(spot);
           await _dbContext.SaveChangesAsync();
            return true;
        }

        //Search Spot By Qr
        public async Task<Spot> FindSpotByQr(string spotQrCode)
        {
            var searchspot = await _dbContext
                .Spots
                .Where(s => s.QR.Contains(spotQrCode))
                .FirstOrDefaultAsync();
            
            return searchspot;
        }
    }
}
