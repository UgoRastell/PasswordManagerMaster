﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetMaster.Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class removemasterpassword3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MasterPassword",
                table: "Users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MasterPassword",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
