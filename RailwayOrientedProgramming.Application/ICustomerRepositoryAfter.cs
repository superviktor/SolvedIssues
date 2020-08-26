using RailwayOrientedProgramming.Common;
using RailwayOrientedProgramming.Domain;

namespace RailwayOrientedProgramming.Application
{
    public interface ICustomerRepositoryAfter
    {
        CustomerAfter? GetById(int id);
        Result Save(CustomerAfter customer);
    }
}
