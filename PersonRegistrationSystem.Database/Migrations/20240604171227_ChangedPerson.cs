using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonRegistrationSystem.Database.Migrations
{
    /// <inheritdoc />
    public partial class ChangedPerson : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonInformations_PlaceOfResidences_PlaceOfResidenceId",
                table: "PersonInformations");

            migrationBuilder.DropIndex(
                name: "IX_PersonInformations_PlaceOfResidenceId",
                table: "PersonInformations");

            migrationBuilder.DropColumn(
                name: "PlaceOfResidenceId",
                table: "PersonInformations");

            migrationBuilder.AddColumn<int>(
                name: "PersonInformationId",
                table: "PlaceOfResidences",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PlaceOfResidences_PersonInformationId",
                table: "PlaceOfResidences",
                column: "PersonInformationId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PlaceOfResidences_PersonInformations_PersonInformationId",
                table: "PlaceOfResidences",
                column: "PersonInformationId",
                principalTable: "PersonInformations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlaceOfResidences_PersonInformations_PersonInformationId",
                table: "PlaceOfResidences");

            migrationBuilder.DropIndex(
                name: "IX_PlaceOfResidences_PersonInformationId",
                table: "PlaceOfResidences");

            migrationBuilder.DropColumn(
                name: "PersonInformationId",
                table: "PlaceOfResidences");

            migrationBuilder.AddColumn<int>(
                name: "PlaceOfResidenceId",
                table: "PersonInformations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PersonInformations_PlaceOfResidenceId",
                table: "PersonInformations",
                column: "PlaceOfResidenceId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonInformations_PlaceOfResidences_PlaceOfResidenceId",
                table: "PersonInformations",
                column: "PlaceOfResidenceId",
                principalTable: "PlaceOfResidences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
