using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RpgApi.Migrations
{
    /// <inheritdoc />
    public partial class MigracaoMuitosParaMuitos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_HABILIDADES",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "Varchar(200)", maxLength: 200, nullable: false),
                    Dano = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_HABILIDADES", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TB_PERSONAGENS_HABILIDADES",
                columns: table => new
                {
                    PersonagemId = table.Column<int>(type: "int", nullable: false),
                    HabilidadeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_PERSONAGENS_HABILIDADES", x => new { x.PersonagemId, x.HabilidadeId });
                    table.ForeignKey(
                        name: "FK_TB_PERSONAGENS_HABILIDADES_TB_HABILIDADES_HabilidadeId",
                        column: x => x.HabilidadeId,
                        principalTable: "TB_HABILIDADES",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TB_PERSONAGENS_HABILIDADES_TB_PERSONAGENS_PersonagemId",
                        column: x => x.PersonagemId,
                        principalTable: "TB_PERSONAGENS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "TB_HABILIDADES",
                columns: new[] { "Id", "Dano", "Nome" },
                values: new object[,]
                {
                    { 1, 39, "Adormecer" },
                    { 2, 41, "Congelar" },
                    { 3, 37, "Hipnotizar" }
                });

            migrationBuilder.UpdateData(
                table: "TB_USUARIOS",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PassworSalt", "PasswordHash" },
                values: new object[] { new byte[] { 201, 201, 154, 157, 43, 242, 133, 49, 154, 13, 200, 0, 57, 55, 235, 243, 141, 217, 175, 239, 252, 118, 156, 106, 131, 69, 206, 150, 141, 27, 142, 216, 21, 2, 55, 194, 31, 253, 242, 106, 84, 101, 131, 182, 248, 134, 143, 148, 10, 149, 161, 192, 83, 45, 234, 23, 127, 89, 88, 218, 49, 129, 185, 193, 221, 181, 238, 174, 32, 80, 5, 247, 8, 88, 15, 104, 159, 96, 110, 209, 94, 212, 203, 228, 84, 230, 103, 104, 116, 120, 63, 190, 4, 158, 163, 111, 146, 67, 41, 192, 192, 199, 173, 119, 66, 202, 29, 35, 226, 53, 183, 82, 30, 51, 174, 160, 212, 241, 29, 182, 202, 52, 69, 181, 51, 28, 40, 159 }, new byte[] { 11, 150, 158, 140, 47, 100, 88, 65, 27, 193, 9, 96, 135, 79, 92, 240, 123, 112, 161, 176, 133, 244, 168, 225, 210, 173, 168, 69, 9, 201, 131, 132, 18, 92, 29, 64, 201, 153, 64, 80, 70, 149, 206, 237, 73, 24, 72, 155, 142, 230, 210, 107, 218, 208, 50, 224, 21, 186, 110, 8, 50, 122, 146, 188 } });

            migrationBuilder.InsertData(
                table: "TB_PERSONAGENS_HABILIDADES",
                columns: new[] { "HabilidadeId", "PersonagemId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 2, 2 },
                    { 2, 3 },
                    { 3, 3 },
                    { 3, 4 },
                    { 1, 5 },
                    { 2, 6 },
                    { 3, 7 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_PERSONAGENS_HABILIDADES_HabilidadeId",
                table: "TB_PERSONAGENS_HABILIDADES",
                column: "HabilidadeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_PERSONAGENS_HABILIDADES");

            migrationBuilder.DropTable(
                name: "TB_HABILIDADES");

            migrationBuilder.UpdateData(
                table: "TB_USUARIOS",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PassworSalt", "PasswordHash" },
                values: new object[] { new byte[] { 243, 234, 86, 170, 113, 95, 106, 67, 118, 178, 248, 10, 167, 231, 19, 14, 175, 56, 51, 165, 93, 27, 0, 147, 84, 197, 123, 182, 1, 255, 130, 61, 38, 154, 245, 40, 210, 79, 54, 58, 242, 242, 25, 223, 150, 123, 123, 188, 206, 127, 227, 112, 132, 70, 49, 108, 11, 176, 11, 19, 16, 94, 114, 108, 99, 173, 237, 67, 48, 56, 51, 94, 182, 55, 110, 237, 108, 130, 32, 67, 203, 242, 219, 249, 63, 30, 30, 28, 244, 38, 11, 147, 31, 147, 235, 168, 188, 125, 179, 125, 108, 42, 123, 165, 187, 223, 221, 50, 245, 253, 178, 28, 54, 246, 29, 1, 72, 130, 9, 68, 187, 6, 161, 231, 20, 136, 221, 56 }, new byte[] { 224, 154, 105, 247, 148, 244, 113, 153, 154, 169, 161, 241, 133, 164, 87, 230, 185, 31, 126, 202, 174, 214, 220, 18, 17, 174, 177, 31, 33, 43, 139, 136, 24, 176, 164, 141, 242, 5, 219, 67, 124, 105, 251, 200, 49, 155, 182, 253, 212, 87, 43, 61, 76, 225, 250, 24, 177, 115, 242, 223, 36, 251, 68, 182 } });
        }
    }
}
