using SQLite;
using SQLiteNetExtensions.Attributes;

namespace DemoMauiFoodRecipe.Models
{
    [Table("Origins")]
    public class Origin
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Recipe> Recipes { get; set; }
    }
}
