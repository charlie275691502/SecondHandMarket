using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Marketplace.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddListingCoverImageId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ListingImages_Listings_ListingId1",
                table: "ListingImages");

            migrationBuilder.DropIndex(
                name: "IX_ListingImages_ListingId1",
                table: "ListingImages");

            migrationBuilder.DropColumn(
                name: "ListingId1",
                table: "ListingImages");

            migrationBuilder.AddColumn<Guid>(
                name: "CoverImageId",
                table: "Listings",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CoverImageId1",
                table: "Listings",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Listings_CoverImageId1",
                table: "Listings",
                column: "CoverImageId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Listings_ListingImages_CoverImageId1",
                table: "Listings",
                column: "CoverImageId1",
                principalTable: "ListingImages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Listings_ListingImages_CoverImageId1",
                table: "Listings");

            migrationBuilder.DropIndex(
                name: "IX_Listings_CoverImageId1",
                table: "Listings");

            migrationBuilder.DropColumn(
                name: "CoverImageId",
                table: "Listings");

            migrationBuilder.DropColumn(
                name: "CoverImageId1",
                table: "Listings");

            migrationBuilder.AddColumn<Guid>(
                name: "ListingId1",
                table: "ListingImages",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ListingImages_ListingId1",
                table: "ListingImages",
                column: "ListingId1",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ListingImages_Listings_ListingId1",
                table: "ListingImages",
                column: "ListingId1",
                principalTable: "Listings",
                principalColumn: "Id");
        }
    }
}
