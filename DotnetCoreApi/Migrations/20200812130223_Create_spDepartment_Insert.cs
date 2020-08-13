using Microsoft.EntityFrameworkCore.Migrations;

namespace DotnetCoreApi.Migrations
{
    public partial class Create_spDepartment_Insert : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
IF EXISTS(SELECT * FROM sysobjects WHERE name = 'Department_Insert')
	DROP PROC Department_Insert
GO

 [dbo].[Department_Insert]
    @Name [nvarchar](50),
    @Budget [money],
    @StartDate [datetime],
    @InstructorID [int],
	@DateModified [datetime2]
AS
BEGIN
    INSERT [dbo].[Department]([Name], [Budget], [StartDate], [InstructorID], [DateModified])
    VALUES (@Name, @Budget, @StartDate, @InstructorID, @DateModified)

    DECLARE @DepartmentID int
    SELECT @DepartmentID = [DepartmentID]
    FROM [dbo].[Department]
    WHERE @@ROWCOUNT > 0 AND [DepartmentID] = scope_identity()

    SELECT t0.*
    FROM [dbo].[Department] AS t0
    WHERE @@ROWCOUNT > 0 AND t0.[DepartmentID] = @DepartmentID
END

GO
");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
