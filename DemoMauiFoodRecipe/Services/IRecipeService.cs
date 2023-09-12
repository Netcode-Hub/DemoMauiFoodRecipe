using DemoMauiFoodRecipe.Models;

namespace DemoMauiFoodRecipe.Services
{
    public interface IRecipeService
    {
        // Origin / Category
        Task<int> AddAsync(Origin origin);
        Task<int> UpdateAsync(Origin origin);
        Task<int> DeleteAsync(Origin origin);
        Task<List<Origin>> GetAsync();

        // Recipe 
        Task<int> AddRecipeAsync(Recipe recipe);
        Task<int> UpdateRecipeAsync(Recipe recipe);
        Task<int> DeleteRecipeAsync(Recipe recipe);
        Task<List<Recipe>> GetRecipesAsync();
        Task<Recipe> GetRecipeAsync(int id);
        Task<List<Recipe>> GetAllRecipesByOriginId(int originId);


        // Procedure 
        Task<int> AddProcedureAsync(Procedure procedure);
        Task<int> UpdateProcedureAsync(Procedure procedure);
        Task<int> DeleteProcedureAsync(Procedure procedure);
        Task<List<Procedure>> GetProceduresAsync();
        Task<Procedure> GetProcedureAsync(int id);
        Task<List<Procedure>> GetAllProceduresByRecipeId(int recipeId);
    }
}
