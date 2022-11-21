using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GUB.TracNghiThiBangLai.EntityFrameworkCore.Migrations
{
    public partial class updateappsettingentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "NameKey",
                table: "AppSettingEntities",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "valueKey",
                table: "AppSettingEntities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "valueKey",
                table: "AppSettingEntities");

            migrationBuilder.AlterColumn<string>(
                name: "NameKey",
                table: "AppSettingEntities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
