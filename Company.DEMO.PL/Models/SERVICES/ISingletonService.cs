namespace Company.DEMO.PL.Models.SERVICES
{
    public interface ISingletonService
    {
        public Guid Guid { get; set; }
        string? GetGuid();
    }
}
