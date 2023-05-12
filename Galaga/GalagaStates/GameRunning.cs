using DIKUArcade;
using DIKUArcade.State;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.GUI;
using DIKUArcade.Events;
using DIKUArcade.Input;
using System;
using Galaga;
using System.IO;
using DIKUArcade.Math;
using System.Security.Principal;
using System.Collections.Generic;
using DIKUArcade.Physics;
using GalagaStates;

namespace GalagaStates {
    public class GameRunning : IGameState {
        private static GameRunning instace = null;
        private GameEventBus eventBus;
        private Entity backGroundImage;
        private Text[] menuButtons;
        public int activeMenuButton {get; private set;}
        private int maxMenuButtons;
        private Player player;
        private EntityContainer<Enemy> enemies;
        private List<Image> enemyStridesGreen;
        private List<Image> enemyStrides;
        private AnimationContainer enemyExplosions;
        private List<Image> explosionStrides;
        private const int EXPLOSION_LENGTH_MS = 500;
        private EntityContainer<PlayerShot> playerShots;
        private IBaseImage playerShotImage;
        private const int PLAYERSHOT_DAMAGE = 1;
        private ISquadron squadWedge;
        private ISquadron squadReverseWedge;
        private ISquadron squadShieldWall;
        private IMovementStrategy moveStrat;
        private IMovementStrategy moveStratNoMove;
        private IMovementStrategy moveStratDown;
        private IMovementStrategy moveStratZigZagDown;
        private Score score;
        private Random rand = new Random();

        
        public static GameRunning GetInstance() {
            if (GameRunning.instace == null) {
                GameRunning.instace = new GameRunning();
                GameRunning.instace.InitializeGameState();
            }
            return GameRunning.instace;
        }

        ///<summary>Adds an explosion at a given position</summary>
        ///<param name = "position">Position to create explosion at</param>
        ///<param name = "extent">The size of the explosion</param>
        private void AddExplosion(Vec2F position, Vec2F extent) {
            StationaryShape statShape = new StationaryShape(position, extent);
            enemyExplosions.AddAnimation(statShape, EXPLOSION_LENGTH_MS,
                new ImageStride(EXPLOSION_LENGTH_MS/8, explosionStrides));
        }

        ///<summary>Iterates player's shots and checks for collision between enemies.</summary>
        private void IterateShots() {
            playerShots.Iterate(shot => {
                shot.Shape.Move();
                if (shot.Shape.Position.Y >= 1.0f) {
                    shot.DeleteEntity();
                }
                else {
                    enemies.Iterate(enemy => {
                        if (CollisionDetection.Aabb(shot.Shape.AsDynamicShape(), // \n
                                enemy.Shape).Collision) {
                            shot.DeleteEntity();
                            if (enemy.Hit(PLAYERSHOT_DAMAGE) == 1) {
                                AddExplosion(enemy.GetPosition(), new Vec2F(0.1f, 0.1f));
                                score.AddPoint();
                            }
                        }
                    });
                }
            });
        }

        ///<summary>Iterates through all enemies and spawns new ones if they're dead</summary>
        private void IterateEnemies() {            
            var moveStrats = new IMovementStrategy[] {moveStratDown,
                moveStratNoMove, moveStratZigZagDown};
            
            var squads = new ISquadron[] {squadWedge, squadReverseWedge,
                squadShieldWall};

            if (enemies.CountEntities() <= 0) {
                var newSquad = squads[rand.Next(squads.Length)];
                newSquad.CreateEnemies(enemyStrides, enemyStridesGreen);

                var newMoveStrat = new IMovementStrategy[3] {moveStratNoMove,
                    moveStratDown, moveStratZigZagDown};
                moveStrat = newMoveStrat[rand.Next(newMoveStrat.Length)];

                foreach (Enemy enemy in newSquad.Enemies) {
                    enemy.ChangeSpeed(enemy.GetSpeed() + 0.001f);
                }

                enemies = newSquad.Enemies;
            }
        }

