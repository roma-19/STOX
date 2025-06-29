using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace STOX.Repo.Migrations
{
    /// <inheritdoc />
    public partial class AddHandlerIdToOrders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "HandlerId",
                table: "orders",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_orders_user_HandlerId",
                table: "orders",
                column: "HandlerId",
                principalTable: "user",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orders_user_HandlerId",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "HandlerId",
                table: "orders");
        }
    }
}
