using Domain.Models.WorkshopDomaine;

namespace Domain.Interfaces
{
    public interface IWorkshopRepository : IGenericRepository<Workshop, int>
    {
        /// <summary>
        /// Retrieves workshop information and checks if the provided email exists in the database for a workshop login.
        /// </summary>
        /// <param name="email">The email address to search for.</param>
        /// <returns>A tuple containing the Workshop object (or null) and a boolean indicating whether the email exists in the database.</returns>
        Task<(Workshop?, bool)> GetWorkshopInformationForLogin(string email);

        /// <summary>
        /// Checks if the provided email address exists in the Workshops table.
        /// </summary>
        /// <param name="email">The email address to check for existence.</param>
        /// <returns>A boolean value indicating whether the email address exists (true) or not (false).</returns>
        Task<bool> CheckIfEmailExists(string email);
    }
}