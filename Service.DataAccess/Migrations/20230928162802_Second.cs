using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Service.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CreatedByUserId",
                table: "Prescriptions",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "FirstModifiedByUserId",
                table: "Prescriptions",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LastModifiedByUserId",
                table: "Prescriptions",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CreatedByUserId",
                table: "Patients",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "FirstModifiedByUserId",
                table: "Patients",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LastModifiedByUserId",
                table: "Patients",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CreatedByUserId",
                table: "Doctors",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "FirstModifiedByUserId",
                table: "Doctors",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LastModifiedByUserId",
                table: "Doctors",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CreatedByUserId",
                table: "Bills",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "FirstModifiedByUserId",
                table: "Bills",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LastModifiedByUserId",
                table: "Bills",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CreatedByUserId",
                table: "Appointments",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "FirstModifiedByUserId",
                table: "Appointments",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LastModifiedByUserId",
                table: "Appointments",
                type: "bigint",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Prescriptions");

            migrationBuilder.DropColumn(
                name: "FirstModifiedByUserId",
                table: "Prescriptions");

            migrationBuilder.DropColumn(
                name: "LastModifiedByUserId",
                table: "Prescriptions");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "FirstModifiedByUserId",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "LastModifiedByUserId",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "FirstModifiedByUserId",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "LastModifiedByUserId",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "FirstModifiedByUserId",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "LastModifiedByUserId",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "FirstModifiedByUserId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "LastModifiedByUserId",
                table: "Appointments");
        }
    }
}
