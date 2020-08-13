using Microsoft.EntityFrameworkCore.Migrations;

namespace DotnetCoreApi.Migrations
{
    public partial class Create_spDepartment_Delete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.Sql(@"
IF EXISTS(SELECT * FROM sysobjects WHERE name = 'Department_Delete')
	DROP PROC Department_Delete
GO

CREATE PROC [dbo].[Department_Delete]
    @DepartmentID [int],
    @RowVersion_Original [rowversion]
AS
BEGIN
    DELETE [dbo].[Department]
    WHERE (([DepartmentID] = @DepartmentID) AND (([RowVersion] = @RowVersion_Original) OR ([RowVersion] IS NULL AND @RowVersion_Original IS NULL)))
END


");
		}

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
