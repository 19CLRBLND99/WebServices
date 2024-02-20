namespace WebServicesBackend.Database
{
    public class DatabaseHouseService
    {
       public Tuple<bool, int?> AddHouse(string houseName)
        {
            //TODO implement
            return  new Tuple<bool, int?>(true, 1);
        }

        public bool DeleteHouse(int houseId)
        {
            //TODO implement
            return true;
        }

        public Tuple<bool, string?> UpdateHouse(string newHouseName, int houseId)
        {
            //TODO implement
            return new Tuple<bool, string?>(true, "Barbies Traumhaus");
        }
    }
}
