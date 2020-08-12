using Microsoft.EntityFrameworkCore.Migrations;

namespace DotnetCoreApi.Migrations
{
    public partial class Create_spDepartment_Delete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.Sql(@"
IF EXISTS(SELECT * FROM sysobjects WHERE name = 'spDepartment_Delete')
	DROP PROC spDepartment_Delete
GO

----------------------------------------------------------------------------
-- Delete a single record from Department
----------------------------------------------------------------------------
CREATE PROC spDepartment_Delete
	@DepartmentID int
AS

DELETE	Department
WHERE 	DepartmentID = @DepartmentID

GO


");
		}

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
