namespace OurWalks.Model
{
    public class Walk
    {
        public Guid Id { get; set; }
        public string Name { get; set; }    
        public string Description { get; set; }
        public string? WalkImageURL { get; set; }   
        public Guid DifficulityId { get; set; }
        public Guid RegionId { get; set; }

        public Difficulty Difficulty { get; set; }
        public Region   Region { get; set; }
    }
}
