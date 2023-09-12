using SQLite;
using SQLiteNetExtensions.Attributes;
namespace DemoMauiFoodRecipe.Models
{
    [Table("Recipes")]
    public class Recipe
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Rank { get; set; }
        public string TimeNeeded { get; set; }
        public string Image { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; } = DateTime.Now;

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Procedure> Procedures { get; set; }

        [ManyToOne(CascadeOperations = CascadeOperation.All)]
        public Origin Origin { get; set; }

        [ForeignKey(typeof(Origin))]
        public int OriginId { get; set; }
    }
}
