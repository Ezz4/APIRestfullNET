using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RestAPIHotel.Migrations
{
    /// <inheritdoc />
    public partial class NewRoomsForHotel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "Amenity", "Area", "CreatedDate", "Details", "ImageUrl", "Name", "Occupancy", "Rate", "UpdateDate", "Url" },
                values: new object[,]
                {
                    { 1, "personal hygiene products", 550, new DateTime(2023, 8, 10, 20, 2, 14, 365, DateTimeKind.Local).AddTicks(9245), "A very big room and a jacuzzi", "https://imagenes.forociudad.com/fotos/203185-casa-fea.jpg", "Luxory suite", 5, 200.0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Not valid for now" },
                    { 2, "personal hygiene products", 500, new DateTime(2023, 8, 10, 20, 2, 14, 365, DateTimeKind.Local).AddTicks(9258), "A very not so big room and a small jacuzzi", "https://st.depositphotos.com/71898554/60977/i/450/depositphotos_609775626-stock-photo-a-lot-of-garbage-in.jpg", "Not so Luxory suite", 5, 180.0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Not valid for now" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
