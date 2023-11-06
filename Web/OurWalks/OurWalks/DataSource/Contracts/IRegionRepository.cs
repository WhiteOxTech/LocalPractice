using OurWalks.Model;

namespace OurWalks.DataSource.Contracts
{
    public interface IRegionRepository
    {
        public List<RegionDTO> GetAllRegionsAsync();
        public RegionDTO GetRegionById(Guid id);
        public RegionDTO GetRegionByName(string name);
        public void InsertRegionAsync(RegionDTO regionDTO);
        public void DeleteRegion(Guid id);

    }
}
