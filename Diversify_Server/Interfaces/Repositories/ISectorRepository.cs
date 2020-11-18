using Diversify_Server.Models.Database;

namespace Diversify_Server.Interfaces.Repositories
{
    public interface ISectorRepository
    {
        /**
         * Find sector by the name, else return others 
         */
        Sector GetSectorIdByName(string sectorName);
    }
}