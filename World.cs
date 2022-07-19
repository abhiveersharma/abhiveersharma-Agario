using Microsoft.Extensions.Logging;
using System.Linq;
using System.Text.Json.Serialization;

namespace AgarioModels
{
    /// <summary>
    /// World object for representing the world that the player plays Agario in. Keeps track of food and player objects.
    /// </summary>
    public class World
    {
        public const float _worldHeight = 5000.0F;
        public const float _worldWidth = 5000.0F;

        private ILogger _logger;


        private Dictionary<long, Player> playerList;

        public Dictionary<long, Player> PlayerList
        {
            get { return playerList; } 
            set { playerList = value; }
        }

        public Dictionary<long, Food> FoodList
        {
            get { return foodList; }
            set { foodList = value; }
        }

        private Dictionary<long, Food> foodList;

        [JsonConstructor]
        public World(ILogger logger)
        {
            _logger = logger;
            playerList = new Dictionary<long, Player>();
            foodList = new Dictionary<long, Food>(); 
        }

        /// <summary>
        /// Add the deserialized players into a dictionary when sent from the Server.
        /// </summary>
        /// <param name="playerListFromServer"></param>
        public void updatePlayerList(List<Player> playerListFromServer)
        {
            foreach (Player player in playerListFromServer)
            {
                if (!playerList.ContainsKey(player.ID))
                {
                    playerList.Add(player.ID, player);
                }
                else
                {
                    playerList.Remove(player.ID);
                    playerList.Add(player.ID,player);
                }
            }
        }

        /// <summary>
        /// Gets an update on the food list from the server.
        /// </summary>
        /// <param name="foodListFromServer"></param>
        public void updateFoodList(List<Food> foodListFromServer)
        {
            foreach (Food food in foodListFromServer)
            {
                if (!foodList.ContainsKey(food.ID))
                {
                    foodList.Add(food.ID, food);
                }
            }
        }

        /// <summary>
        /// Iterates through player list to remove all eaten/dead players as reported by the server.
        /// </summary>
        /// <param name="deadPlayerIDsFromServer"></param>
        public void removeDeadPlayers(List<long> deadPlayerIDsFromServer)
        {
            
            foreach(long playerID in deadPlayerIDsFromServer)
            {
                playerList.Remove(playerID);
            }
        }

        /// <summary>
        /// Iterates through food list to remove all eaten food as reported by the server.
        /// </summary>
        /// <param name="eatenFoodIDsFromServer"></param>
        public void removeEatenFood(List<long> eatenFoodIDsFromServer)
        {
            foreach (long foodID in eatenFoodIDsFromServer)
            {
                playerList.Remove(foodID);
            }
        }

    }
}