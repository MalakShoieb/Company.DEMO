
namespace Company.DEMO.PL.Models.SERVICES
{
    public class ScopedService : IScopedService
    {
        public ScopedService()
        {
            Guid = Guid.NewGuid();
        }
        public Guid Guid { get ; set ; }


        string? IScopedService.GetGuid()
        {
            return Guid.ToString();
        }
    }
}
