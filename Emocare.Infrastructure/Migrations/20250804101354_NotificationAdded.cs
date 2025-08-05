using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Emocare.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NotificationAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserPushSubscription_Users_UserId",
                table: "UserPushSubscription");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserPushSubscription",
                table: "UserPushSubscription");

            migrationBuilder.RenameTable(
                name: "UserPushSubscription",
                newName: "UserPushSubscriptions");

            migrationBuilder.RenameIndex(
                name: "IX_UserPushSubscription_UserId",
                table: "UserPushSubscriptions",
                newName: "IX_UserPushSubscriptions_UserId");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "ReminderTime",
                table: "Habits",
                type: "time",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserPushSubscriptions",
                table: "UserPushSubscriptions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserPushSubscriptions_Users_UserId",
                table: "UserPushSubscriptions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserPushSubscriptions_Users_UserId",
                table: "UserPushSubscriptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserPushSubscriptions",
                table: "UserPushSubscriptions");

            migrationBuilder.DropColumn(
                name: "ReminderTime",
                table: "Habits");

            migrationBuilder.RenameTable(
                name: "UserPushSubscriptions",
                newName: "UserPushSubscription");

            migrationBuilder.RenameIndex(
                name: "IX_UserPushSubscriptions_UserId",
                table: "UserPushSubscription",
                newName: "IX_UserPushSubscription_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserPushSubscription",
                table: "UserPushSubscription",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserPushSubscription_Users_UserId",
                table: "UserPushSubscription",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
