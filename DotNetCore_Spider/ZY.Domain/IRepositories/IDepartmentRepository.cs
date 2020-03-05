using System;
using ZY.Domain.Entities;

namespace ZY.Domain.IRepositories
{
    public interface IDepartmentRepository : IRepository<Department, Guid>
    {
    }
}
