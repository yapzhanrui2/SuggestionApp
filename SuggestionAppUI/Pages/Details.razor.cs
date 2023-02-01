using Microsoft.AspNetCore.Components;

namespace SuggestionAppUI.Pages
{
    public partial class Details
    {
        [Parameter]
        public string Id { get; set; }

        private SuggestionModel suggestion;
        private UserModel loggedInUser;
        private List<StatusModel> statuses;
        private string settingStatus = "";
        private string urlText = "";
        protected async override Task OnInitializedAsync()
        {
            suggestion = await suggestionData.GetSuggestion(Id);
            loggedInUser = await authProvider.GetUserFromAuth(userData);
            statuses = await statusData.GetAllStatuses();
        }

        private async Task CompleteSetStatus()
        {
            switch (settingStatus)
            {
                case "completed":
                    if (string.IsNullOrWhiteSpace(urlText))
                    {
                        return;
                    }

                    suggestion.SuggestionStatus = statuses.Where(s => s.StatusName.ToLower() == settingStatus.ToLower()).First();
                    suggestion.OwnerNotes = $"You are right, this is an important topic. Resource created here at: <a href='{urlText}' target='_blank'>{urlText}</a>";
                    break;
                case "watching":
                    suggestion.SuggestionStatus = statuses.Where(s => s.StatusName.ToLower() == settingStatus.ToLower()).First();
                    suggestion.OwnerNotes = "We noticed this interesting suggestion, waiting for more people to be interested to address it.";
                    break;
                case "upcoming":
                    suggestion.SuggestionStatus = statuses.Where(s => s.StatusName.ToLower() == settingStatus.ToLower()).First();
                    suggestion.OwnerNotes = "Upcoming feature";
                    break;
                case "dismissed":
                    suggestion.SuggestionStatus = statuses.Where(s => s.StatusName.ToLower() == settingStatus.ToLower()).First();
                    suggestion.OwnerNotes = "Good idea but doesnt fit in our scope sorry!";
                    break;
                default:
                    return;
            }

            settingStatus = null;
            await suggestionData.UpdateSuggestion(suggestion);
        }

        private void ClosePage()
        {
            navManager.NavigateTo("/");
        }

        private string GetUpvoteTopText()
        {
            if (suggestion.UserVotes?.Count > 0)
            {
                return suggestion.UserVotes.Count.ToString("00");
            }
            else
            {
                if (suggestion.Author.Id == loggedInUser?.Id)
                {
                    return "Awaiting";
                }
                else
                {
                    return "Click To";
                }
            }
        }

        private string GetUpvoteBottomText()
        {
            if (suggestion.UserVotes?.Count > 1)
            {
                return "Upvotes";
            }
            else
            {
                return "Upvote";
            }
        }

        private async Task VoteUp()
        {
            if (loggedInUser is not null)
            {
                if (suggestion.Author.Id == loggedInUser.Id)
                {
                    // Can't Vote On Your Own Suggestion
                    return;
                }

                if (suggestion.UserVotes.Add(loggedInUser.Id) == false)
                {
                    suggestion.UserVotes.Remove(loggedInUser.Id);
                }

                await suggestionData.UpvoteSuggetion(suggestion.Id, loggedInUser.Id);
            }
            else
            {
                navManager.NavigateTo("/MicrosoftIdentity/Account/SignIn", true);
            }
        }

        private string GetVoteClass()
        {
            if (suggestion.UserVotes is null || suggestion.UserVotes.Count == 0)
            {
                return "suggestion-detail-no-votes";
            }
            else if (suggestion.UserVotes.Contains(loggedInUser?.Id))
            {
                return "suggestion-detail-voted";
            }
            else
            {
                return "suggestion-detail-not-voted";
            }
        }

        private string GetStatusClass()
        {
            if (suggestion is null || suggestion.SuggestionStatus is null)
            {
                return "suggestion-detail-status-none";
            }

            string output = suggestion.SuggestionStatus.StatusName switch
            {
                "Completed" => "suggestion-detail-status-completed",
                "Watching" => "suggestion-detail-status-watching",
                "Upcoming" => "suggestion-detail-status-upcoming",
                "Dismissed" => "suggestion-detail-status-dismissed",
                _ => "suggestion-detail-status-none",
            };
            return output;
        }
    }
}