using SQLite;
using SQLiteNetExtensions.Attributes;
namespace DemoMauiFoodRecipe.Models
{
    [Table("Procedures")]
    public class Procedure
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int Number { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string TimeNeeded { get; set; }
        [ManyToOne(CascadeOperations = CascadeOperation.All)]
        public Recipe Recipe { get; set; }
        [ForeignKey(typeof(Recipe))]
        public int RecipeId { get; set; }
    }
}
