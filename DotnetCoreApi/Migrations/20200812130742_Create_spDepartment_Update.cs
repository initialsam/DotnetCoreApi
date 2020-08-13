using Microsoft.EntityFrameworkCore.Migrations;

namespace DotnetCoreApi.Migrations
{
    public partial class Create_spDepartment_Update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
IF EXISTS(SELECT * FROM sysobjects WHERE name = 'Department_Update')
	DROP PROC Department_Update
GO

[dbo].[Department_Update]
    @DepartmentID [int],
    @Name [nvarchar](50),
    @Budget [money],
    @StartDate [datetime],
    @InstructorID [int],
    @RowVersion_Original [rowversion],
	@DateModified [datetime2]
AS
BEGIN
    UPDATE [dbo].[Department]
    SET [Name] = @Name, [Budget] = @Budget, [StartDate] = @StartDate, [InstructorID] = @InstructorID, [DateModified] = @DateModified
    WHERE (([DepartmentID] = @DepartmentID) AND (([RowVersion] = @RowVersion_Original) OR ([RowVersion] IS NULL AND @RowVersion_Original IS NULL)))

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
