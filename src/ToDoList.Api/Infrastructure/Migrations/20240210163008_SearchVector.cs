using Microsoft.EntityFrameworkCore.Migrations;
using NpgsqlTypes;

#nullable disable

namespace ToDoList.Api.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SearchVector : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<NpgsqlTsVector>(
                name: "search_vector",
                table: "to_dos",
                type: "tsvector",
                nullable: false)
                .Annotation("Npgsql:TsVectorConfig", "english")
                .Annotation("Npgsql:TsVectorProperties", new[] { "name", "id" });

            migrationBuilder.CreateIndex(
                name: "ix_to_dos_search_vector",
                table: "to_dos",
                column: "search_vector")
                .Annotation("Npgsql:IndexMethod", "GIN");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_to_dos_search_vector",
                table: "to_dos");

            migrationBuilder.DropColumn(
                name: "search_vector",
                table: "to_dos");
        }
    }
}