        ///<summary>Checks if the game is over</summary>
        ///<returns>A boolean corresponding to if the game is over</returns>
        private bool CheckGameOver() {
            foreach (Enemy enemy in enemies) {
                if (enemy.GetPosition().Y < 0.0f) {
                    return true;
                }
            }
            
            return false;
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

        ///<summary>Registers a PlayerEvent when a key is pressed</summary>
        ///<param name = "key">The pressed key</param>
        public void KeyPress(KeyboardKey key) {
            switch (key) {
                case KeyboardKey.Left:
                    GalagaBus.GetBus().RegisterEvent(new GameEvent {
                        EventType = GameEventType.PlayerEvent, Message = "MOVE_LEFT"});
                    player.Move();
                    break;
                
                case KeyboardKey.Right:
                    GalagaBus.GetBus().RegisterEvent(new GameEvent {
                        EventType = GameEventType.PlayerEvent, Message = "MOVE_RIGHT"});
                    player.Move();
                    break;

                case KeyboardKey.Escape:
                    System.Environment.Exit(1);
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
                    GalagaBus.GetBus().RegisterEvent(new GameEvent {
                        EventType = GameEventType.PlayerEvent, Message = "MOVE_LEFT_FALSE"});
                    break;
                
                case KeyboardKey.Right:
                    GalagaBus.GetBus().RegisterEvent(new GameEvent {
                        EventType = GameEventType.PlayerEvent, Message = "MOVE_RIGHT_FALSE"});
                    break;
                
                case KeyboardKey.Space:
                    playerShots.AddEntity(new PlayerShot(player.GetPosition()
                        + new Vec2F(0.05f, 0.0f), playerShotImage));
                    break;

                default:
                    break;
            }
        }

        public void RenderState() {
            if (!CheckGameOver()) {
                player.Render();
                enemies.RenderEntities();
                playerShots.RenderEntities();
                enemyExplosions.RenderAnimations();
            }
            score.RenderScore();  
        }

        private void InitializeGameState() {
            enemyStrides = ImageStride.CreateStrides(4, Path.Combine("Assets", "Images",
                "BlueMonster.png"));
            enemyStridesGreen = ImageStride.CreateStrides(2, Path.Combine("Assets", "Images",
                "GreenMonster.png"));
            const int numEnemies = 8;

            enemyExplosions = new AnimationContainer(numEnemies);
            explosionStrides = ImageStride.CreateStrides(8,
                Path.Combine("Assets", "Images", "Explosion.png"));

            playerShots = new EntityContainer<PlayerShot>();
            playerShotImage = new Image(Path.Combine("Assets", "Images", "BulletRed2.png"));

            player = new Player(
                new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
                new Image(Path.Combine("Assets", "Images", "Player.png")));

            squadWedge = new Wedge();
            squadReverseWedge = new ReverseWedge();
            squadShieldWall = new ShieldWall();

            squadWedge.CreateEnemies(enemyStrides, enemyStridesGreen);
            squadReverseWedge.CreateEnemies(enemyStrides, enemyStridesGreen);
            squadShieldWall.CreateEnemies(enemyStrides, enemyStridesGreen);
            enemies = squadWedge.Enemies;

            moveStratNoMove = new NoMove();
            moveStratDown = new Down();
            moveStratZigZagDown = new ZigZagDown();

            score = new Score(new Vec2F(0.0f, -0.15f), new Vec2F(0.2f, 0.2f));
            eventBus = new GameEventBus();
            GalagaBus.GetBus().Subscribe(GameEventType.PlayerEvent, player);

            moveStrat = moveStratZigZagDown;
        }

        public void ResetState() {
        }

        public void UpdateState() {
            GalagaBus.GetBus().ProcessEventsSequentially();
            moveStrat.MoveEnemies(enemies);
            CheckGameOver();
            IterateShots();
            IterateEnemies();
            player.Move();
        }

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