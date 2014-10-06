
namespace PersonnalLibrary.Navigation
{
    public interface INavigationAware
    {
        /// <summary>
        /// Indicate if the view is able to manage the navigation request.
        /// </summary>
        bool IsNavigationTarget { get; set; }

        /// <summary>
        /// Calls after the navigation takes place.
        /// </summary>
        void OnNavigatedTo();

        /// <summary>
        /// Calls before the navigation takes place.
        /// </summary>
        /// <param name="obj">The parameter to pass to the view.</param>
        void OnNavigatedFrom(NavigationParameters parameter);
    }
}
