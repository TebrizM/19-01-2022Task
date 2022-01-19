using Microsoft.EntityFrameworkCore.Migrations;

namespace PustokTask.Migrations
{
    public partial class BookImageTableEdited : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookImages_Books_BookId1",
                table: "BookImages");

            migrationBuilder.DropIndex(
                name: "IX_BookImages_BookId1",
                table: "BookImages");

            migrationBuilder.DropColumn(
                name: "BookId1",
                table: "BookImages");

            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "BookImages",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BookId",
                table: "BookImages",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookImages_BookId",
                table: "BookImages",
                column: "BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookImages_Books_BookId",
                table: "BookImages",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookImages_Books_BookId",
                table: "BookImages");

            migrationBuilder.DropIndex(
                name: "IX_BookImages_BookId",
                table: "BookImages");

            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "BookImages",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BookId",
                table: "BookImages",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "BookId1",
                table: "BookImages",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookImages_BookId1",
                table: "BookImages",
                column: "BookId1");

            migrationBuilder.AddForeignKey(
                name: "FK_BookImages_Books_BookId1",
                table: "BookImages",
                column: "BookId1",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
