using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RpgApi.Migrations
{
    /// <inheritdoc />
    public partial class MigracaoUmParaUm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Derrotas",
                table: "TB_PERSONAGENS",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Disputas",
                table: "TB_PERSONAGENS",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Vitorias",
                table: "TB_PERSONAGENS",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PersonagemId",
                table: "TB_ARMAS",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "TB_ARMAS",
                keyColumn: "Id",
                keyValue: 1,
                column: "PersonagemId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "TB_ARMAS",
                keyColumn: "Id",
                keyValue: 2,
                column: "PersonagemId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "TB_ARMAS",
                keyColumn: "Id",
                keyValue: 3,
                column: "PersonagemId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "TB_ARMAS",
                keyColumn: "Id",
                keyValue: 4,
                column: "PersonagemId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "TB_ARMAS",
                keyColumn: "Id",
                keyValue: 5,
                column: "PersonagemId",
                value: 5);

            migrationBuilder.UpdateData(
                table: "TB_ARMAS",
                keyColumn: "Id",
                keyValue: 6,
                column: "PersonagemId",
                value: 6);

            migrationBuilder.UpdateData(
                table: "TB_ARMAS",
                keyColumn: "Id",
                keyValue: 7,
                column: "PersonagemId",
                value: 7);

            migrationBuilder.UpdateData(
                table: "TB_PERSONAGENS",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Derrotas", "Disputas", "Vitorias" },
                values: new object[] { 0, 0, 0 });

            migrationBuilder.UpdateData(
                table: "TB_PERSONAGENS",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Derrotas", "Disputas", "Vitorias" },
                values: new object[] { 0, 0, 0 });

            migrationBuilder.UpdateData(
                table: "TB_PERSONAGENS",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Derrotas", "Disputas", "Vitorias" },
                values: new object[] { 0, 0, 0 });

            migrationBuilder.UpdateData(
                table: "TB_PERSONAGENS",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Derrotas", "Disputas", "Vitorias" },
                values: new object[] { 0, 0, 0 });

            migrationBuilder.UpdateData(
                table: "TB_PERSONAGENS",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Derrotas", "Disputas", "Vitorias" },
                values: new object[] { 0, 0, 0 });

            migrationBuilder.UpdateData(
                table: "TB_PERSONAGENS",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Derrotas", "Disputas", "Vitorias" },
                values: new object[] { 0, 0, 0 });

            migrationBuilder.UpdateData(
                table: "TB_PERSONAGENS",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Derrotas", "Disputas", "Vitorias" },
                values: new object[] { 0, 0, 0 });

            migrationBuilder.UpdateData(
                table: "TB_USUARIOS",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PassworSalt", "PasswordHash" },
                values: new object[] { new byte[] { 243, 234, 86, 170, 113, 95, 106, 67, 118, 178, 248, 10, 167, 231, 19, 14, 175, 56, 51, 165, 93, 27, 0, 147, 84, 197, 123, 182, 1, 255, 130, 61, 38, 154, 245, 40, 210, 79, 54, 58, 242, 242, 25, 223, 150, 123, 123, 188, 206, 127, 227, 112, 132, 70, 49, 108, 11, 176, 11, 19, 16, 94, 114, 108, 99, 173, 237, 67, 48, 56, 51, 94, 182, 55, 110, 237, 108, 130, 32, 67, 203, 242, 219, 249, 63, 30, 30, 28, 244, 38, 11, 147, 31, 147, 235, 168, 188, 125, 179, 125, 108, 42, 123, 165, 187, 223, 221, 50, 245, 253, 178, 28, 54, 246, 29, 1, 72, 130, 9, 68, 187, 6, 161, 231, 20, 136, 221, 56 }, new byte[] { 224, 154, 105, 247, 148, 244, 113, 153, 154, 169, 161, 241, 133, 164, 87, 230, 185, 31, 126, 202, 174, 214, 220, 18, 17, 174, 177, 31, 33, 43, 139, 136, 24, 176, 164, 141, 242, 5, 219, 67, 124, 105, 251, 200, 49, 155, 182, 253, 212, 87, 43, 61, 76, 225, 250, 24, 177, 115, 242, 223, 36, 251, 68, 182 } });

            migrationBuilder.CreateIndex(
                name: "IX_TB_ARMAS_PersonagemId",
                table: "TB_ARMAS",
                column: "PersonagemId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TB_ARMAS_TB_PERSONAGENS_PersonagemId",
                table: "TB_ARMAS",
                column: "PersonagemId",
                principalTable: "TB_PERSONAGENS",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_ARMAS_TB_PERSONAGENS_PersonagemId",
                table: "TB_ARMAS");

            migrationBuilder.DropIndex(
                name: "IX_TB_ARMAS_PersonagemId",
                table: "TB_ARMAS");

            migrationBuilder.DropColumn(
                name: "Derrotas",
                table: "TB_PERSONAGENS");

            migrationBuilder.DropColumn(
                name: "Disputas",
                table: "TB_PERSONAGENS");

            migrationBuilder.DropColumn(
                name: "Vitorias",
                table: "TB_PERSONAGENS");

            migrationBuilder.DropColumn(
                name: "PersonagemId",
                table: "TB_ARMAS");

            migrationBuilder.UpdateData(
                table: "TB_USUARIOS",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PassworSalt", "PasswordHash" },
                values: new object[] { new byte[] { 244, 45, 226, 214, 75, 62, 226, 100, 47, 50, 89, 219, 33, 88, 33, 19, 42, 164, 203, 148, 117, 246, 157, 45, 131, 101, 138, 123, 216, 123, 211, 179, 253, 106, 171, 190, 126, 72, 18, 87, 47, 242, 30, 6, 141, 170, 13, 136, 237, 11, 51, 238, 54, 167, 165, 97, 66, 105, 167, 17, 192, 71, 42, 55, 27, 108, 175, 215, 192, 155, 64, 249, 236, 178, 164, 129, 65, 87, 2, 56, 188, 19, 241, 99, 48, 12, 11, 226, 207, 53, 117, 123, 175, 188, 200, 224, 206, 106, 231, 27, 125, 98, 130, 231, 66, 71, 218, 186, 163, 81, 160, 190, 138, 97, 73, 196, 60, 90, 236, 110, 43, 32, 37, 134, 7, 77, 71, 241 }, new byte[] { 8, 71, 18, 154, 148, 147, 221, 218, 220, 149, 80, 32, 38, 171, 167, 50, 142, 110, 99, 158, 109, 18, 142, 215, 173, 135, 226, 231, 226, 191, 26, 246, 93, 164, 92, 93, 104, 234, 197, 119, 198, 147, 34, 228, 141, 140, 51, 89, 192, 154, 200, 47, 136, 17, 128, 69, 196, 164, 7, 171, 198, 43, 72, 82 } });
        }
    }
}
