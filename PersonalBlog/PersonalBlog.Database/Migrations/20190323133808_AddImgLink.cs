using Microsoft.EntityFrameworkCore.Migrations;

namespace PersonalBlog.Database.Migrations
{
    public partial class AddImgLink : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImgLink",
                table: "Users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImgLink",
                table: "Users");
        }
    }
}
