using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.IO;


namespace mDrive
{
    public class Game : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        mDraw mydraw;

        private float Speed;
        private int Score;
        private int Life;
        private double mNextHazardAppearsIn, mNextMoneyAppearsIn;
        private Random mRandom = new Random();
        private static readonly int[] vals = new int[3] {1, 2, 3};

        private Car player;
        private Road road;
        
        private List<Money> money = new List<Money>();
        private List<Hazard> hazard = new List<Hazard>();

        private Texture2D mRoad;
        private Texture2D mBack;
        private Texture2D mKu;
        private Texture2D mLife;
        private Texture2D[] mMoney,mHazard,mCars,mmCar;

        private Texture2D[] mLeftRight = new Texture2D[2];
        private Texture2D[] mLeftRightPressed = new Texture2D[2];
        private Texture2D[] mOK = new Texture2D[2];
        private Texture2D[] mBACK = new Texture2D[2];
        private Texture2D mOver;
        private Texture2D mMenu;
        private Texture2D mLock;
        private Texture2D mMouse;
        private Texture2D mAbout;
        private Texture2D mMenuListGrey;
        private Texture2D mSpeedDisplay;
        private Texture2D mlifeScoreForm;
        private Texture2D mRunBack;
        private Texture2D mFire;
        private Texture2D mSpeedPoint;
        private Texture2D mShowNotEnoughMoney;
        private Texture2D mShowSureToBuy;
        private Texture2D[] mContinue = new Texture2D[2];
        private Texture2D[] mOverGarage = new Texture2D[2];
        private Texture2D[] mEXIT = new Texture2D[2];
        private Texture2D[] mNewGame = new Texture2D[2];
        private Texture2D[] mGarageStart = new Texture2D[2];
        private double distLeft, distRight;
        private int menuState = 1;
        private int aboutState = 1;
        private int menuMouseX, menuMouseY;
        private int picIndex = 7, prePicIndex;
        private int[] speedOfCar = { 2, 5, 4, 4, 3, 3, 3, 3 };
        private int[] maxSpeedOfCar = { 13, 150, 12, 12, 12, 11, 10, 10 };
        private int[] AccOfCar = { 3, 5, 2, 2, 4, 4, 4, 4 };
        private int[] lifeOfCar = { 4, 12, 8, 7, 6, 5, 7, 3 };
        private int[] isLockOfCars;
        private int[] priceOfCars = { 1200000, 1000000, 800000, 700000, 600000, 20000, 400000, 0 };
        int showNotEnoughMoney = -1, showSureToBuyIt = -1;

        private Song backSong;

        private int myMoney; //总钱数

        private FileStream mFile;

        private SpriteFont mFont, mFont2;

        private enum State
        {
            StartPage,
            Running,
            Over,
            Garage
        }

        private State mode, preMode;
        private KeyboardState mPreviousKeyboardState;
        private MouseState mPreviousMouseState;

