using DIKUArcade.State;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Events;
using DIKUArcade.Input;
using Breakout;
using DIKUArcade.Math;
using DIKUArcade.Physics;
using BreakoutLevels;
using BreakoutEntities;
using DIKUArcade.Timers;
using Breakout.BreakoutUtils;

namespace BreakoutStates {
    public class GameRunning : IGameEventProcessor, IGameState {
        private static GameRunning instance = null;
        private GameEventBus eventBus;
        private Entity backGroundImage;
        private Text[] menuButtons;
        public int activeMenuButton {get; private set;}
        private int maxMenuButtons;
        private Player player;
        private Player playerOld;
        private float playerSpeedOld;
        private const int PLAYERSHOT_DAMAGE = 1;
        private LevelLoader loader;
        private EntityContainer<Block> blocks;
        private Random rand = new Random();
        private Score score;
        private Ball ball;
        private int ballOldDamage;
        private Lives lives;
        private string levelName;
        private Block[] breakableBlocks;
        public PowerUp powerUp;
        private EntityContainer<PowerUp> powerUps;
        private long time;
        private int blocksLeft;
        private IImageDatabase imageDatabase;
        
        ///<summary>Initializes (if not already) and returns the GameRunning state</summary>
        ///<returns>An initialized GameRunning state</returns>
        public static GameRunning GetInstance() {
            if (GameRunning.instance == null) {
                GameRunning.instance = new GameRunning("level1");
            }
            else {
                GameRunning.instance.ResetState();
            }
            return GameRunning.instance;
        }

        public GameRunning(string levelName) {
            this.levelName = levelName;
        }

        
        ///<summary>Iterates through the power-ups currently active to check collision</summary>
        private void IteratePowerUps() {
            powerUps.Iterate(power => {
                if (CollisionDetection.Aabb(power.Shape.AsDynamicShape(), player.Shape).Collision) {
                    power.DeleteEntity();

                    switch (power.CurrentPowerUp) {
                        case PowerUp.PowerUpType.ExtraLife:
                            lives.AddLife(1);
                            break;
                        
                        case PowerUp.PowerUpType.WidePlayer:
                            playerOld = player;

                            player = new Player(new DynamicShape(player.GetPosition(),
                                player.Shape.Extent * 2), player.Image);
                            BreakoutBus.GetBus().Subscribe(GameEventType.PlayerEvent, player);
                            BreakoutBus.GetBus().RegisterTimedEvent(new GameEvent{
                                EventType = GameEventType.TimedEvent,
                                Message = "STOP_POWERUP",
                                StringArg1 = "WidePlayer",
                                },
                                TimePeriod.NewSeconds(10));
                            break;
                        
                        case PowerUp.PowerUpType.PlayerSpeed:
                            playerSpeedOld = player.speed;

                            player.speed *= 2;

                            BreakoutBus.GetBus().RegisterTimedEvent(new GameEvent{
                                EventType = GameEventType.TimedEvent,
                                Message = "STOP_POWERUP",
                                StringArg1 = "PlayerSpeed",
                                },
                                TimePeriod.NewSeconds(10));
                            break;
                        
                        case PowerUp.PowerUpType.HardBall:
                            ballOldDamage = ball.Damage;

                            ball.Damage = int.MaxValue;

                            BreakoutBus.GetBus().RegisterTimedEvent(new GameEvent{
                                EventType = GameEventType.TimedEvent,
                                Message = "STOP_POWERUP",
                                StringArg1 = "HardBall",
                                },
                                TimePeriod.NewSeconds(10));
                            break;
                        
                        case PowerUp.PowerUpType.DoubleSpeed:
                            ball.DoubleSpeed();

                            BreakoutBus.GetBus().RegisterTimedEvent(new GameEvent{
                                EventType = GameEventType.TimedEvent,
                                Message = "STOP_POWERUP",
                                StringArg1 = "DoubleSpeed",
                                },
                                TimePeriod.NewSeconds(10));
                            break;

                            case PowerUp.PowerUpType.AcceleratingBall:
                            ball = BallFactory.GetAcceleratingBall(ball);

                            BreakoutBus.GetBus().RegisterTimedEvent(new GameEvent{
                                EventType = GameEventType.TimedEvent,
                                Message = "STOP_POWERUP",
                                StringArg1 = "AcceleratingBall",
                                },
                                TimePeriod.NewSeconds(10));
                            break;

                            case PowerUp.PowerUpType.InvertedBall:
                            ball.Invert();
                            break;

                            case PowerUp.PowerUpType.InvisibleBall:
                            ball.Invisible = true;
            
                            BreakoutBus.GetBus().RegisterTimedEvent(new GameEvent{
                                EventType = GameEventType.TimedEvent,
                                Message = "STOP_POWERUP",
                                StringArg1 = "InvisibleBall",
                                },
                                TimePeriod.NewSeconds(2));

                            break;
                    }
                }
            });
        }

        

