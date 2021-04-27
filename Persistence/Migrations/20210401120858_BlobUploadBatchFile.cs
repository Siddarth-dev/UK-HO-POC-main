using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class BlobUploadBatchFile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UploadBatchFiles",
                columns: table => new
                {
                    UploadBatchFileId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    LastModified = table.Column<DateTime>(nullable: true),
                    BatchID = table.Column<Guid>(nullable: false),
                    ContainerName = table.Column<string>(maxLength: 40, nullable: true),
                    FileName = table.Column<string>(maxLength: 100, nullable: true),
                    FileSize = table.Column<long>(nullable: false),
                    MimeType = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UploadBatchFiles", x => x.UploadBatchFileId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UploadBatchFiles");
        }
    }
}
