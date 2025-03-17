namespace Company.DEMO.PL.Models.SERVICES
{
    public class TranseintService:ITranseintService
    {
        public TranseintService()
        {
            Guid = Guid.NewGuid();
        }
        public Guid Guid { get; set; }


        string? ITranseintService.GetGuid()
        {
            return Guid.ToString();
        }
    }
}
    

