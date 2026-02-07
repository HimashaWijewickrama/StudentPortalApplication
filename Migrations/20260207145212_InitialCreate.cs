using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StudentPortalApplication.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Faculties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faculties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StudentTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FacultyId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Departments_Faculties_FacultyId",
                        column: x => x.FacultyId,
                        principalTable: "Faculties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FacultyId = table.Column<int>(type: "int", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StudentTypeId = table.Column<int>(type: "int", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Students_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Students_Faculties_FacultyId",
                        column: x => x.FacultyId,
                        principalTable: "Faculties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Students_StudentTypes_StudentTypeId",
                        column: x => x.StudentTypeId,
                        principalTable: "StudentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Faculties",
                columns: new[] { "Id", "IsActive", "Name" },
                values: new object[,]
                {
                    { 1, false, "IT" },
                    { 2, false, "Business" },
                    { 3, false, "Engineering" }
                });

            migrationBuilder.InsertData(
                table: "StudentTypes",
                columns: new[] { "Id", "IsActive", "Name" },
                values: new object[,]
                {
                    { 1, false, "Foreign" },
                    { 2, false, "Local" }
                });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "FacultyId", "IsActive", "Name" },
                values: new object[,]
                {
                    { 1, 1, false, "Sofware Engineering" },
                    { 2, 1, false, "Cyber Security" },
                    { 3, 1, false, "Data Science" },
                    { 4, 2, false, "Accounting" },
                    { 5, 2, false, "Human Resource Management" },
                    { 6, 2, false, "Finance" },
                    { 7, 3, false, "Civil" },
                    { 8, 3, false, "Mechanical" },
                    { 9, 3, false, "Chemical" }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "DateOfBirth", "DepartmentId", "Email", "FacultyId", "FullName", "Gender", "RegistrationDate", "StudentTypeId" },
                values: new object[,]
                {
                    { 1, new DateTime(1998, 2, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "himasha@email.com", 1, "Himasha Wijewickrama", "Female", new DateTime(2020, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 2, new DateTime(1992, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "maheesha@gmail.com", 1, "Maheesha Wijewickrama", "Female", new DateTime(2024, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 3, new DateTime(1999, 6, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "mandira.l@email.com", 1, "Mandira Liyanage", "Female", new DateTime(2022, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 4, new DateTime(1998, 4, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "jib@yahoo.com", 2, "Jithma Vithanage", "Female", new DateTime(2020, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 5, new DateTime(1996, 10, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "tilinar@hotmail.com", 2, "Tilina Ratnayake", "Male", new DateTime(2019, 10, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 6, new DateTime(1993, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "kasun@email.com", 2, "Kasun Radhitha", "Male", new DateTime(2018, 12, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 7, new DateTime(1996, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "binura@example.com", 3, "Binura Yasas", "Male", new DateTime(2019, 6, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 8, new DateTime(1990, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "conrad@gmail.com", 3, "Conrad Fisher", "Male", new DateTime(2017, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 9, new DateTime(1990, 3, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "bellyc@yahoo.com", 3, "Belly Conklin", "Female", new DateTime(2017, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 10, new DateTime(1994, 7, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "ryomans@yahoo.com", 1, "Ryomen Sukuna", "Male", new DateTime(2019, 9, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Departments_FacultyId",
                table: "Departments",
                column: "FacultyId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_DepartmentId",
                table: "Students",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_FacultyId",
                table: "Students",
                column: "FacultyId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_StudentTypeId",
                table: "Students",
                column: "StudentTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "StudentTypes");

            migrationBuilder.DropTable(
                name: "Faculties");
        }
    }
}