        ///<summary>Iterates through all blocks to check for collision</summary>
        private void IterateBlocks() {
            List<Block> collidedBlocks = new List<Block>();
            
            blocks.Iterate(block => {
                CollisionData collisionVal = CollisionDetection.Aabb(ball.Shape.AsDynamicShape(),
                        block.Shape.AsDynamicShape());

                if (collisionVal.Collision) {
                    collidedBlocks.Add(block);
                }
            });

            // Meassure distance between ball and block, and collide with the closest
            int idx = -1;
            double mn = 1.0;
            for (int i = 0; i < collidedBlocks.Count; i++) {
                Block block = collidedBlocks[i];
                double dist = (block.Shape.Position.X-ball.Shape.Position.X)*(block.Shape.Position.X-ball.Shape.Position.X)+
                              (block.Shape.Position.X-ball.Shape.Position.X)*(block.Shape.Position.X-ball.Shape.Position.X);
                if (dist < mn) {
                    mn = dist;
                    idx = i;
                }
            }

            if (collidedBlocks.Count != 0) {
                Block bestBlock = collidedBlocks[idx];

                CollisionData collisionVal = CollisionDetection.Aabb(ball.Shape.AsDynamicShape(), bestBlock.Shape.AsDynamicShape());

                if (collisionVal.Collision) {
                    if (collisionVal.CollisionDir == CollisionDirection.CollisionDirLeft ||
                        collisionVal.CollisionDir == CollisionDirection.CollisionDirRight) {
                        
                        ball.ChangeDirection(player, "TRUE", "X");
                    }
                    else if (collisionVal.CollisionDir == CollisionDirection.CollisionDirUp ||
                        collisionVal.CollisionDir == CollisionDirection.CollisionDirDown) {
                        
                        ball.ChangeDirection(player, "TRUE", "Y");
                    }
                    else if (collisionVal.CollisionDir == CollisionDirection.CollisionDirUnchecked) {
                        throw new ArgumentException("Collision direction invalid");
                    }

                    bestBlock.TakeDamage(ball.Damage);

                    if (bestBlock.IsDeleted()) {
                        BreakoutBus.GetBus().RegisterEvent(new GameEvent {
                            EventType = GameEventType.StatusEvent,
                            Message = "ADD_POINT",
                            IntArg1 = bestBlock.value,
                        });

                        blocksLeft--;
                        if (bestBlock is PowerUpBlock) {

                                powerUps.AddEntity(PowerUp.RandomPowerUp(new DynamicShape(bestBlock.GetPosition(), new Vec2F(0.05f, 0.02f), new Vec2F(0.0f,- 0.006f)), imageDatabase)); 
                        }
                    }
                }
            }

            // If ball is out of bounds; lose life, delete ball, and add a new one
            if (ball.IsOutOfBounds) {
                lives.RemoveLife(1);
                ball.DeleteEntity();
                ball = BallFactory.GetNewBallFromPlayer(player);
                }
        }
        

        ///<summary>Handles calling either KeyPress or KeyRelease</summary>
        ///<param name = "action">The action that should be handled</param>
        ///<param name = "key">The pressed key</param>
        private void KeyHandler(KeyboardAction action, KeyboardKey key) {
            switch (action) {
                case KeyboardAction.KeyPress:
                    KeyPress(key);
                    break;

                case KeyboardAction.KeyRelease:
                    KeyRelease(key);
                    break;
            }
        }

        ///<summary>Registers a PlayerEvent when a key is pressed, or exists</summary>
        ///<param name = "key">The pressed key</param>
        public void KeyPress(KeyboardKey key) {
            switch (key) {
                case KeyboardKey.Left:
                    BreakoutBus.GetBus().RegisterEvent(new GameEvent {
                        EventType = GameEventType.PlayerEvent, Message = "MOVE_LEFT"});
                    player.Move();
                    break;
                
                case KeyboardKey.Right:
                    BreakoutBus.GetBus().RegisterEvent(new GameEvent {
                        EventType = GameEventType.PlayerEvent, Message = "MOVE_RIGHT"});
                    player.Move();
                    break;

                case KeyboardKey.Escape:
                    System.Environment.Exit(1);
                    break;

                case KeyboardKey.A:
                    Console.WriteLine($"{ball.GetType()}, {ball.Damage}, ({ball.SpeedX}, {ball.SpeedY})");
                    break;
                
                default:
                    break;
            }
        }

        ///<summary>Registers a PlayerEvent when a key is released</summary>
        ///<param name = "key">The released key</param>
        public void KeyRelease(KeyboardKey key) {
            switch (key) {
                case KeyboardKey.Left:
                    BreakoutBus.GetBus().RegisterEvent(new GameEvent {
                        EventType = GameEventType.PlayerEvent, Message = "MOVE_LEFT_FALSE"});
                    break;
                
                case KeyboardKey.Right:
                    BreakoutBus.GetBus().RegisterEvent(new GameEvent {
                        EventType = GameEventType.PlayerEvent, Message = "MOVE_RIGHT_FALSE"});
                    break;

                default:
                    break;
            }
        }

