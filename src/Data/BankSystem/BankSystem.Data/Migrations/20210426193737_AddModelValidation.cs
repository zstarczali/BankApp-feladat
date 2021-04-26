﻿namespace BankSystem.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddModelValidation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                "Source",
                "Transfers",
                maxLength: 34,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                "SenderName",
                "Transfers",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                "RecipientName",
                "Transfers",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                "Destination",
                "Transfers",
                maxLength: 34,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                "Description",
                "Transfers",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);


            migrationBuilder.AlterColumn<string>(
                "FullName",
                "AspNetUsers",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                "UniqueId",
                "Accounts",
                maxLength: 34,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                "Name",
                "Accounts",
                maxLength: 35,
                nullable: false,
                oldClrType: typeof(string));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                "Source",
                "Transfers",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 34);

            migrationBuilder.AlterColumn<string>(
                "SenderName",
                "Transfers",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                "RecipientName",
                "Transfers",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                "Destination",
                "Transfers",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 34);

            migrationBuilder.AlterColumn<string>(
                "Description",
                "Transfers",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                "FullName",
                "AspNetUsers",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                "UniqueId",
                "Accounts",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 34);

            migrationBuilder.AlterColumn<string>(
                "Name",
                "Accounts",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 35);
        }
    }
}