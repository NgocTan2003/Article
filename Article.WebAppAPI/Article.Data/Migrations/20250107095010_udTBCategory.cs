using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Article.Data.Migrations
{
    public partial class udTBCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastDeleteBy",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastDeleteBy",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "LastDeleteBy",
                table: "ArticleTags");

            migrationBuilder.DropColumn(
                name: "LastDeleteBy",
                table: "ArticleSubCategories");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "ArticleSubCategories");

            migrationBuilder.DropColumn(
                name: "LastDeleteBy",
                table: "ArticleDatas");

            migrationBuilder.DropColumn(
                name: "LastDeleteBy",
                table: "ArticleCategories");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "ArticleCategories");

            migrationBuilder.RenameColumn(
                name: "LastUpdateBy",
                table: "AspNetUsers",
                newName: "UpdateBy");

            migrationBuilder.RenameColumn(
                name: "LastUpdateBy",
                table: "AspNetRoles",
                newName: "UpdateBy");

            migrationBuilder.RenameColumn(
                name: "LastUpdateBy",
                table: "ArticleTags",
                newName: "UpdateBy");

            migrationBuilder.RenameColumn(
                name: "LastUpdateBy",
                table: "ArticleSubCategories",
                newName: "UpdateBy");

            migrationBuilder.RenameColumn(
                name: "LastUpdateBy",
                table: "ArticleDatas",
                newName: "UpdateBy");

            migrationBuilder.RenameColumn(
                name: "LastUpdateBy",
                table: "ArticleCategories",
                newName: "UpdateBy");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateUpdated",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateUpdated",
                table: "AspNetRoles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateUpdated",
                table: "ArticleTags",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateUpdated",
                table: "ArticleSubCategories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "DisplayOrder",
                table: "ArticleSubCategories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateUpdated",
                table: "ArticleDatas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateUpdated",
                table: "ArticleCategories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "DisplayOrder",
                table: "ArticleCategories",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateUpdated",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DateUpdated",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "DateUpdated",
                table: "ArticleTags");

            migrationBuilder.DropColumn(
                name: "DateUpdated",
                table: "ArticleSubCategories");

            migrationBuilder.DropColumn(
                name: "DisplayOrder",
                table: "ArticleSubCategories");

            migrationBuilder.DropColumn(
                name: "DateUpdated",
                table: "ArticleDatas");

            migrationBuilder.DropColumn(
                name: "DateUpdated",
                table: "ArticleCategories");

            migrationBuilder.DropColumn(
                name: "DisplayOrder",
                table: "ArticleCategories");

            migrationBuilder.RenameColumn(
                name: "UpdateBy",
                table: "AspNetUsers",
                newName: "LastUpdateBy");

            migrationBuilder.RenameColumn(
                name: "UpdateBy",
                table: "AspNetRoles",
                newName: "LastUpdateBy");

            migrationBuilder.RenameColumn(
                name: "UpdateBy",
                table: "ArticleTags",
                newName: "LastUpdateBy");

            migrationBuilder.RenameColumn(
                name: "UpdateBy",
                table: "ArticleSubCategories",
                newName: "LastUpdateBy");

            migrationBuilder.RenameColumn(
                name: "UpdateBy",
                table: "ArticleDatas",
                newName: "LastUpdateBy");

            migrationBuilder.RenameColumn(
                name: "UpdateBy",
                table: "ArticleCategories",
                newName: "LastUpdateBy");

            migrationBuilder.AddColumn<string>(
                name: "LastDeleteBy",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastDeleteBy",
                table: "AspNetRoles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastDeleteBy",
                table: "ArticleTags",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastDeleteBy",
                table: "ArticleSubCategories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ParentId",
                table: "ArticleSubCategories",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastDeleteBy",
                table: "ArticleDatas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastDeleteBy",
                table: "ArticleCategories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ParentId",
                table: "ArticleCategories",
                type: "int",
                nullable: true);
        }
    }
}