        ///<summary>Renders the objects used in the current GameRunning state</summary>
        public void RenderState() {
            BreakoutBus.GetBus().ProcessEventsSequentially();

            player.RenderEntity();
            blocks.RenderEntities();
            ball.RenderEntity();
            score.RenderScore();
            lives.RenderLives();
            powerUps.RenderEntities();
        }

        ///<summary>Initializes the current GameRunning state</summary>
        private void InitializeGameState() {
            imageDatabase = new ImageDatabase(Path.Combine("Assets", "Images"));
            player = new Player(
                new DynamicShape(new Vec2F(0.4f, 0.1f), new Vec2F(0.2f, 0.03f)),
                imageDatabase.GetImage("player.png"));

            loader = new LevelLoader();
            

            score = new Score(new Vec2F(0.0f, -0.15f), new Vec2F(0.2f, 0.2f));
            lives = new Lives(new Vec2F(0.85f, -0.15f), new Vec2F(0.2f, 0.2f));
            eventBus = new GameEventBus();
            

            loader.ReadLevel(Path.Combine("Breakout", "Assets", "Levels", levelName + ".txt"));

            blocks = loader.blocks;

            blocks.Iterate(block => {
                if (block.GetType() != typeof(UnbreakableBlock)) {
                    blocksLeft++;
                }
            });

            powerUps = new EntityContainer<PowerUp>();

            ball = BallFactory.GetNewBallFromPlayer(player);

            BreakoutBus.GetBus().Subscribe(GameEventType.PlayerEvent, player);
            BreakoutBus.GetBus().Subscribe(GameEventType.StatusEvent, score);
            BreakoutBus.GetBus().Subscribe(GameEventType.TimedEvent, this);


            StaticTimer.RestartTimer();
        }

        ///<summary>Resets the current GameRunning state back to it's original state</summary>
        public void ResetState() {
            InitializeGameState();
            lives.ResetLives();
            ball.ResetBall();
        }

        ///<summary>Updates the current GameState every frame</summary>
        public void UpdateState() {
            BreakoutBus.GetBus().ProcessEventsSequentially();
            time = StaticTimer.GetElapsedMilliseconds();
            IterateBlocks();

            blocks.RenderEntities();
            powerUps.RenderEntities();

            powerUps.Iterate(power => {
                power.Move();
            });

            IteratePowerUps();

            player.Move();
            score.RenderScore();
            lives.RenderLives();
            ball.ChangeDirection(player, "FALSE", "X");
            ball.Move();

            if (lives.lives == 0) {
                BreakoutBus.GetBus().RegisterEvent(
                    new GameEvent {
                        EventType = GameEventType.GameStateEvent,
                        Message = "CHANGE_STATE",
                        StringArg1 = "GAME_OVER"
                    }
                );
            }

            if (blocksLeft <= 0) {
                BreakoutBus.GetBus().RegisterEvent(
                    new GameEvent {
                        EventType = GameEventType.GameStateEvent,
                        Message = "CHANGE_STATE",
                        StringArg1 = "GAME_RUNNING2"
                    }
                );
            }
        }

        ///<summary>Processes a given game event</summary>
        ///<param name = "gameEvent">The game event to process<param>
        public void ProcessEvent(GameEvent gameEvent) {
            if (gameEvent.EventType == GameEventType.TimedEvent
                && gameEvent.Message == "STOP_POWERUP") {
                    switch (gameEvent.StringArg1) {
                        case "WidePlayer":
                            player = playerOld;
                            break;

                        case "PlayerSpeed":
                            player.speed = playerSpeedOld;
                            break;

                        case "HardBall":
                            ball = ball.RemovePowerUp(PowerUp.PowerUpType.HardBall);
                            break;

                        case "DoubleSpeed":
                            ball = ball.RemovePowerUp(PowerUp.PowerUpType.DoubleSpeed);
                            break;

                        case "AcceleratingBall":
                            ball = ball.RemovePowerUp(PowerUp.PowerUpType.AcceleratingBall);
                            break;

                        case "InvisibleBall":
                            ball = ball.RemovePowerUp(PowerUp.PowerUpType.InvisibleBall);
                            break;
                    }
                }
        }

        ///<summary>Processes a given keyboard event</summary>
        ///<param name = "action">The KeyboardAction to process<param>
        ///<param name = "key">The KeyboardKey to process<param>
        public void ProcessEvent(KeyboardAction action, KeyboardKey key) {
            switch (action) {
                case KeyboardAction.KeyPress:
                    KeyPress(key);
                    break;

                case KeyboardAction.KeyRelease:
                    KeyRelease(key);
                    break;
            }
        }

        ///<summary>Processes a given keyboard event</summary>
        ///<param name = "action">The KeyboardAction to process<param>
        ///<param name = "key">The KeyboardKey to process<param>
        public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
            switch (action) {
                case KeyboardAction.KeyPress:
                    KeyPress(key);
                    break;
                        
                case KeyboardAction.KeyRelease:
                    KeyRelease(key);
                    break;
            }
        }
    }
}