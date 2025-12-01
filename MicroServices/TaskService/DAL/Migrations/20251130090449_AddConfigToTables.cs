using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddConfigToTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskTags_Tags_TagId",
                table: "TaskTags");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskTags_Tasks_TaskId",
                table: "TaskTags");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskTechnologies_Tasks_TaskId",
                table: "TaskTechnologies");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskTechnologies_Technologies_TechnologyId",
                table: "TaskTechnologies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Technologies",
                table: "Technologies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TaskTechnologies",
                table: "TaskTechnologies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TaskTags",
                table: "TaskTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tasks",
                table: "Tasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tags",
                table: "Tags");

            migrationBuilder.RenameTable(
                name: "Technologies",
                newName: "technology");

            migrationBuilder.RenameTable(
                name: "TaskTechnologies",
                newName: "task-technology");

            migrationBuilder.RenameTable(
                name: "TaskTags",
                newName: "task_tag");

            migrationBuilder.RenameTable(
                name: "Tasks",
                newName: "task");

            migrationBuilder.RenameTable(
                name: "Tags",
                newName: "tag");

            migrationBuilder.RenameIndex(
                name: "IX_TaskTechnologies_TechnologyId",
                table: "task-technology",
                newName: "IX_task-technology_TechnologyId");

            migrationBuilder.RenameIndex(
                name: "IX_TaskTags_TagId",
                table: "task_tag",
                newName: "IX_task_tag_TagId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "technology",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "task",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "NOW()",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "task",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "task",
                type: "boolean",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "task",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "NOW()",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "tag",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddPrimaryKey(
                name: "PK_technology",
                table: "technology",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_task-technology",
                table: "task-technology",
                columns: new[] { "TaskId", "TechnologyId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_task_tag",
                table: "task_tag",
                columns: new[] { "TaskId", "TagId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_task",
                table: "task",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tag",
                table: "tag",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_task_tag_tag_TagId",
                table: "task_tag",
                column: "TagId",
                principalTable: "tag",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_task_tag_task_TaskId",
                table: "task_tag",
                column: "TaskId",
                principalTable: "task",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_task-technology_task_TaskId",
                table: "task-technology",
                column: "TaskId",
                principalTable: "task",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_task-technology_technology_TechnologyId",
                table: "task-technology",
                column: "TechnologyId",
                principalTable: "technology",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_task_tag_tag_TagId",
                table: "task_tag");

            migrationBuilder.DropForeignKey(
                name: "FK_task_tag_task_TaskId",
                table: "task_tag");

            migrationBuilder.DropForeignKey(
                name: "FK_task-technology_task_TaskId",
                table: "task-technology");

            migrationBuilder.DropForeignKey(
                name: "FK_task-technology_technology_TechnologyId",
                table: "task-technology");

            migrationBuilder.DropPrimaryKey(
                name: "PK_technology",
                table: "technology");

            migrationBuilder.DropPrimaryKey(
                name: "PK_task-technology",
                table: "task-technology");

            migrationBuilder.DropPrimaryKey(
                name: "PK_task_tag",
                table: "task_tag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_task",
                table: "task");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tag",
                table: "tag");

            migrationBuilder.RenameTable(
                name: "technology",
                newName: "Technologies");

            migrationBuilder.RenameTable(
                name: "task-technology",
                newName: "TaskTechnologies");

            migrationBuilder.RenameTable(
                name: "task_tag",
                newName: "TaskTags");

            migrationBuilder.RenameTable(
                name: "task",
                newName: "Tasks");

            migrationBuilder.RenameTable(
                name: "tag",
                newName: "Tags");

            migrationBuilder.RenameIndex(
                name: "IX_task-technology_TechnologyId",
                table: "TaskTechnologies",
                newName: "IX_TaskTechnologies_TechnologyId");

            migrationBuilder.RenameIndex(
                name: "IX_task_tag_TagId",
                table: "TaskTags",
                newName: "IX_TaskTags_TagId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Technologies",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Tasks",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "NOW()");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Tasks",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "Tasks",
                type: "boolean",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValue: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Tasks",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "NOW()");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Tags",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Technologies",
                table: "Technologies",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TaskTechnologies",
                table: "TaskTechnologies",
                columns: new[] { "TaskId", "TechnologyId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_TaskTags",
                table: "TaskTags",
                columns: new[] { "TaskId", "TagId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tasks",
                table: "Tasks",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tags",
                table: "Tags",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskTags_Tags_TagId",
                table: "TaskTags",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskTags_Tasks_TaskId",
                table: "TaskTags",
                column: "TaskId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskTechnologies_Tasks_TaskId",
                table: "TaskTechnologies",
                column: "TaskId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskTechnologies_Technologies_TechnologyId",
                table: "TaskTechnologies",
                column: "TechnologyId",
                principalTable: "Technologies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
