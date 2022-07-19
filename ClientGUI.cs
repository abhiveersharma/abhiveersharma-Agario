using AgarioModels;
using Communications;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System.Net;
using System.Numerics;
using System.Text.Json;

namespace ClientGUI
{
    /// <summary>
    /// Client GUI form for displaying the player graphics when a user wants to play the Agario knockoff game! Uses a networking object to connect to the server over TCP.
    /// Uses world, food, and players objects for abstracting and drawing the game. Sends and receives serialized/deserialized messages with the server to get information to draw.
    /// </summary>
    public partial class GameClientForm : Form
    {
        private Networking channel;
        private readonly ILogger<GameClientForm> _logger;
        private char terminationChar;

        string host;
        string serverIPAddress;
        const int port = 11000;

        Player player;
        World world;

        /// <summary>
        /// The size of the game view
        /// </summary>
        private const float windowWidth = 500.0F;
        private const float windowHeight = 500.0F;

        private const float windowCenterX = 255.0F;
        private const float windowCenterY = 255.0F;



        /// <summary>
        /// Initialize the client GUI form. Display the initial buttons and textboxes that the user needs to connect to the server.
        /// </summary>
        /// <param name="logger"></param>
        public GameClientForm(ILogger<GameClientForm> logger)
        {
            InitializeComponent();
            var timer = new System.Windows.Forms.Timer();
            timer.Interval = 30;
            timer.Tick += (a, b) => this.Invalidate();
            timer.Start();


            _logger = logger;

            host = Dns.GetHostName();
            serverIPAddress = "localhost"; //Default serverIPAddress to localhost
            if (serverTextbox.Text != "")
            {
                serverIPAddress = serverTextbox.Text;
            }

            terminationChar = '\n';
            channel = new Networking(logger, onConnect, onDisconnect, onMessage, terminationChar);
            player = new Player("",0,new Vector2(),0,0,0,0);
            world = new World(logger);

        }

        /// <summary>
        /// Callback when the networking object sends or receives a message to/from client and server. 
        /// Essentially used to check when server sends a message to update the list of food, players, etc 
        /// to display on the client GUI.
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="message"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void onMessage(Networking channel, string message)
        {

            // Get the created ID for the player from the server
            if (message.Length >= Protocols.CMD_Player_Object.Length && (message.Substring(0, Protocols.CMD_Player_Object.Length) == Protocols.CMD_Player_Object))
            {
                string strPlayersID = message[Protocols.CMD_Player_Object.Length..]!;
                long.TryParse(strPlayersID, out long longPlayersID);
                player.ID = longPlayersID;

                _logger.Log(LogLevel.Information, "Client received player ID mssg from server!");
            }

            // Food message check
            else if (message.Substring(0, Protocols.CMD_Food.Length) == Protocols.CMD_Food)
            {
                string food_items = message[Protocols.CMD_Food.Length..]!; //Get the rest of the message without the command prefix
                List<Food>? result = JsonSerializer.Deserialize<List<Food>>(food_items);
                lock(world)
                {
                    world.updateFoodList(result);
                }

                _logger.Log(LogLevel.Information, "Client received food mssg from server!");
            }

            // Eaten food message check
            else if (message.Substring(0, Protocols.CMD_Eaten_Food.Length) == Protocols.CMD_Eaten_Food)
            {
                string eaten_items = message[Protocols.CMD_Eaten_Food.Length..]!; 
                List<long>? result = JsonSerializer.Deserialize<List<long>>(eaten_items);
                lock (world)
                {
                    world.removeEatenFood(result);
                }

                _logger.Log(LogLevel.Information, "Client received eaten food mssg from server!");
            }

            //Dead players message check
            else if (message.StartsWith(Protocols.CMD_Dead_Players))
            {
                string eaten_players = message[Protocols.CMD_Dead_Players.Length..]!; 
                List<long>? result = JsonSerializer.Deserialize<List<long>>(eaten_players);
                lock (world)
                {
                    world.removeDeadPlayers(result);

                }

                _logger.Log(LogLevel.Information, "Client received dead players from server!");
            }

            //Current players update message check
            else if (message.Substring(0, Protocols.CMD_Update_Players.Length) == Protocols.CMD_Update_Players)
            {
                string update_players = message[Protocols.CMD_Update_Players.Length..]!; 
                List<Player>? result = JsonSerializer.Deserialize<List<Player>>(update_players);
                lock (world)
                {
                    world.updatePlayerList(result);
                    player.updateUserPlayer(result); //Attempt to update user/player using info sent from server.
                }

                foreach (Player p in result)
                {
                    if (p.ID == player.ID)
                    {
                        Invoke(() => { positionLabel.Text = $"Position: {p.X},{p.Y}"; });
                    }
                }

                _logger.Log(LogLevel.Information, "Client received update players mssg from server!");
            }

            // Heartbeat count message check
            else if (message.Substring(0, Protocols.CMD_HeartBeat.Length) == Protocols.CMD_HeartBeat)
            {
                string heartbeatCountStr = message[Protocols.CMD_HeartBeat.Length..]!;
                int.TryParse(heartbeatCountStr, out int currHeartbeatCount);
                this.heartbeatLabel.Text = $"Heartbeats: {currHeartbeatCount}";
                //lock (world)
                //{
                //    //this.Invalidate();
                //    // Draw here aka draw everytime we receive a heartbeat (about 30x per sec)???
                //}

                _logger.Log(LogLevel.Information, "Client received heartbeat mssg from server!");
            }

            //this.Paint += Draw_Scene;
        }

