using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OurWalks.DataSource;
using OurWalks.Model;

namespace OurWalks.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        private OutWalksDbContext _outWalksDbContext;
        public RegionController(OutWalksDbContext dbContext)
        {
            _outWalksDbContext = dbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var obj = await _outWalksDbContext.Regions.ToListAsync();

            List<RegionDTO> lstRegionOut = new List<RegionDTO>();

            foreach (var item in obj)
            {
                RegionDTO region = new RegionDTO();
                region.Name = item.Name;
                region.Code = item.Code;
                region.RegionImageURL = item.RegionImageURL;
                region.Id = item.Id;
                lstRegionOut.Add(region);

            }
            return Ok(lstRegionOut);
        }

        [HttpPost]
        public async Task<IActionResult> InsertRegion([FromBody] RegionDTO region)
        {
            Region values = new Region()
            {
                Name = region.Name,
                Code = region.Code,
                RegionImageURL = region.RegionImageURL
            };
            await _outWalksDbContext.Regions.AddAsync(values);
            _outWalksDbContext.SaveChanges();
            return CreatedAtAction(nameof(GetAll), values);
        }

        // Get region by Id
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            //Region region =  _outWalksDbContext.Regions.Select(x => x.Code ==  Regioncode).FirstOrDefault();
            var regionValue1 = _outWalksDbContext.Regions.FirstOrDefault(x => x.Code == "");
            var regionValue = _outWalksDbContext.Regions.Find(id); // Find method will accept only Primary key
            if (regionValue == null)
                return NotFound();
            else
                return Ok(regionValue);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            Region region = await _outWalksDbContext.Regions.Find(id);
            try
            {
                if (region != null)
                {
                    _outWalksDbContext.Regions.Remove(region);
                    _outWalksDbContext.SaveChanges();
                    return Ok(region);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
