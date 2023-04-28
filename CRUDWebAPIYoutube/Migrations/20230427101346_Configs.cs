using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CRUDWebAPIYoutube.Migrations
{
    /// <inheritdoc />
    public partial class Configs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<long>(type: "bigint", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileSizeInBytes = table.Column<long>(type: "bigint", nullable: false),
                    FileExtension = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Details",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PanNumber = table.Column<long>(type: "bigint", nullable: false),
                    AadharNumber = table.Column<long>(type: "bigint", nullable: false),
                    ContactId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Details", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Details_Contacts_ContactId",
                        column: x => x.ContactId,
                        principalTable: "Contacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "Id", "Address", "Email", "FullName", "Phone" },
                values: new object[,]
                {
                    { new Guid("816f70bc-7f5a-4b7c-9ae0-0e3cc37db9a0"), "Old post office street", "ram@gmail.com", "Ram", 987238642942L },
                    { new Guid("88bfe5d0-a564-4f08-a9e2-69cb1d2b94d3"), "Rajahmundry", "chandha@gmail.com", "Chand", 987238642942L },
                    { new Guid("be001a26-3788-4b03-8320-704fadb48f3d"), "Nuzvid", "mohit@gmail.com", "Mohit", 9872228642942L }
                });

            migrationBuilder.InsertData(
                table: "Details",
                columns: new[] { "Id", "AadharNumber", "ContactId", "PanNumber" },
                values: new object[,]
                {
                    { new Guid("0d9e233a-fd21-4eef-a27e-054e84c0c7f1"), 345345345345L, new Guid("816f70bc-7f5a-4b7c-9ae0-0e3cc37db9a0"), 123123123123L },
                    { new Guid("1f087157-1c26-447f-af4d-06d1676824b9"), 345345345345L, new Guid("88bfe5d0-a564-4f08-a9e2-69cb1d2b94d3"), 567545464454L },
                    { new Guid("42f3c01d-a632-4cb4-9bb1-b50514683381"), 345345345345L, new Guid("be001a26-3788-4b03-8320-704fadb48f3d"), 567545464454L }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Details_ContactId",
                table: "Details",
                column: "ContactId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Details");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "Contacts");
        }
    }
}
