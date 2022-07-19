
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
    /// Represents a general game object in the Agario game. Helps serialize and deserialize the objects to send and receive from the server. Essentially just defines 
    /// and object's ID for identification, the location in x,y coords, the radius of the mostly circular game objects, the color of the object, and the mass/size of the object.
    /// Provides a constructor for JSON serialize/deserialize.
    /// </summary>
    public class GameObject
    {
        private long _ID; 
        
        public long ID 
        {
            get { return _ID; }
            set { _ID = value; } 
        }

        private Vector2 _location;
        [JsonIgnore]
        public Vector2  Location
        {
            get { return _location; }
            set 
            { 
                _location.X = value.X;
                _location.Y = value.Y;
            }
        }

        public float X 
        {   
            get { return _location.X;}
        }
        public float Y
        {
            get { return _location.Y;}
        }

        [JsonPropertyName("ARGBColor")]
        public int ARGBColor { get; set; }

        [JsonPropertyName("Mass")]
        public float Mass { get; set; }

        public float Radius 
        {
            get
            {
                float foodArea = this.Mass;
                float foodRadius = (float)Math.Sqrt(foodArea / (float)Math.PI);
                return foodRadius;
            }
        }

        [JsonConstructor]
        public GameObject(long ID,Vector2 location,int ARGBColor,float Mass,float X, float Y)
        {
            this.ID = ID;
            this.Location = new Vector2(X, Y);
            this.ARGBColor = ARGBColor;
            this.Mass = Mass;
        }

    }
}
