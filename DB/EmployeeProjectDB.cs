using Microsoft.EntityFrameworkCore;
using pom_api.Models;

namespace pom_api.Context;
class EmployeeProjectDB : DbContext
{
    public EmployeeProjectDB(DbContextOptions<EmployeeProjectDB> options)
    : base(options) { }

    public DbSet<EmployeeProjects> EmployeeProjects => Set<EmployeeProjects>();
}