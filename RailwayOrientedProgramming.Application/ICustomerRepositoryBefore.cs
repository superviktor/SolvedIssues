using RailwayOrientedProgramming.Domain;

namespace RailwayOrientedProgramming.Application
{
    public interface ICustomerRepositoryBefore
    {
        CustomerBefore GetById(int id);
        void Save(CustomerBefore customerBefore);
    }
}
