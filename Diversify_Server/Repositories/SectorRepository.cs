using System.Linq;
using Diversify_Server.Data;
using Diversify_Server.Models.Database;

namespace Diversify_Server.Repositories
{
    public class SectorRepository
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
            Sector sector = _context.Sector.First(x => x.SectorName.Equals(sectorName)) ?? new Sector
            {
                SectorId = 13,
                SectorName = "Others"
            };

            return sector;
        }
    }
}
