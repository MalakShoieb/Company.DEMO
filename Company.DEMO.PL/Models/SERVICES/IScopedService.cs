namespace Company.DEMO.PL.Models.SERVICES
{
    public interface IScopedService
    {
        public Guid Guid { get; set; }
        public string? GetGuid();
    }
}
