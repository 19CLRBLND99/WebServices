using WebServicesBackend.Database;

namespace WebServicesBackend.Services
{
    public class HouseService
    {
        /// <summary>
        /// Method which is used to create a new house.
        /// </summary>
        /// <param name="houseName">The name of the house to be created</param>
        /// <returns>A Tuple which contains a boolean that indicates whether the creation was successfull or not and the Id of the newly created house</returns>
        public Tuple<bool, int?> AddHouse(string houseName)
        {
            var houseDbService = new DatabaseHouseService();
            var result = houseDbService.AddHouse(houseName);

            return (result.Item1) ? result : new Tuple<bool, int?>(false, null);
        }

        /// <summary>
        /// Method which is used for deleting an existing house
        /// </summary>
        /// <param name="houseId">The Id of the house to be deleted </param>
        /// <returns>A boolean that indicates whether the deletion was successfull or not </returns>
        public bool DeleteHouse(int houseId)
        {
            var houseDbService = new DatabaseHouseService();
            var result = houseDbService.DeleteHouse(houseId);

            return (result) ? result : false;
        }

        /// <summary>
        /// Method which is used for updating the name of a house 
        /// </summary>
        /// <param name="newHouseName">the new name for the house </param>
        /// <param name="houseId">the id of an existing house </param>
        /// <returns>a tuple of a boolean and a string. The boolean indicates whether the update was successfull or not and the string is the updated name </returns>
        public Tuple<bool, string?> UpdateHouse(string newHouseName, int houseId)
        {
            var houseDbService = new DatabaseHouseService();
            var result = houseDbService.UpdateHouse(newHouseName, houseId);

            return (result.Item1) ? result : new Tuple<bool, string?>(false, null);
        }
    }
}
