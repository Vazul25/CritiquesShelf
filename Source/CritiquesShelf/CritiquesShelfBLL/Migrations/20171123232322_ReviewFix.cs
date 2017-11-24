using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CritiquesShelfBLL.Migrations
{
    public partial class ReviewFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Books_BookId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_TagProposal_BookProposals_BookProposalId",
                table: "TagProposal");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TagProposal",
                table: "TagProposal");

            migrationBuilder.RenameTable(
                name: "TagProposal",
                newName: "TagProposals");

            migrationBuilder.RenameIndex(
                name: "IX_TagProposal_BookProposalId",
                table: "TagProposals",
                newName: "IX_TagProposals_BookProposalId");

            migrationBuilder.AlterColumn<double>(
                name: "Score",
                table: "Reviews",
                type: "float",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<long>(
                name: "BookId",
                table: "Reviews",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TagProposals",
                table: "TagProposals",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Books_BookId",
                table: "Reviews",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TagProposals_BookProposals_BookProposalId",
                table: "TagProposals",
                column: "BookProposalId",
                principalTable: "BookProposals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Books_BookId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_TagProposals_BookProposals_BookProposalId",
                table: "TagProposals");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TagProposals",
                table: "TagProposals");

            migrationBuilder.RenameTable(
                name: "TagProposals",
                newName: "TagProposal");

            migrationBuilder.RenameIndex(
                name: "IX_TagProposals_BookProposalId",
                table: "TagProposal",
                newName: "IX_TagProposal_BookProposalId");

            migrationBuilder.AlterColumn<int>(
                name: "Score",
                table: "Reviews",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<long>(
                name: "BookId",
                table: "Reviews",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TagProposal",
                table: "TagProposal",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Books_BookId",
                table: "Reviews",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TagProposal_BookProposals_BookProposalId",
                table: "TagProposal",
                column: "BookProposalId",
                principalTable: "BookProposals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
