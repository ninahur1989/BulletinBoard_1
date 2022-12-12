using BulletinBoard.Models.NovaPoshtaModels.Request.Interfaces;

namespace BulletinBoard.Models.NovaPoshtaModels.Request
{
    public class WarehousesRequest : IRequest<Warehouse>
    {
        public string success { get; set; }
        public List<Warehouse>? data { get; set; }
    }
}