        /// <summary>
        /// Actions that should be taken when the client disconnects from the server.
        /// </summary>
        /// <param name="channel"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void onDisconnect(Networking channel)
        {
            _logger.Log(LogLevel.Information, "Client disconnected from the server!");
        }

        /// <summary>
        /// Actions that should be taken when the client connects to the server.
        /// </summary>
        /// <param name="channel"></param>
        private void onConnect(Networking channel)
        {
            _logger.Log(LogLevel.Information, "Client connected to server");
            Invoke(() => { 
                this.serverTextbox.Visible = false;
                this.serverLabel.Visible = false;
                this.playerNameTextbox.Visible = false;
                this.playerNameLabel.Visible = false;
                this.connectButton.Visible = false;
                this.connectButton.Enabled = false;
                this.serverTextbox.Enabled = false;
                this.serverLabel.Enabled = false;
                this.playerNameTextbox.Enabled = false;
                this.playerNameLabel.Enabled = false;

                channel.Send(String.Format(Protocols.CMD_Start_Game, player.Name));


                //this.Paint += Draw_Scene;
                //var timer = new System.Windows.Forms.Timer();
                //timer.Interval = 1000 / 30;  // 1000 milliseconds in a second divided by 30 frames per second
                //timer.Tick += (a, b) => this.Invalidate();
                //timer.Start();

            });
        }

        /// <summary>
        /// Use this button to connect to the server and start listening for messages.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void connectButton_Click(object sender, EventArgs e)
        {
            channel.Connect(host, port);
            player.Name = playerNameTextbox.Text;

            channel.ClientAwaitMessagesAsync(true);
        }

