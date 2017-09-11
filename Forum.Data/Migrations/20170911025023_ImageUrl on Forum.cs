using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace forum_app_demo.Data.Migrations
{
    public partial class ImageUrlonForum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Forums",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Forums");
        }
    }
}
