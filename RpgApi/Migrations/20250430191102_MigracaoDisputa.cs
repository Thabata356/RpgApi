using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RpgApi.Migrations
{
    /// <inheritdoc />
    public partial class MigracaoDisputa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "passwordSalt",
                table: "TB_USUARIOS",
                newName: "PasswordSalt");

            migrationBuilder.CreateTable(
                name: "TB_DISPUTAS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Dt_Disputa = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AtacanteId = table.Column<int>(type: "int", nullable: false),
                    OponenteId = table.Column<int>(type: "int", nullable: false),
                    Tx_Narracao = table.Column<string>(type: "Varchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_DISPUTAS", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "TB_USUARIOS",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 72, 216, 226, 248, 243, 75, 19, 107, 179, 29, 2, 160, 251, 2, 206, 137, 141, 23, 221, 126, 66, 37, 36, 81, 23, 251, 57, 73, 230, 129, 59, 28, 198, 149, 67, 113, 136, 237, 5, 64, 156, 8, 148, 181, 252, 73, 100, 50, 31, 202, 220, 148, 167, 252, 106, 82, 91, 150, 255, 44, 97, 226, 142, 179 }, new byte[] { 231, 165, 139, 2, 3, 203, 46, 159, 84, 40, 232, 243, 198, 93, 37, 86, 10, 73, 74, 18, 28, 2, 168, 61, 220, 151, 85, 10, 151, 205, 50, 190, 167, 150, 12, 57, 123, 24, 152, 11, 187, 219, 209, 194, 13, 29, 130, 253, 119, 124, 202, 218, 101, 227, 41, 99, 162, 91, 93, 251, 111, 157, 159, 82, 153, 177, 56, 51, 93, 136, 227, 67, 80, 15, 231, 166, 60, 213, 80, 191, 111, 11, 49, 82, 93, 218, 200, 75, 206, 74, 25, 162, 5, 148, 201, 92, 7, 94, 110, 110, 157, 55, 81, 105, 235, 46, 143, 157, 204, 3, 119, 132, 3, 104, 221, 23, 156, 92, 50, 212, 5, 151, 77, 233, 209, 253, 96, 10 } });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_DISPUTAS");

            migrationBuilder.RenameColumn(
                name: "PasswordSalt",
                table: "TB_USUARIOS",
                newName: "passwordSalt");

            migrationBuilder.UpdateData(
                table: "TB_USUARIOS",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "passwordSalt", "PasswordHash" },
                values: new object[] { new byte[] { 201, 201, 154, 157, 43, 242, 133, 49, 154, 13, 200, 0, 57, 55, 235, 243, 141, 217, 175, 239, 252, 118, 156, 106, 131, 69, 206, 150, 141, 27, 142, 216, 21, 2, 55, 194, 31, 253, 242, 106, 84, 101, 131, 182, 248, 134, 143, 148, 10, 149, 161, 192, 83, 45, 234, 23, 127, 89, 88, 218, 49, 129, 185, 193, 221, 181, 238, 174, 32, 80, 5, 247, 8, 88, 15, 104, 159, 96, 110, 209, 94, 212, 203, 228, 84, 230, 103, 104, 116, 120, 63, 190, 4, 158, 163, 111, 146, 67, 41, 192, 192, 199, 173, 119, 66, 202, 29, 35, 226, 53, 183, 82, 30, 51, 174, 160, 212, 241, 29, 182, 202, 52, 69, 181, 51, 28, 40, 159 }, new byte[] { 11, 150, 158, 140, 47, 100, 88, 65, 27, 193, 9, 96, 135, 79, 92, 240, 123, 112, 161, 176, 133, 244, 168, 225, 210, 173, 168, 69, 9, 201, 131, 132, 18, 92, 29, 64, 201, 153, 64, 80, 70, 149, 206, 237, 73, 24, 72, 155, 142, 230, 210, 107, 218, 208, 50, 224, 21, 186, 110, 8, 50, 122, 146, 188 } });
        }
    }
}
