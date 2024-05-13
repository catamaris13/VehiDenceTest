using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VehiDenceAPI.Migrations
{
    /// <inheritdoc />
    public partial class Emailconstrain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                 name: "Email",
                 table: "Users",
                 type: "nvarchar(100)",
                 maxLength: 100,
                 nullable: false,
                 oldClrType: typeof(string),
                 oldType: "nvarchar(max)",
                 oldNullable: false);

            // Add a unique constraint to the Email column
            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
               name: "Adaugare_Unicitate",
               table: "Users");
            migrationBuilder.AlterColumn<string>(
               name: "Email",
               table: "Users",
               type: "nvarchar(max)",
               maxLength: 100,
               nullable: false,
               oldClrType: typeof(string),
               oldType: "nvarchar(100)");
        }
    }
}
