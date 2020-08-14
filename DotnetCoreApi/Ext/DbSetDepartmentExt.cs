using DotnetCoreApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetCoreApi.Ext
{
    public static class DbSetDepartmentExt
    {
        public static async Task<Department> StoredProcedureUpdate(this DbSet<Department> dbSet, Department department)
        {
            var res = await dbSet.FromSqlInterpolated(
                  $"EXEC Department_Update {department.DepartmentId},{ department.Name}, { department.Budget}, {department.StartDate}, {department.InstructorId}, {department.RowVersion}, {department.DateModified}, {department.IsDeleted}")
                   .ToListAsync();
            var updatedDepartment = res.SingleOrDefault();
            return updatedDepartment;
        }
    }
}
