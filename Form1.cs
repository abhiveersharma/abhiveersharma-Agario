using System.Numerics;

namespace TowardAgarioStepOne
{
    /// <summary>
    /// Simple GUI code to practice drawing graphics on form for Agario project. Uses paint even handler, double buffering, and a timer for invalidating.
    /// </summary>
    public partial class Form1 : Form
    {
        private Vector2 CircleCenter;
        private Vector2 direction = new Vector2(5, 5);
        public Form1()
        {
            InitializeComponent();
            this.Paint += Draw_Scene;
            this.DoubleBuffered = true;
            var timer = new System.Windows.Forms.Timer();
            timer.Interval = 1000 / 30;  // 1000 milliseconds in a second divided by 30 frames per second
            timer.Tick += (a, b) => this.Invalidate();
            timer.Start();

          
        }

        private void Draw_Scene(object? sender, PaintEventArgs e)
        {

            move_circle();

            SolidBrush brush = new(Color.Gray);
            SolidBrush brush2 = new(Color.Firebrick);
            Pen pen = new(new SolidBrush(Color.Black));

            e.Graphics.DrawRectangle(pen, 10, 10, 510, 510);
            e.Graphics.FillRectangle(brush, 10, 10, 510, 510);
            e.Graphics.FillEllipse(brush2, new Rectangle((int)CircleCenter.X, (int)CircleCenter.Y, 30, 30));
        }

        private void move_circle()
        {
           
            
            if(CircleCenter.X > 500)   { direction.X = -(new Random().NextInt64() % 5 + 3);
                
            }
            if(CircleCenter.Y > 500)   { direction.Y = -(new Random().NextInt64() % 5 + 3);
              
            }
            if (CircleCenter.X < 0)
            {
                direction.X = (new Random().NextInt64() % 5 + 3);

            }
            if (CircleCenter.Y < 0)
            {
                direction.Y = (new Random().NextInt64() % 5 + 3);

            }

            CircleCenter += direction;
            // need if for both circlecentre.x and circlecenter.y <0 do +ve random
            label1.Text = "The X value is: " + CircleCenter.X.ToString() + "The Y value is: "+ CircleCenter.Y.ToString();
        }
    }
}