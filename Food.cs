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
    public class Food : GameObject
    {
        [JsonConstructor]
        public Food(long ID, Vector2 location, int ARGBColor, float Mass,float X,float Y) : base(ID, location, ARGBColor, Mass,X,Y)
        {
            
        }
    }
}
