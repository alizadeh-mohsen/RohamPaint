using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RohamPaint.Migrations
{
    /// <inheritdoc />
    public partial class AddColorFormulAndCarChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Create table
            migrationBuilder.CreateTable(
                name: "ColorFormul",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ColorID = table.Column<int>(nullable: false),
                    BaseColor = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Weight = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ColorFormul", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ColorFormul_Color",
                        column: x => x.ColorID,
                        principalTable: "Color",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            // Insert data using raw SQL
            migrationBuilder.Sql(@"
            INSERT INTO ColorFormul (ColorID, BaseColor, Weight)
            SELECT 
                f.ColorID,
                (SELECT b.Code FROM BaseColor b WHERE f.BaseId = b.ID),
                f.Weight
            FROM formul f
        ");

            // Add new column to Car
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Car",
                type: "nvarchar(50)",
                nullable: true);

            // Copy values
            migrationBuilder.Sql("UPDATE Car SET Name = Car");

            // Drop old column
            migrationBuilder.DropColumn(
                name: "Car",
                table: "Car");

            // Indexes
            migrationBuilder.CreateIndex(
                name: "IX_Color_Code",
                table: "Color",
                column: "Code");

            migrationBuilder.CreateIndex(
                name: "IX_Color_CarID",
                table: "Color",
                column: "CarID");

            migrationBuilder.CreateIndex(
                name: "IX_Color_ColorTypeID",
                table: "Color",
                column: "ColorTypeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Reverse indexes
            migrationBuilder.DropIndex("IX_Color_Code", "Color");
            migrationBuilder.DropIndex("IX_Color_CarID", "Color");
            migrationBuilder.DropIndex("IX_Color_ColorTypeID", "Color");

            // Recreate dropped Car column
            migrationBuilder.AddColumn<string>(
                name: "Car",
                table: "Car",
                type: "nvarchar(max)",
                nullable: true);

            // Restore data
            migrationBuilder.Sql("UPDATE Car SET Car = Name");

            // Drop new column
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Car");

            // Drop table
            migrationBuilder.DropTable(name: "ColorFormul");
        }
    }

}
