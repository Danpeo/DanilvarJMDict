using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Danilvar.JMDict.Api.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:hstore", ",,");

            migrationBuilder.CreateTable(
                name: "JMDictDatas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Version = table.Column<string>(type: "text", nullable: true),
                    Languages = table.Column<List<string>>(type: "text[]", nullable: true),
                    CommonOnly = table.Column<bool>(type: "boolean", nullable: true),
                    DictRevisions = table.Column<List<string>>(type: "text[]", nullable: true),
                    Tags = table.Column<Dictionary<string, string>>(type: "hstore", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JMDictDatas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Words",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    JMDictDataId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Words", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Words_JMDictDatas_JMDictDataId",
                        column: x => x.JMDictDataId,
                        principalTable: "JMDictDatas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Kana",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Common = table.Column<bool>(type: "boolean", nullable: true),
                    Text = table.Column<string>(type: "text", nullable: true),
                    Tags = table.Column<List<string>>(type: "text[]", nullable: true),
                    AppliesToKanji = table.Column<List<string>>(type: "text[]", nullable: true),
                    WordId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kana", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Kana_Words_WordId",
                        column: x => x.WordId,
                        principalTable: "Words",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Kanji",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Common = table.Column<bool>(type: "boolean", nullable: true),
                    Text = table.Column<string>(type: "text", nullable: true),
                    Tags = table.Column<List<string>>(type: "text[]", nullable: true),
                    WordId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kanji", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Kanji_Words_WordId",
                        column: x => x.WordId,
                        principalTable: "Words",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Senses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PartOfSpeech = table.Column<List<string>>(type: "text[]", nullable: true),
                    AppliesToKanji = table.Column<List<string>>(type: "text[]", nullable: true),
                    AppliesToKana = table.Column<List<string>>(type: "text[]", nullable: true),
                    Related = table.Column<List<string>>(type: "text[]", nullable: true),
                    Antonym = table.Column<List<string>>(type: "text[]", nullable: true),
                    Field = table.Column<List<string>>(type: "text[]", nullable: true),
                    Dialect = table.Column<List<string>>(type: "text[]", nullable: true),
                    Misc = table.Column<List<string>>(type: "text[]", nullable: true),
                    Info = table.Column<List<string>>(type: "text[]", nullable: true),
                    LanguageSource = table.Column<List<string>>(type: "text[]", nullable: true),
                    WordId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Senses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Senses_Words_WordId",
                        column: x => x.WordId,
                        principalTable: "Words",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Glosses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Lang = table.Column<string>(type: "text", nullable: true),
                    Gender = table.Column<string>(type: "text", nullable: true),
                    Type = table.Column<string>(type: "text", nullable: true),
                    Text = table.Column<string>(type: "text", nullable: true),
                    SenseId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Glosses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Glosses_Senses_SenseId",
                        column: x => x.SenseId,
                        principalTable: "Senses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Glosses_SenseId",
                table: "Glosses",
                column: "SenseId");

            migrationBuilder.CreateIndex(
                name: "IX_Kana_WordId",
                table: "Kana",
                column: "WordId");

            migrationBuilder.CreateIndex(
                name: "IX_Kanji_WordId",
                table: "Kanji",
                column: "WordId");

            migrationBuilder.CreateIndex(
                name: "IX_Senses_WordId",
                table: "Senses",
                column: "WordId");

            migrationBuilder.CreateIndex(
                name: "IX_Words_JMDictDataId",
                table: "Words",
                column: "JMDictDataId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Glosses");

            migrationBuilder.DropTable(
                name: "Kana");

            migrationBuilder.DropTable(
                name: "Kanji");

            migrationBuilder.DropTable(
                name: "Senses");

            migrationBuilder.DropTable(
                name: "Words");

            migrationBuilder.DropTable(
                name: "JMDictDatas");
        }
    }
}