        public Game()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferHeight = 720;
            graphics.PreferredBackBufferWidth = 1280;
        }

        protected override void Initialize()
        {
            mode = State.StartPage;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            mydraw = new mDraw(GraphicsDevice);

            mCars = new Texture2D[8];
            mCars[0] = Content.Load<Texture2D>("Images/Car0");
            mCars[1] = Content.Load<Texture2D>("Images/Car1");
            mCars[2] = Content.Load<Texture2D>("Images/Car2");
            mCars[3] = Content.Load<Texture2D>("Images/Car3");
            mCars[4] = Content.Load<Texture2D>("Images/Car4");
            mCars[5] = Content.Load<Texture2D>("Images/Car5");
            mCars[6] = Content.Load<Texture2D>("Images/Car6");
            mCars[7] = Content.Load<Texture2D>("Images/Car7");

            mmCar = new Texture2D[8];
            mmCar[0] = Content.Load<Texture2D>("Images/Car_0");
            mmCar[1] = Content.Load<Texture2D>("Images/Car_1");
            mmCar[2] = Content.Load<Texture2D>("Images/Car_2");
            mmCar[3] = Content.Load<Texture2D>("Images/Car_3");
            mmCar[4] = Content.Load<Texture2D>("Images/Car_4");
            mmCar[5] = Content.Load<Texture2D>("Images/Car_5");
            mmCar[6] = Content.Load<Texture2D>("Images/Car_6");
            mmCar[7] = Content.Load<Texture2D>("Images/Car_7");

            mRoad = Content.Load<Texture2D>("Images/Road");
            mBack = Content.Load<Texture2D>("Images/Bakcground");
            mOver = Content.Load<Texture2D>("Images/Over");
            mLife = Content.Load<Texture2D>("Images/Life");
            mKu = Content.Load<Texture2D>("Images/Ku");
            mSpeedDisplay = Content.Load<Texture2D>("Images/speedDisplay");
            mlifeScoreForm = Content.Load<Texture2D>("Images/lifeScoreForm");
            mRunBack = Content.Load<Texture2D>("Images/runningBackground");

            mMoney = new Texture2D[3];
            mMoney[0] = Content.Load<Texture2D>("Images/Cuprum");
            mMoney[1] = Content.Load<Texture2D>("Images/Silver");
            mMoney[2] = Content.Load<Texture2D>("Images/Gold");

            mHazard = new Texture2D[4];
            mHazard[0] = Content.Load<Texture2D>("Images/PoliceBlack");
            mHazard[1] = Content.Load<Texture2D>("Images/PoliceBlue");
            mHazard[2] = Content.Load<Texture2D>("Images/TaxiWhite");
            mHazard[3] = Content.Load<Texture2D>("Images/TaxiYellow");

            mAbout = Content.Load<Texture2D>("Images/about");
            mLeftRight[0] = Content.Load<Texture2D>("Images/mouseClickedLEFT");
            mLeftRightPressed[0] = Content.Load<Texture2D>("Images/mouseClickedLEFT_pressed");
            mLeftRightPressed[1] = Content.Load<Texture2D>("Images/mouseClickedRIGHT_pressed");
            mNewGame[0] = Content.Load<Texture2D>("Images/newGame_");
            mNewGame[1] = Content.Load<Texture2D>("Images/newGame_pressed");
            mGarageStart[0] = Content.Load<Texture2D>("Images/garageStart_");
            mGarageStart[1] = Content.Load<Texture2D>("Images/garageStart_pressed");
            mLeftRight[1] = Content.Load<Texture2D>("Images/mouseClickedRIGHT");
            mLock = Content.Load<Texture2D>("Images/isLock");
            mOK[0] = Content.Load<Texture2D>("Images/ok_noPressed");
            mOK[1] = Content.Load<Texture2D>("Images/ok_pressed");
            mBACK[0] = Content.Load<Texture2D>("Images/back_Pressed");
            mBACK[1] = Content.Load<Texture2D>("Images/back_pressed");
            mMouse = Content.Load<Texture2D>("Images/mouse");
            mMenu = Content.Load<Texture2D>("Images/mMenu");
            mMenuListGrey = Content.Load<Texture2D>("Images/menuListGrey");
            mContinue[0] = Content.Load<Texture2D>("Images/continue");
            mContinue[1] = Content.Load<Texture2D>("Images/continuePressed");
            mEXIT[0] = Content.Load<Texture2D>("Images/EXIT");
            mEXIT[1] = Content.Load<Texture2D>("Images/EXITpressed");
            mOverGarage[0] = Content.Load<Texture2D>("Images/overGarage");
            mOverGarage[1] = Content.Load<Texture2D>("Images/overGaragePressed");
            mFire = Content.Load<Texture2D>("Images/fireCar");
            mSpeedPoint = Content.Load<Texture2D>("Images/speedPoint");
            mShowSureToBuy = Content.Load<Texture2D>("Images/showSureToBuy");
            mShowNotEnoughMoney = Content.Load<Texture2D>("Images/showNotEnoughMoney");

            backSong = Content.Load<Song>("lstd");

            mFont = Content.Load<SpriteFont>("MyFont");
            mFont2 = Content.Load<SpriteFont>("MyFont2");

            isLockOfCars = new int[8];
            try
            {
                mFile = new FileStream("./data.dat", FileMode.Open);
                StreamReader sr = new StreamReader(mFile);
                string strLine = sr.ReadLine();
                myMoney = int.Parse(strLine);
                for (int i = 0; i < 8; i++)
                    isLockOfCars[i] = int.Parse(sr.ReadLine());
                sr.Close();
            }
            catch (Exception ex)
            {
                myMoney = 0;
                for (int i = 0; i < 8; i++)
                    isLockOfCars[i] = 1;
                isLockOfCars[7] = 0;
            }

        }
        protected override void UnloadContent()
        {}

        void StartGame()
        {
            road = new Road(new Vector2(0, 0),0,new Vector2(Item.WindowsSize.X, Item.WindowsSize.Y));
            player = new Car(new Vector2(300, 500), speedOfCar[picIndex], maxSpeedOfCar[picIndex], AccOfCar[picIndex], new Vector2((float)(mCars[picIndex].Width * 0.2), (float)(mCars[picIndex].Height * 0.2)));
            road.Add(mRoad);
            player.Add(mCars[picIndex]);
            hazard.Clear();
            money.Clear();
            mNextHazardAppearsIn = 1.5;
            mNextMoneyAppearsIn = 3;
            Life = lifeOfCar[picIndex];
            MediaPlayer.Play(backSong);
            Score = 0;
            mode = State.Running;
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState aCurrentKeyboardState = Keyboard.GetState();
            MouseState aCurrentMouseState = Mouse.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            switch (mode)
            {
                case State.Over:
                    Save();
                    break;
                case State.StartPage:
                    break;
                case State.Running:
                    {
                        if (aCurrentKeyboardState.IsKeyDown(Keys.Left) == true && mPreviousKeyboardState.IsKeyDown(Keys.Left) == false)
                            player.Move(-1, 0);
                        if (aCurrentKeyboardState.IsKeyDown(Keys.Right) == true && mPreviousKeyboardState.IsKeyDown(Keys.Right) == false)
                            player.Move(1, 0);
                        if (aCurrentKeyboardState.IsKeyDown(Keys.Up) == true && mPreviousKeyboardState.IsKeyDown(Keys.Up) == false)
                            player.Higher();
                        if (aCurrentKeyboardState.IsKeyDown(Keys.Down) == true && mPreviousKeyboardState.IsKeyDown(Keys.Down) == false)
                            player.Lower();
                        Speed = player.Move();
                        road.Move(0, Speed);
                        Boolean changed = true;
                        while (changed)
                        {
                            changed = false;
                            foreach (Money amoney in money)
                            {
                                if (Crash(player, amoney) || Crash(amoney, player) || amoney.Move(0, Speed))
                                {
                                    if (Crash(player, amoney) || Crash(amoney, player))
                                        Score += (int)(player.Speed) * amoney.Value * 3;
                                    money.Remove(amoney);
                                    changed = true;
                                    break;
                                }
                            }
                        }
                        changed = true;
                        while (changed)
                        {
                            changed = false;
                            foreach (Hazard acar in hazard)
                            {
                                if (Crash(player, acar)||Crash(acar,player))
                                {
                                    if (Life == 1)
                                    {
                                        myMoney += Score;
                                        mode = State.Over;
                                    }
                                    else
                                    {
                                        Life--;
                                        hazard.Remove(acar);
                                        changed = true;
                                    }
                                    break;
                                }
                                if (acar.Move(0, Speed))
                                {
                                    hazard.Remove(acar);
                                    changed = true;
                                    break;
                                }

                            }
                            if (mode == State.Over)
                                break;
                        }
                        updateM(gameTime);
                        break;
                    }
                case State.Garage:
                    if (aCurrentKeyboardState.IsKeyDown(Keys.Right) == true && picIndex != 7 && mPreviousKeyboardState.IsKeyDown(Keys.Right)==false){
                        picIndex += 1;
                    }
                    if (aCurrentKeyboardState.IsKeyDown(Keys.Left) == true && picIndex != 0 && mPreviousKeyboardState.IsKeyDown(Keys.Left) == false){
                        picIndex -= 1;
                    }

                    distLeft = Math.Sqrt(Math.Pow(Mouse.GetState().X - 87, 2) + Math.Pow(Mouse.GetState().Y - 347, 2));
                    if (aCurrentMouseState.LeftButton == ButtonState.Released && distLeft < 69.5 && picIndex != 0 && mPreviousMouseState.LeftButton == ButtonState.Pressed){
                        picIndex -= 1;
                    }

                    distRight = Math.Sqrt(Math.Pow(Mouse.GetState().X - 1192, 2) + Math.Pow(Mouse.GetState().Y - 347, 2));
                    if (aCurrentMouseState.LeftButton == ButtonState.Released && distRight < 69.5 && 
                        picIndex != 7 && mPreviousMouseState.LeftButton == ButtonState.Pressed){
                        picIndex += 1;
                    }

                    if(aCurrentMouseState.RightButton == ButtonState.Released && mPreviousMouseState.RightButton == ButtonState.Pressed 
                  ||(((aCurrentMouseState.RightButton == ButtonState.Released && mPreviousMouseState.RightButton == ButtonState.Pressed) 
                   || (aCurrentMouseState.LeftButton==ButtonState.Released && mPreviousMouseState.LeftButton == ButtonState.Pressed))&& menuState==-1)){
                        menuMouseX = Mouse.GetState().X;
                        menuMouseY = Mouse.GetState().Y;
                        menuState = -menuState;
                    }
                    if (menuState == -1 && Mouse.GetState().X > menuMouseX && Mouse.GetState().X < menuMouseX + 133 && Mouse.GetState().Y > menuMouseY && Mouse.GetState().Y < menuMouseY + 39.5 && Mouse.GetState().LeftButton == ButtonState.Pressed)
                    {
                        menuState = 1;
                        if (myMoney < priceOfCars[picIndex] && isLockOfCars[picIndex] == 1)
                        {
                            showNotEnoughMoney = 1;
                        }
                        if (myMoney > priceOfCars[picIndex] && isLockOfCars[picIndex] == 1)
                        {
                            showSureToBuyIt = 1;
                        }
                    }
                    if (showNotEnoughMoney == 1)
                    {
                        if (aCurrentMouseState.LeftButton == ButtonState.Released && mPreviousMouseState.LeftButton == ButtonState.Pressed && 
                            aCurrentMouseState.X > 442 && aCurrentMouseState.X < 529 && aCurrentMouseState.Y > 419 && aCurrentMouseState.Y < 452)
                        {
                            showNotEnoughMoney = -1;
                        }
                        if (aCurrentMouseState.LeftButton == ButtonState.Released && mPreviousMouseState.LeftButton == ButtonState.Pressed && 
                            aCurrentMouseState.X > 743 && aCurrentMouseState.X < 838 && aCurrentMouseState.Y > 419 && aCurrentMouseState.Y < 452)
                        {
                            showNotEnoughMoney = -1;
                            picIndex = prePicIndex;
                            StartGame();
                            mode = State.Running;
                        }
                    }
                    if (showSureToBuyIt == 1)
                    {
                        if(aCurrentMouseState.LeftButton == ButtonState.Released && mPreviousMouseState.LeftButton == ButtonState.Pressed && 
                           aCurrentMouseState.X > 462 && aCurrentMouseState.X < 531 && aCurrentMouseState.Y > 390 && aCurrentMouseState.Y < 423)
                        {
                            showSureToBuyIt = -1;
                        }
                        if(aCurrentMouseState.LeftButton == ButtonState.Released && mPreviousMouseState.LeftButton == ButtonState.Pressed && 
                            aCurrentMouseState.X > 734 && aCurrentMouseState.X < 798 && aCurrentMouseState.Y > 390 && aCurrentMouseState.Y < 423)
                        {
                            isLockOfCars[picIndex] = 0;
                            myMoney -= priceOfCars[picIndex];
                            showSureToBuyIt = -1;
                            Save();
                        }
                    }
                    if (menuState == -1 && Mouse.GetState().X > menuMouseX && Mouse.GetState().X < menuMouseX + 133 && Mouse.GetState().Y > menuMouseY + 39.5 && 
                        Mouse.GetState().Y < menuMouseY + 79 && Mouse.GetState().LeftButton == ButtonState.Pressed)
                    {
                        mode = State.StartPage;
                        menuState = 1;
                    }
                    if (menuState == -1 && Mouse.GetState().X > menuMouseX && Mouse.GetState().X < menuMouseX + 133 
                        && Mouse.GetState().Y > menuMouseY + 79 + 39.5 && Mouse.GetState().Y < menuMouseY + 79 * 2 && Mouse.GetState().LeftButton == ButtonState.Pressed){
                        this.Exit();
                    }
                    if (menuState == -1 && Mouse.GetState().X > menuMouseX && Mouse.GetState().X < menuMouseX + 133
                     && Mouse.GetState().Y > menuMouseY + 79 && Mouse.GetState().Y < menuMouseY + 79 + 39.5 
                     && aCurrentMouseState.LeftButton == ButtonState.Pressed && mPreviousMouseState.LeftButton ==ButtonState.Released
                     ||(aboutState==-1 && aCurrentMouseState.LeftButton==ButtonState.Pressed && mPreviousMouseState.LeftButton==ButtonState.Released)){
                        menuState = 1;
                        aboutState = -aboutState;
                    }
                    mPreviousKeyboardState = aCurrentKeyboardState;
                    mPreviousMouseState = aCurrentMouseState;
                break;
                default:
                break;
            }
            base.Update(gameTime);
        }

        private void Save()
        {
            try
            {
                mFile = new FileStream("D:\\data.dat", FileMode.OpenOrCreate);
                StreamWriter sw = new StreamWriter(mFile);
                sw.WriteLine(myMoney.ToString());
                for (int i = 0; i < 8; i++)
                    sw.WriteLine(isLockOfCars[i].ToString());
                sw.Close();
            }
            catch (IOException ex) { }
        }

        private Boolean inItem(Item a, float x, float y)
        {
            if (x < a.Position.X || x > a.Position.X + a.Size.X)
                return false;
            if (y < a.Position.Y || y > a.Position.Y + a.Size.Y)
                return false;
            return true;
        }

        private Boolean Crash(Item a, Item b)
        {
            if(inItem(a,b.Position.X,b.Position.Y)) return true;
            if(inItem(a,b.Position.X + b.Size.X,b.Position.Y)) return true;
            if(inItem(a,b.Position.X,b.Position.Y + b.Size.Y)) return true;
            if(inItem(a,b.Position.X + b.Size.X,b.Position.Y + b.Size.Y)) return true;
            return false;
        }

        private void updateM(GameTime thisGameTime)
        {
            mNextMoneyAppearsIn -= thisGameTime.ElapsedGameTime.TotalSeconds;
            mNextHazardAppearsIn -= thisGameTime.ElapsedGameTime.TotalSeconds;
            if (mNextMoneyAppearsIn < 0)
            {
                int choose = (int)mRandom.Next(3);
                Money m = new Money(new Vector2(mRandom.Next((int)Item.WindowsSize.X - (int)(mMoney[choose].Width * 0.2)), -(int)(mMoney[choose].Height * 0.2)), 0, new Vector2(80, 80), vals[choose]);
                m.Add(mMoney[choose]);
                money.Add(m);
                mNextMoneyAppearsIn = (double)mRandom.Next(2, 5);
            }
            if (mNextHazardAppearsIn < 0)
            {
                int choose = (int)mRandom.Next(4);
                Hazard n = new Hazard(new Vector2(mRandom.Next((int)Item.WindowsSize.X - (int)(mHazard[choose].Width * 0.2)), -(float)(mHazard[choose].Height * 0.2)), player.Speed - 2, new Vector2((float)(mHazard[choose].Width * 0.2), (float)(mHazard[choose].Height * 0.2)));
                n.Add(mHazard[choose]);
                hazard.Add(n);
                mNextHazardAppearsIn = (double)mRandom.Next(2, 5);
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Gray);

            mydraw.Begin();
            switch(mode) {
                case State.Running:
                    int lifeHeight = 300;
                    int iTemp;
                    String disSpeed, disScore;
                    mydraw.Draw(mRunBack, new Rectangle(960, 0, 360, 720) ,Color.White);
                    mydraw.Draw(road);
                    mydraw.Draw(player);
                    if(Keyboard.GetState().IsKeyDown(Keys.Up)==true && player.Speed != player.Maxs) mydraw.Draw(mFire, new Rectangle((int)(player.Position.X+(player.Size.X)/16), (int)(player.Size.Y+player.Position.Y), 65, 56), Color.White);
                    mydraw.Draw(mlifeScoreForm, new Rectangle(976, 8, 300, 403), Color.White);
                    foreach (Money amoney in money)
                    {
                        mydraw.Draw(amoney);
                    }
                    foreach (Hazard ahazard in hazard)
                    {
                        mydraw.Draw(ahazard);
                    }
                    for (int i = 0; i < Life; i++)
                    {
                        iTemp = i%6;
                        if (i!=0 && i%6 == 0) lifeHeight += (int)((mLife.Height) * 0.09);
                        mydraw.Draw(mLife, new Rectangle((int)(iTemp * mLife.Width * 0.08) + 1000, lifeHeight, (int)(mLife.Width * 0.08), (int)(mLife.Height * 0.08)), Color.White);
                    }
                    disSpeed = ((int)(player.Speed*10)).ToString();
                    disScore = Score.ToString();

                    mydraw.DrawString(mFont, disSpeed, new Vector2(1102, 560), Color.White);
                    mydraw.Draw(mSpeedDisplay, new Rectangle(987, 453, 267, 257), Color.White);

                    mydraw.Draw(mSpeedPoint, new Rectangle((int)(1113-Math.Sin(20*player.Speed/180*Math.PI)*70), (int)(568+Math.Cos(20*player.Speed/180*Math.PI)*70), 27, 27), Color.White);

                    mydraw.DrawString(mFont2, disScore,  new Vector2(1050, 110), Color.White);
                    
                    break;
                case State.StartPage:
                    MouseState mouse = Mouse.GetState();
                    mydraw.Draw(mBack, new Rectangle(0, 0, 1280,720), Color.White);
                    mydraw.Draw(mNewGame[0], new Rectangle(1052, 564, 180, 52), Color.White);
                    mydraw.Draw(mGarageStart[0], new Rectangle(1052, 634, 180, 52), Color.White);
                    mydraw.Draw(mEXIT[0], new Rectangle(1, 1, 209, 60), Color.White);
                    if(mouse.LeftButton==ButtonState.Pressed && mouse.X>1052 && mouse.X<1232 && mouse.Y>564 && mouse.Y<616)
                    {
                        mydraw.Draw(mNewGame[1], new Rectangle(1052, 564, 180, 52), Color.White);
                    }
                    if (mouse.LeftButton == ButtonState.Released && mPreviousMouseState.LeftButton == ButtonState.Pressed
                     && mouse.X > 1052 && mouse.X < 1232 && mouse.Y > 564 && mouse.Y < 616) { StartGame(); mode = State.Running; }

                    if(mouse.LeftButton == ButtonState.Pressed && mouse.X > 1052 && mouse.X < 1232 && mouse.Y > 634 && mouse.Y < 686)
                    {
                        mydraw.Draw(mGarageStart[1], new Rectangle(1052, 634, 180, 52), Color.White);
                    }
                    if (mouse.LeftButton == ButtonState.Released && mPreviousMouseState.LeftButton == ButtonState.Pressed
                     && mouse.X > 1052 && mouse.X < 1232 && mouse.Y > 634 && mouse.Y < 686)
                    {
                        preMode = mode;
                        prePicIndex = picIndex;
                        mode = State.Garage;
                    }
                    
                    if(mouse.LeftButton==ButtonState.Pressed && mouse.X>1 && mouse.X<210 && mouse.Y>1 && mouse.Y<61)
                    {
                        mydraw.Draw(mEXIT[1], new Rectangle(1, 1, 209, 60), Color.White);
                    }
                    if (mouse.LeftButton == ButtonState.Released && mPreviousMouseState.LeftButton == ButtonState.Pressed
                     && mouse.X > 1 && mouse.X < 210 && mouse.Y > 1 && mouse.Y < 61) { this.Exit(); }

                    mPreviousMouseState = mouse;
                    break;
                case State.Over:
                    MouseState mouse2 = Mouse.GetState();
                    mydraw.Draw(mOver, new Rectangle(0, 0, 1280,720), Color.White);
                    mydraw.Draw(mContinue[0],new Rectangle(77, 245, 209, 60), Color.White);
                    mydraw.Draw(mOverGarage[0],new Rectangle(77, 332, 209, 60), Color.White);
                    mydraw.Draw(mEXIT[0], new Rectangle(77, 419, 209, 60), Color.White);

                    if(mouse2.LeftButton==ButtonState.Pressed && mouse2.X>77 && mouse2.X<279 && mouse2.Y>245 && mouse2.Y<305)
                    {
                        mydraw.Draw(mContinue[1], new Rectangle(77, 245, 209, 60), Color.White);
                    }
                    if (mouse2.LeftButton == ButtonState.Released && mPreviousMouseState.LeftButton == ButtonState.Pressed
                     && mouse2.X > 77 && mouse2.X < 279 && mouse2.Y > 245 && mouse2.Y < 305) { StartGame(); mode = State.Running; }

                    if(mouse2.LeftButton==ButtonState.Pressed && mouse2.X>77 && mouse2.X<279 && mouse2.Y>332 && mouse2.Y<392)
                    {
                        mydraw.Draw(mOverGarage[1], new Rectangle(77, 332, 209, 60), Color.White);
                    }
                    if (mouse2.LeftButton == ButtonState.Released && mPreviousMouseState.LeftButton == ButtonState.Pressed
                     && mouse2.X > 77 && mouse2.X < 279 && mouse2.Y > 332 && mouse2.Y < 392) { prePicIndex = picIndex; preMode = mode; mode = State.Garage; }

                    if(mouse2.LeftButton==ButtonState.Pressed && mouse2.X>77 && mouse2.X<279 && mouse2.Y>419 && mouse2.Y<479)
                    {
                        mydraw.Draw(mEXIT[1], new Rectangle(77, 419, 209, 60), Color.White);
                    }
                    if (mouse2.LeftButton == ButtonState.Released && mPreviousMouseState.LeftButton == ButtonState.Pressed
                     && mouse2.X > 77 && mouse2.X < 279 && mouse2.Y > 419 && mouse2.Y < 479) { this.Exit(); }
                    mPreviousMouseState = mouse2;
                    break;
                case State.Garage:
                    mydraw.Draw(mKu, new Rectangle(graphics.GraphicsDevice.Viewport.X, graphics.GraphicsDevice.Viewport.Y, graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height), Color.White);
                    mydraw.Draw(mmCar[picIndex], new Rectangle(graphics.GraphicsDevice.Viewport.X, graphics.GraphicsDevice.Viewport.Y, graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height), Color.White);
                    
                    if(isLockOfCars[picIndex]==1)
                    mydraw.Draw(mLock, new Rectangle(338, 360, 121, 190), Color.White);
            
                    //左右箭头是否画出↓
                    if (picIndex != 0){
                        mydraw.Draw(mLeftRight[0], new Rectangle(17, 277, 139, 139), Color.White);
                        if (Mouse.GetState().LeftButton == ButtonState.Pressed && distLeft < 69.5)
                            mydraw.Draw(mLeftRightPressed[0], new Rectangle(17, 277, 139, 139), Color.White);
                    }
                    if (picIndex != 7){
                        mydraw.Draw(mLeftRight[1], new Rectangle(1122, 277, 139, 139), Color.White);
                        if(Mouse.GetState().LeftButton == ButtonState.Pressed && distRight < 69.5)
                            mydraw.Draw(mLeftRightPressed[1], new Rectangle(1122, 277, 139, 139), Color.White);
                    }

                    mydraw.Draw(mOK[0], new Rectangle(1125, 672, 134, 36), Color.White);
                    if (isLockOfCars[picIndex]!=1&& Mouse.GetState().LeftButton == ButtonState.Pressed && Mouse.GetState().X > 1125 && Mouse.GetState().X < 1259 && Mouse.GetState().Y > 672 && Mouse.GetState().Y < 708)
                    {
                        mydraw.Draw(mOK[1], new Rectangle(1125, 672, 134, 36), Color.White);
                        StartGame();
                        mode = State.Running;
                    }
                    mydraw.Draw(mBACK[0], new Rectangle(20, 672, 134, 36), Color.White);
                    if (Mouse.GetState().LeftButton == ButtonState.Pressed && Mouse.GetState().X > 20 && Mouse.GetState().X < 154 && Mouse.GetState().Y > 672 && Mouse.GetState().Y < 708)
                    {
                        mydraw.Draw(mBACK[1], new Rectangle(20, 672, 134, 36), Color.White);
                        mode = preMode;
                    }

                    if (menuState == -1 && aboutState != -1){
                        mydraw.Draw(mMenu, new Rectangle(menuMouseX, menuMouseY, 133, 158), Color.White);
                        if (Mouse.GetState().X > menuMouseX && Mouse.GetState().X < menuMouseX + 133 && Mouse.GetState().Y > menuMouseY && Mouse.GetState().Y < menuMouseY + 39.5)
                            mydraw.Draw(mMenuListGrey, new Rectangle(menuMouseX +4, menuMouseY + 4, 126, 32), Color.White);
                        if(Mouse.GetState().X > menuMouseX && Mouse.GetState().X < menuMouseX + 133 && Mouse.GetState().Y > menuMouseY+39.5 && Mouse.GetState().Y < menuMouseY + 79)
                            mydraw.Draw(mMenuListGrey, new Rectangle(menuMouseX +4, menuMouseY + 43, 126, 32), Color.White);
                        if (Mouse.GetState().X > menuMouseX && Mouse.GetState().X < menuMouseX + 133 && Mouse.GetState().Y > menuMouseY+79 && Mouse.GetState().Y < menuMouseY + 39.5+79)
                            mydraw.Draw(mMenuListGrey, new Rectangle(menuMouseX +4, menuMouseY + 83, 126, 32), Color.White);
                        if (Mouse.GetState().X > menuMouseX && Mouse.GetState().X < menuMouseX + 133 && Mouse.GetState().Y > menuMouseY + 79+39.5 && Mouse.GetState().Y < menuMouseY + 79*2)
                            mydraw.Draw(mMenuListGrey, new Rectangle(menuMouseX +4, menuMouseY + 123, 126, 32), Color.White);
                    }
                    if (aboutState == -1)
                        mydraw.Draw(mAbout, new Rectangle(0, 0, 1280, 720), Color.White);
                    String mymoneyString = "Your Money:"+myMoney;
                    mymoneyString.ToString();
                    mydraw.DrawString(mFont, mymoneyString, new Vector2(4, 2), Color.Gray);
                    if (showSureToBuyIt == 1)
                    {
                        mydraw.Draw(mShowSureToBuy, new Rectangle(389, 263, 502, 194), Color.White);
                    }
                    if (showNotEnoughMoney == 1)
                    {
                        mydraw.Draw(mShowNotEnoughMoney, new Rectangle(398, 230, 484, 259), Color.White);
                    }
                    break;
                default:
                    break;
            }
            mydraw.Draw(mMouse, new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, 32, 47), Color.White);
            mydraw.End();
            base.Draw(gameTime);
        }
    }
}