using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CritiquesShelfBLL.Migrations
{
    public partial class MultipleAuthors : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TagConnector_BookProposals_BookProposalId",
                table: "TagConnector");

            migrationBuilder.DropIndex(
                name: "IX_TagConnector_BookProposalId",
                table: "TagConnector");

            migrationBuilder.DropColumn(
                name: "BookProposalId",
                table: "TagConnector");

            migrationBuilder.DropColumn(
                name: "AuthorFirstName",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "AuthorLastName",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "AuthorFirstName",
                table: "BookProposals");

            migrationBuilder.DropColumn(
                name: "AuthorLastName",
                table: "BookProposals");

            migrationBuilder.AlterColumn<string>(
                name: "Label",
                table: "Tags",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DatePublished",
                table: "Books",
                type: "int",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<int>(
                name: "DatePublished",
                table: "BookProposals",
                type: "int",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Tags_Label",
                table: "Tags",
                column: "Label");

            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BookId = table.Column<long>(type: "bigint", nullable: true),
                    BookProposalId = table.Column<long>(type: "bigint", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Authors_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Authors_BookProposals_BookProposalId",
                        column: x => x.BookProposalId,
                        principalTable: "BookProposals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TagProposal",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BookProposalId = table.Column<long>(type: "bigint", nullable: true),
                    Label = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagProposal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TagProposal_BookProposals_BookProposalId",
                        column: x => x.BookProposalId,
                        principalTable: "BookProposals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Authors_BookId",
                table: "Authors",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Authors_BookProposalId",
                table: "Authors",
                column: "BookProposalId");

            migrationBuilder.CreateIndex(
                name: "IX_TagProposal_BookProposalId",
                table: "TagProposal",
                column: "BookProposalId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "TagProposal");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Tags_Label",
                table: "Tags");

            migrationBuilder.AlterColumn<string>(
                name: "Label",
                table: "Tags",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<long>(
                name: "BookProposalId",
                table: "TagConnector",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DatePublished",
                table: "Books",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AuthorFirstName",
                table: "Books",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AuthorLastName",
                table: "Books",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DatePublished",
                table: "BookProposals",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AuthorFirstName",
                table: "BookProposals",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AuthorLastName",
                table: "BookProposals",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TagConnector_BookProposalId",
                table: "TagConnector",
                column: "BookProposalId");

            migrationBuilder.AddForeignKey(
                name: "FK_TagConnector_BookProposals_BookProposalId",
                table: "TagConnector",
                column: "BookProposalId",
                principalTable: "BookProposals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
 