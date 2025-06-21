using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class updatedRecipeAndDTOsAGAIN : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipeTags_Recipes_RecipeId",
                table: "RecipeTags");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipeTags_Tags_TagId",
                table: "RecipeTags");

            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Recipes_RecipeId",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_RecipeId",
                table: "Tags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RecipeTags",
                table: "RecipeTags");

            migrationBuilder.DropIndex(
                name: "IX_RecipeTags_RecipeId",
                table: "RecipeTags");

            migrationBuilder.DropColumn(
                name: "RecipeId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "RecipeTags");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "RecipeTags");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "RecipeTags");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "RecipeTags");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "RecipeTags");

            migrationBuilder.DropColumn(
                name: "UpdatedOn",
                table: "RecipeTags");

            migrationBuilder.RenameColumn(
                name: "TagId",
                table: "RecipeTags",
                newName: "TagsId");

            migrationBuilder.RenameColumn(
                name: "RecipeId",
                table: "RecipeTags",
                newName: "RecipesId");

            migrationBuilder.RenameIndex(
                name: "IX_RecipeTags_TagId",
                table: "RecipeTags",
                newName: "IX_RecipeTags_TagsId");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecipeTags",
                table: "RecipeTags",
                columns: new[] { "RecipesId", "TagsId" });

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeTags_Recipes_RecipesId",
                table: "RecipeTags",
                column: "RecipesId",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeTags_Tags_TagsId",
                table: "RecipeTags",
                column: "TagsId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipeTags_Recipes_RecipesId",
                table: "RecipeTags");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipeTags_Tags_TagsId",
                table: "RecipeTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RecipeTags",
                table: "RecipeTags");

            migrationBuilder.RenameColumn(
                name: "TagsId",
                table: "RecipeTags",
                newName: "TagId");

            migrationBuilder.RenameColumn(
                name: "RecipesId",
                table: "RecipeTags",
                newName: "RecipeId");

            migrationBuilder.RenameIndex(
                name: "IX_RecipeTags_TagsId",
                table: "RecipeTags",
                newName: "IX_RecipeTags_TagId");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RecipeId",
                table: "Tags",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "RecipeTags",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "RecipeTags",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "RecipeTags",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "RecipeTags",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "RecipeTags",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOn",
                table: "RecipeTags",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecipeTags",
                table: "RecipeTags",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_RecipeId",
                table: "Tags",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeTags_RecipeId",
                table: "RecipeTags",
                column: "RecipeId");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeTags_Recipes_RecipeId",
                table: "RecipeTags",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeTags_Tags_TagId",
                table: "RecipeTags",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Recipes_RecipeId",
                table: "Tags",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "Id");
        }
    }
}
