using Microsoft.EntityFrameworkCore.Migrations;

namespace Rx_Tracker_v3.Migrations
{
    public partial class Test_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TransactionAction",
                table: "Transactions",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TransactionIDType",
                table: "Transactions",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TransactionIDType",
                table: "Transactions");

            migrationBuilder.AlterColumn<string>(
                name: "TransactionAction",
                table: "Transactions",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
