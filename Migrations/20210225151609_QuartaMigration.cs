using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace webapi_EF_mysql.Migrations
{
    public partial class QuartaMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pessoa",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Sucesso = table.Column<bool>(nullable: false),
                    MensagemErro = table.Column<string>(nullable: true),
                    Nome = table.Column<string>(maxLength: 100, nullable: false),
                    Cpf = table.Column<string>(maxLength: 11, nullable: false),
                    DataNascimento = table.Column<string>(nullable: false),
                    Sexo = table.Column<string>(maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pessoa", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pessoa");
        }
    }
}
