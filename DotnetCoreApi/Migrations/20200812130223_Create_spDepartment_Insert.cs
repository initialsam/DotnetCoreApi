using Microsoft.EntityFrameworkCore.Migrations;

namespace DotnetCoreApi.Migrations
{
    public partial class Create_spDepartment_Insert : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
IF EXISTS(SELECT * FROM sysobjects WHERE name = 'spDepartment_Insert')
	DROP PROC spDepartment_Insert
GO

----------------------------------------------------------------------------
-- Insert a single record into Department
----------------------------------------------------------------------------
CREATE PROC spDepartment_Insert
	@Name nvarchar(100) = NULL,
	@Budget money,
	@StartDate datetime,
	@InstructorID int = NULL,
	@RowVersion timestamp = NULL,
	@DateModified datetime2 = NULL
AS

INSERT Department(Name, Budget, StartDate, InstructorID, RowVersion, DateModified)
VALUES (@Name, @Budget, @StartDate, @InstructorID, NULL, COALESCE(@DateModified, '0001-01-01T00:00:00.0000000'))

RETURN SCOPE_IDENTITY()

GO
");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
