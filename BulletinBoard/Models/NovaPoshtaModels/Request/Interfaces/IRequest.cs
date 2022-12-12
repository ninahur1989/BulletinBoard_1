namespace BulletinBoard.Models.NovaPoshtaModels.Request.Interfaces
{
    public interface IRequest<T>
    {
        public string success { get; set; }
        public List<T>? data { get; set; }
    }
}
