using POS_ASP_ORA.Models;
using System.Collections.Generic;

namespace POS_ASP_ORA.Services.Interfaces
{
    public interface ISupplierService
    {
        // GET ALL
        List<Supplier> GetSuppliers();

        // INSERT
        string InsertSupplier(Supplier model);

        // UPDATE
        string UpdateSupplier(Supplier model);

        // DELETE
        string DeleteSupplier(int id);
    }
}