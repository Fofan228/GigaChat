using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GigaChat.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class FuckingShit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "owner_id",
                table: "chat_rooms",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "ix_chat_rooms_owner_id",
                table: "chat_rooms",
                column: "owner_id");

            migrationBuilder.AddForeignKey(
                name: "fk_chat_rooms_users_user_id",
                table: "chat_rooms",
                column: "owner_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_chat_rooms_users_user_id",
                table: "chat_rooms");

            migrationBuilder.DropIndex(
                name: "ix_chat_rooms_owner_id",
                table: "chat_rooms");

            migrationBuilder.DropColumn(
                name: "owner_id",
                table: "chat_rooms");
        }
    }
}
