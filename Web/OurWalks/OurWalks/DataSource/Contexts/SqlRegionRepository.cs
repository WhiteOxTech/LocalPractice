using OurWalks.DataSource.Contracts;
using OurWalks.Model;

namespace OurWalks.DataSource.Contexts
{
    public class SqlRegionRepository : IRegionRepository
    {
        private readonly OutWalksDbContext _dbContext;
        public SqlRegionRepository(OutWalksDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public void DeleteRegion(Guid id)
        {
            var region = _dbContext.Regions.FindAsync(id);
            if (region != null)
            {
                _dbContext.Remove(region);
                _dbContext.SaveChanges();
            }
           // return region;
        }

        public List<RegionDTO> GetAllRegionsAsync()
        {
            return null;
        }

        public RegionDTO GetRegionById(Guid id)
        {
            throw new NotImplementedException();
        }

        public RegionDTO GetRegionByName(string name)
        {
            throw new NotImplementedException();
        }

        public void InsertRegionAsync(RegionDTO regionDTO)
        {
            throw new NotImplementedException();
        }
    }
}
