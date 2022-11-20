using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GUB.TracNghiThiBangLai.EntityFrameworkCore.Migrations
{
    public partial class UpdateComputer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Computers_Departments_DepartmentId",
                table: "Computers");

            migrationBuilder.DropForeignKey(
                name: "FK_Computers_Users_UserId",
                table: "Computers");

            migrationBuilder.DropIndex(
                name: "IX_Computers_UserId",
                table: "Computers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Computers");

            migrationBuilder.AlterColumn<int>(
                name: "DepartmentId",
                table: "Computers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "CCCD",
                table: "Computers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Computers_Departments_DepartmentId",
                table: "Computers",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Computers_Departments_DepartmentId",
                table: "Computers");

            migrationBuilder.DropColumn(
                name: "CCCD",
                table: "Computers");

            migrationBuilder.AlterColumn<int>(
                name: "DepartmentId",
                table: "Computers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Computers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Computers_UserId",
                table: "Computers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Computers_Departments_DepartmentId",
                table: "Computers",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Computers_Users_UserId",
                table: "Computers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
