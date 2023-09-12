
using DemoMauiFoodRecipe.Models;
using SQLite;
using SQLiteNetExtensionsAsync.Extensions;

namespace DemoMauiFoodRecipe.Services
{
    public class RecipeService : IRecipeService
    {
        private SQLiteAsyncConnection connection;
        public RecipeService()
        {
            SetupDatabase();
        }
        private async void SetupDatabase()
        {
            if (connection is null)
            {
                string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "DemoMauiRecipe1.db3");
                connection = new SQLiteAsyncConnection(dbPath);
               // await connection.CreateTablesAsync<Procedure, Origin, Recipe>();
                await connection.CreateTableAsync<Procedure>();
                await connection.CreateTableAsync<Recipe>();
                await connection.CreateTableAsync<Origin>();
            }
        }

        // Origin Service
        public async Task<int> AddAsync(Origin origin)
        {
            if (origin is null)
                return (int)System.Net.HttpStatusCode.BadRequest;

            int result = await connection.InsertAsync(origin);
            return result;
        }
        public async Task<int> DeleteAsync(Origin origin)
        {
            var result = await connection?.DeleteAsync(origin);
            return result;
        }
        public async Task<List<Origin>> GetAsync()
        {
            var result = await connection.Table<Origin>().ToListAsync();
            return result;
        }

        public async Task<int> UpdateAsync(Origin origin)
        {
            var result = await connection.UpdateAsync(origin);
            return result;
        }


        //Recipe
        public async Task<int> AddRecipeAsync(Recipe recipe)
        {
            if (recipe is null)
                return (int)System.Net.HttpStatusCode.BadRequest;

            int result = await connection.InsertAsync(recipe);
            return result;
        }
        public async Task<int> DeleteRecipeAsync(Recipe recipe)
        {
            var result = await connection?.DeleteAsync(recipe);
            return result;
        }
        public async Task<int> UpdateRecipeAsync(Recipe recipe)
        {
            var result = await connection.UpdateAsync(recipe);
            return result;
        }
        public async Task<List<Recipe>> GetRecipesAsync()
        {
            var result = await connection.GetAllWithChildrenAsync<Recipe>(recursive: true);
            if (result is null) return null;
            return result.ToList();
        }
        public async Task<Recipe> GetRecipeAsync(int id)
        {
            var result = await connection.Table<Recipe>().Where(_ => _.Id == id).FirstOrDefaultAsync();
            return result;
        }

        public async Task<List<Recipe>> GetAllRecipesByOriginId(int originId)
        {
            var result = await connection.GetAllWithChildrenAsync<Recipe>(_ => _.OriginId == originId, recursive: true);
            if (result.Count == 0) return null;
            return result.ToList();
        }

        // Procedure Service
        public async Task<int> AddProcedureAsync(Procedure procedure)
        {
            if (procedure is null)
                return (int)System.Net.HttpStatusCode.BadRequest;

            int result = await connection.InsertAsync(procedure);
            return result;
        }

        public async Task<int> UpdateProcedureAsync(Procedure procedure)
        {
            var result = await connection.UpdateAsync(procedure);
            return result;
        }

        public async Task<int> DeleteProcedureAsync(Procedure procedure)
        {
            var result = await connection?.DeleteAsync(procedure);
            return result;
        }

        public async Task<List<Procedure>> GetProceduresAsync()
        {
            var result = await connection.GetAllWithChildrenAsync<Procedure>(recursive: true);
            if (result.Count == 0) return null;
            return result.ToList();
        }

        public async Task<Procedure> GetProcedureAsync(int id)
        {
            var result = await connection.Table<Procedure>().Where(_ => _.Id == id).FirstOrDefaultAsync();
            return result;
        }

        public async Task<List<Procedure>> GetAllProceduresByRecipeId(int recipeId)
        {
            var result = await connection.GetAllWithChildrenAsync<Procedure>(_ => _.RecipeId == recipeId, recursive: true);
            if (result.Count == 0) return null;
            return result.ToList();
        }
    }
}
