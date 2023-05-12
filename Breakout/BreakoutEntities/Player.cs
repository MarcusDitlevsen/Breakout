using System.Collections.Generic;
using DIKUArcade.Events;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System;
using Breakout.BreakoutUtils;

namespace BreakoutEntities {
    public class Player : Entity, IGameEventProcessor {
        private GameEventBus eventBus;
        private DynamicShape shape;
        private float moveLeft = 0.0f;
        private float moveRight = 0.0f;
        public float speed = 0.02f;
        const string backupImage = "yellow_block.png";

        public Player(DynamicShape shape, IBaseImage image) : base(shape, image) {

            this.shape = shape;

            eventBus = new GameEventBus();
            eventBus.InitializeEventBus(new List<GameEventType> {GameEventType.MovementEvent});
        }

        ///<summary>Processes a given game event</summary>
        ///<param name = "gameEvent">The given game event to process</param>
        public void ProcessEvent(GameEvent gameEvent) {
            if (gameEvent.EventType == GameEventType.PlayerEvent) {
                switch (gameEvent.Message) {
                    case "MOVE_LEFT":
                        SetMoveLeft(true);
                        break;
                    case "MOVE_RIGHT":
                        SetMoveRight(true);
                        break;
                    case "MOVE_LEFT_FALSE":
                        SetMoveLeft(false);
                        break;
                    case "MOVE_RIGHT_FALSE":
                        SetMoveRight(false);
                        break;
                    default:
                        break;
                }
            }
        }




        ///<summary>Moves the player</summary>
        public void Move() {
            if ((shape.Direction.X >= 0.0f && shape.Position.X + shape.Extent.X < 1.0f) // \n
                    || (shape.Direction.X <= 0.0f && shape.Position.X > 0.0f)) {
                shape.Move();
            }   
        }

        ///<summary>Moves the player to the left</summary>
        ///<param name = "val">A bool indicating that the player is indeed going to move left</param>
        private void SetMoveLeft(bool val) {
            if (val) {
                moveLeft -= speed;
            }
            else {
                moveLeft = 0.0f;
            }
            UpdateDirection();
        }

        ///<summary>Moves the player to the right</summary>
        ///<param name = "val">A bool indicating that the player is indeed going to move right</param>
        private void SetMoveRight(bool val) {
            if (val) {
                moveRight += speed;
            }
            else {
                moveRight = 0.0f;
            }
            UpdateDirection();
        }

        public void ChangeSize(Player player, int size){
            
        }

        ///<summary>Updates the current direction the player is traveling in</summary>
        private void UpdateDirection() {
            shape.Direction.X = moveLeft + moveRight;
        }

        ///<summary>Returns the Player entity's position</summary>
        ///<returns>A Vec2F representing the player entity's position</returns>
        public Vec2F GetPosition() {
            return shape.Position;
        }
    }
}