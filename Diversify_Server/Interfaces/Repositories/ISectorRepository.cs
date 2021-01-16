using DiversifyCL.Models.Database;

namespace Diversify_Server.Interfaces.Repositories
{
    public interface ISectorRepository
    {
        /**
         * Find sector by the name, else return others 
         */
        Sector GetSectorIdByName(string sectorName);

        string GetSectorNameById(int sectorId);
    }
}