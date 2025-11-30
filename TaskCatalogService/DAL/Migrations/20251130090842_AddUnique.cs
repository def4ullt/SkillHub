using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_technology_Name",
                table: "technology",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tag_Name",
                table: "tag",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_technology_Name",
                table: "technology");

            migrationBuilder.DropIndex(
                name: "IX_tag_Name",
                table: "tag");
        }
    }
}