        /// <summary>
        /// Event handler for any keys getting pressed. Needed to make ProcessCmdKey work for keyboard shortcuts of Agar.io Client GUI
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClientGUI_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        /// <summary>
        /// processes key presses using keydata. Currently allows pressing enter to send a message.
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {

            if (keyData == Keys.Enter)
            {
                if(this.connectButton.Visible) //not connected to server, then allow pressing enter to push join server/game button
                {
                    channel.Connect(host, port);
                    player.Name = playerNameTextbox.Text;

                    channel.ClientAwaitMessagesAsync(true);
                }
            }

            if (keyData == Keys.Space && !this.connectButton.Enabled) // Spacebar allows the user to split their player/character and send their split body towards the mouse position.
            {
                PointF mouseScreenToClientPoint = PointToClient(Control.MousePosition);
                Vector2 mousePos = new Vector2(mouseScreenToClientPoint.X, mouseScreenToClientPoint.Y);
                channel.Send(String.Format(Protocols.CMD_Split, mousePos.X, mousePos.Y));

                _logger.Log(LogLevel.Information, "Client sent split cmd message to the server!");
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        /// <summary>
        /// Handles drawing the food, players, etc using a paint event handler.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Draw_Scene(object? sender, PaintEventArgs e)
        {
            Console.WriteLine("Drawing !!");
            lock (world)
            {
                if (!this.playerNameTextbox.Enabled) //Since we init Draw_Scene in GUI designer code, only draw the game portal after connecting and disabling textboxes.
                {
                    //Draw view window:
                    SolidBrush brush = new(Color.LightGray);
                    Pen pen = new(new SolidBrush(Color.Black));
                    e.Graphics.DrawRectangle(pen, 10, 10, 510, 510);
                    e.Graphics.FillRectangle(brush, 10, 10, 510, 510);
                }

                movePlayer();

                foodLabel.Text = $"Food: {world.FoodList.Count}";
                this.playerMassLabel.Text = $"Mass: {player.Mass}";

                var C = (float x1, float b1, float b2) =>
                {
                    return (x1 / b1) * b2;
                };

                int game_w = 5000;
                int game_h = 5000;
                int monitor_w = 500;
                int monitor_h = 500;

                SolidBrush foodBrush = new(Color.Gray);
                foreach (Food foodToDraw in world.FoodList.Values)
                {
                    int screen_x, screen_y, screen_w, screen_h;
                    float width = Math.Max(foodToDraw.Radius * 2, 5);

                    //convert_from_world_to_screen(foodToDraw.X, foodToDraw.Y, width, width, out screen_x, out screen_y, out screen_w, out screen_h);

                    float fX = C(foodToDraw.X, game_w, monitor_w);
                    float fY = C(foodToDraw.Y, game_h, monitor_h);
                    int sz = 2;
                    e.Graphics.FillEllipse(foodBrush, 
                        new Rectangle((int)fX, (int)fY, sz, sz));

                    //if (screen_x < 10 || screen_x > 510) // only draw the food if it is within the region of the world the user can see

                    //{
                    //    continue;
                    //}
                    //if (screen_y < 10 || screen_y > 510)
                    //{
                    //    continue;
                    //}

                    //foodBrush.Color = Color.FromArgb(foodToDraw.ARGBColor);

                    //e.Graphics.FillEllipse(foodBrush, new Rectangle((int)screen_x, (int)screen_y, (int)screen_w, (int)screen_h));
                    //e.Graphics.FillEllipse(foodBrush, new Rectangle((int)foodToDraw.X, (int)foodToDraw.Y, (int)foodToDraw.Radius * 2, (int)foodToDraw.Radius * 2));
                }

                //Draw the players:
                SolidBrush playerBrush = new(Color.Blue);
                foreach (Player playerToDraw in world.PlayerList.Values)
                {
                    int screen_x, screen_y, screen_w, screen_h;
                    float width = Math.Max(playerToDraw.Radius * 2, 5);

                    convert_from_world_to_screen(playerToDraw.X, playerToDraw.Y, width, width, out screen_x, out screen_y, out screen_w, out screen_h);
                    float pX = C(playerToDraw.X, game_w, monitor_w);
                    float pY = C(playerToDraw.Y, game_h, monitor_h);
                    int sz = 10;
                    e.Graphics.FillEllipse(playerBrush,
                        new Rectangle((int)pX, (int)pY, sz, sz));

                    //if(screen_x < 10 || screen_x > 510)
                    //{
                    //    continue;
                    //}
                    //if (screen_y < 10 || screen_y > 510)
                    //{
                    //    continue;
                    //}

                    //playerBrush.Color = Color.FromArgb(playerToDraw.ARGBColor);
                    //e.Graphics.FillEllipse(playerBrush, new Rectangle((int)screen_x, (int)screen_y, (int)screen_w, (int)screen_h));
                    //e.Graphics.FillEllipse(playerBrush, new Rectangle((int)playerToDraw.X, (int)playerToDraw.Y, (int)playerToDraw.Radius * 2, (int)playerToDraw.Radius * 2));

                }

            }
        }

        /// <summary>
        /// Tries to accept parameters of coordinates from the larger server game world and converts them to show on the smaller windows form screen.
        /// The portal is what the player/user views in the client... a 500x500 box but it also zooms out as the player grows.
        /// The world is the server generated game world that is 5000x5000.
        /// The screen is a subsection of the world that is around the player... also 500x500
        /// </summary>
        /// <param name="world_x"></param>
        /// <param name="world_y"></param>
        /// <param name="world_w"></param>
        /// <param name="world_h"></param>
        /// <param name="screen_x"></param>
        /// <param name="screen_y"></param>
        /// <param name="screen_w"></param>
        /// <param name="screen_h"></param>
        private void convert_from_world_to_screen(
        in float world_x, in float world_y, in float world_w, in float world_h,
        out int screen_x, out int screen_y, out int screen_w, out int screen_h)
        {

            float objXRelativeToCenter = world_x - player.X; //We can calculate the x position of an object relative to the player/center in the game world.
            float portal_width = 500;  //15*(player.Radius*2); // we want it to expand based on the mass of the player. This is 15 times the player's width (aka diameter since they are a circle).
            float screen_width = windowWidth;
            float objXRelativeToPortal = (screen_width * objXRelativeToCenter) / portal_width;
            screen_x = (int)(windowCenterX + objXRelativeToPortal);


            float objYRelativeToCenter = world_y - player.Y;
            float portal_height = 500;  //15*(player.Radius*2); // we want it to expand based on the mass of the player
            float screen_height = windowHeight;
            float objYRelativeToPortal = (screen_height * objYRelativeToCenter) / portal_height;
            screen_y = (int)(windowCenterY + objYRelativeToPortal);
            screen_w = 10;
            screen_h = 10;

        }

        private void convert_from_screen_to_world(int screen_x, int screen_y, float game_w, float game_h,
                                                  out float game_x, out float game_y)
        {
            int screen_w = 5000;
            int screen_h = 5000;
            game_x = (screen_x / screen_w) * game_w;
            game_y = (screen_y / screen_h) * game_h;
        }

        /// <summary>
        /// Moves the player by getting the mouse coordinates and moving towards it using vector2 addition and the center location of player circle object. 
        /// Sends a move request to the server so that the client and server stay in sync.
        /// </summary>
        private void movePlayer()
        {
            //var C = (float c1, float b1, float b2) =>
            //{
            //    return (c1 / b1) * b2;
            //};

            // To make sure the dead player doesn't move
            if (world.PlayerList.ContainsKey(player.ID))
            {
                var C = (float c1, float b1, float b2) =>
                {
                    return (c1 / b1) * b2;
                };
                PointF mouseScreenToClientPoint = PointToClient(Control.MousePosition);
                Vector2 mousePos = new Vector2(mouseScreenToClientPoint.X, mouseScreenToClientPoint.Y);
                float game_w = 500;
                float game_h = 500;
                Invoke(() => { mouseCoordsLabel.Text = $"Mouse Position: {mousePos.X},{mousePos.X}"; });

                //float moveX = (direction.X + player.X);
                //float moveY = (direction.Y + player.Y);
                int moveX = (int)C(mousePos.X, 500, 5000);
                int moveY = (int)C(mousePos.Y, 500, 5000);
                var posMsg = String.Format(Protocols.CMD_Move, moveX, moveY);
                channel.Send(posMsg);
                _logger.Log(LogLevel.Information, "Client sent movement request to server!");

            }
            else
            {
                return;
            }

            // 1. size of the player and food
            //2. zooming 
            //3. fine tuning the movement and clean up networking 


            //int r1 = 6;
            //int H1 = 5000;

            //int H2 = 500;
            //
            //C(6, 5000, 500);


            //PointF mouseScreenToClientPoint = PointToClient(Control.MousePosition);
            //Vector2 mousePos = new Vector2(mouseScreenToClientPoint.X, mouseScreenToClientPoint.Y);
            //float game_w = 500;
            //float game_h = 500;
            //convert_from_screen_to_world((int)mousePos.X, (int)mousePos.Y, game_w, game_h,
            //                                      out float moveX, out float moveY);
            //Vector2 viewVector = new Vector2((windowWidth + windowWidth / 2), (windowHeight + windowHeight / 2));
            //Vector2 direction = Vector2.Subtract(mousePos, viewVector);
            //convert_from_world_to_screen(player.X, player.Y, width, width, out screen_x, out screen_y, out screen_w, out screen_h);

            //Vector2 playerPos = new Vector2(player.X, player.Y);
            //float width = Math.Max(player.Radius * 2, 5);
            //Vector2 direction = Vector2.Subtract(mousePos, viewVector);

            //    Invoke(() => { mouseCoordsLabel.Text = $"Mouse Position: {mousePos.X},{mousePos.X}"; });

            //    //float moveX = (direction.X + player.X);
            //    //float moveY = (direction.Y + player.Y);
            //    int moveX = (int)C(mousePos.X, 500, 5000);
            //    int moveY = (int)C(mousePos.Y, 500, 5000);
            //    var posMsg = String.Format(Protocols.CMD_Move, moveX, moveY);
            //    channel.Send(posMsg);
            //    _logger.Log(LogLevel.Information, "Client sent movement request to server!");
            //}
        }

    }
}