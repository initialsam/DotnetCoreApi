using Microsoft.EntityFrameworkCore.Migrations;

namespace DotnetCoreApi.Migrations
{
    public partial class Create_spDepartment_Update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
IF EXISTS(SELECT * FROM sysobjects WHERE name = 'spDepartment_Update')
	DROP PROC spDepartment_Update
GO

----------------------------------------------------------------------------
-- Update a single record in Department
----------------------------------------------------------------------------
CREATE PROC spDepartment_Update
	@DepartmentID int,
	@Name nvarchar(100) = NULL,
	@Budget money,
	@StartDate datetime,
	@InstructorID int = NULL,
	@DateModified datetime2
AS

UPDATE	Department
SET	Name = @Name,
	Budget = @Budget,
	StartDate = @StartDate,
	InstructorID = @InstructorID,
	DateModified = COALESCE(@DateModified, '0001-01-01T00:00:00.0000000')
WHERE 	DepartmentID = @DepartmentID

GO

");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
