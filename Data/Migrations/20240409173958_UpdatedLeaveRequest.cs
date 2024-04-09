using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeaveManagement.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedLeaveRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RequestComments",
                table: "LeaveRequests",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<bool>(
                name: "Cancelled",
                table: "LeaveRequests",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3f4631bd-f907-4409-b416-ba356312e659",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d9793e04-c3d4-45cc-910f-faf53ba5ef2e", "AQAAAAIAAYagAAAAEMxeX4Epq3u0uonIKxiDKZbdbNsa0+TWvwDdOG6cmtnnkj8nmnfkmqEyvq8iccJeJg==", "6a8ed363-8201-4657-acf9-b50d634c1cd6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "408aa945-3d84-4421-8342-7269ec64d949",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "61055660-cffc-42a3-94bb-35e070b38cb9", "AQAAAAIAAYagAAAAEL5zmqsvtgHWNEllidHOSkXrRo9SDYPsYLLEHCvfHKENKcPybDs7DTnbyEhGJD90fg==", "5768a897-517d-411c-96b8-26483c9a6399" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RequestComments",
                table: "LeaveRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Cancelled",
                table: "LeaveRequests",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3f4631bd-f907-4409-b416-ba356312e659",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6798c296-77a8-4b64-bc10-5b0f991bf1e8", "AQAAAAIAAYagAAAAEPJTUpwSscgUrRHr51uKRqnXlEP8nrDRsN0HljYNLk+VecJIx5hxWS+4zQTy4yZGZw==", "ed110d87-0de9-4dbd-9254-0b2480b96739" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "408aa945-3d84-4421-8342-7269ec64d949",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4f9874dd-16e0-406c-9910-0e919a5f490d", "AQAAAAIAAYagAAAAEG1FF9O/Se38TIMG3E09tjyfPcKHpbldlVoi4cFHKmySKpbB89hq+5oFFs5OR7jAyg==", "f07633ed-e856-4a5b-a47f-b5c117acad73" });
        }
    }
}
