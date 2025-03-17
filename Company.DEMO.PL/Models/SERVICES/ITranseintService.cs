namespace Company.DEMO.PL.Models.SERVICES
{
    public interface ITranseintService
    {
        public Guid Guid { get; set; }
        string? GetGuid();
    }
}
