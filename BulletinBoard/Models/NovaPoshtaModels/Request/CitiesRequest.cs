using BulletinBoard.Models.NovaPoshtaModels.Request.Interfaces;

namespace BulletinBoard.Models.NovaPoshtaModels.Request
{
    public class CitiesRequest: IRequest<City>
    {
        public string success { get; set; }
        public List<City>? data { get; set; }
    }
}
