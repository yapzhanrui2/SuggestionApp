using SuggestionAppUI.Models;

namespace SuggestionAppUI.Pages
{
    public partial class Create
    {
        private CreateSuggestionModel suggestion = new();
        private List<CategoryModel> categories;
        private UserModel loggedInUser;
        protected async override Task OnInitializedAsync()
        {
            categories = await categoryData.GetAllCategories();
            loggedInUser = await authProvider.GetUserFromAuth(userData);
        }

        private void ClosePage()
        {
            navManager.NavigateTo("/");
        }

        private async Task CreateSuggestion()
        {
            SuggestionModel s = new();
            s.Suggestion = suggestion.Suggestion;
            s.Description = suggestion.Description;
            s.Author = new BasicUserModel(loggedInUser);
            s.Category = categories.Where(c => c.Id == suggestion.CategoryID).FirstOrDefault();
            if (s.Category is null)
            {
                suggestion.CategoryID = "";
                return;
            }

            await suggestionData.CreateSuggestion(s);
            suggestion = new();
            ClosePage();
        }
    }
}