using API.Models;
using System.Collections.Generic;
using System.Linq;

namespace API.Services
{
    public class ShipmentService
    {
        private readonly DataContext _context;

        public ShipmentService(DataContext context)
        {
            _context = context;
        }

        public List<Shipment> Get()
        {
            return _context.Shipments.ToList();
        }

        public Shipment Get(int id)
        {
            return _context.Shipments.Find(id);
        }

        public void Create(Shipment shipment)
        {
            _context.Shipments.Add(shipment);
            _context.SaveChanges();
        }

        public void Update(int id, Shipment shipment)
        {
            var existingShipment = _context.Shipments.Find(id);
            if (existingShipment != null)
            {
                existingShipment.OrderId = shipment.OrderId;
                existingShipment.ShipmentDate = shipment.ShipmentDate;
                existingShipment.DeliveryStatus = shipment.DeliveryStatus;
                _context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var shipment = _context.Shipments.Find(id);
            if (shipment != null)
            {
                _context.Shipments.Remove(shipment);
                _context.SaveChanges();
            }
        }
    }
}
