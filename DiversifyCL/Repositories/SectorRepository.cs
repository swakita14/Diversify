using System.Linq;
using DiversifyCL.Data;
using DiversifyCL.Interfaces.Repositories;
using DiversifyCL.Models.Database;

namespace DiversifyCL.Repositories
{
    public class SectorRepository : ISectorRepository
    {
        private readonly DiversifyContext _context;
        public SectorRepository(DiversifyContext context)
        {
            _context = context;
        }

        /**
         * Find sector by the name, else return others 
         */
        public Sector GetSectorIdByName(string sectorName)
        {
            Sector sector = _context.Sectors.FirstOrDefault(x => x.SectorName == sectorName);

            if (sector is null)
            {
                sector = new Sector
                {
                    SectorName = "Others",
                    SectorId = 13
                };
            }

            return sector;
        }

        /**
         * Return Sector Name when given SectorId 
         */
        public string GetSectorNameById(int sectorId)
        {
            return _context.Sectors.Find(sectorId).SectorName;
        }
    }
}
