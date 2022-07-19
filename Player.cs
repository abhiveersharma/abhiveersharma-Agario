using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AgarioModels
{
    /// <summary>
    /// Represent the player objects that are used in the Agario game. Helps JSON serialize/deserialize them. Each has an additional property of a name that
    /// is displayed for each player as they play the game.
    /// </summary>
    public class Player : GameObject
    {

        public string Name
        {
            get;
            set;
        }
        [JsonConstructor]
        public Player(string Name,long ID,Vector2 location,int ARGBColor,float Mass,float X,float Y):base( ID,  location,  ARGBColor, Mass,X,Y)
        {
            this.Name  = Name;
            this.ID = ID;
            this.Mass = Mass;
            this.ARGBColor = ARGBColor;
            location.X = X;
            location.Y = Y;
            this.Location = location;
        }

        /// <summary>
        /// Try to update user player with info sent from server about all players.
        /// </summary>
        /// <param name="playerListFromServer"></param>
        public void updateUserPlayer(List<Player> playerListFromServer)
        {
            foreach (Player player in playerListFromServer)
            {
                if (playerListFromServer.Contains(this))
                {
                    this.Name = Name;
                    this.ID = ID;
                    this.Mass = Mass;
                    this.ARGBColor = ARGBColor;
                    this.Location = player.Location;
                }
            }
        }
    }
}
