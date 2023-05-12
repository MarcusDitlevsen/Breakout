using System.Collections.Generic;
using DIKUArcade.Events;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System;

namespace Galaga {
    public class Player : IGameEventProcessor {
        private GameEventBus eventBus;
        public Entity entity {get; set;}
        private DynamicShape shape;
        private float moveLeft = 0.0f;
        private float moveRight = 0.0f;
        private const float MOVEMENT_SPEED = 0.02f;

    
        public Player(DynamicShape shape, IBaseImage image) {
            entity = new Entity(shape, image);
            this.shape = shape;

            eventBus = new GameEventBus();
            eventBus.InitializeEventBus(new List<GameEventType> {GameEventType.MovementEvent});
        }

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

        ///<summary>Returns the Player entity's position</summary>
        ///<returns>A Vec2F representing the player entity's position</returns>
        public Vec2F GetPosition() {
            return shape.Position;
        }

        public void Move() {
            // Checks if the X position is inside the bounds of the board.
            if ((shape.Direction.X >= 0.0f && shape.Position.X < 0.9f) // \n
                    || (shape.Direction.X <= 0.0f && shape.Position.X > 0.0f)) {
                shape.Move();
            }   
        }

        ///<summary>Moves the player to the left</summary>
        ///<param name = "val">A bool indicating that the player is indeed going to move left</param>
        private void SetMoveLeft(bool val) {
            if (val) {
                this.moveLeft -= MOVEMENT_SPEED;
            }
            else {
                this.moveLeft = 0.0f;
            }
            this.UpdateDirection();
        }

        // Moves right.
        private void SetMoveRight(bool val) {
            if (val) {
                this.moveRight += MOVEMENT_SPEED;
            }
            else {
                this.moveRight = 0.0f;
            }
            this.UpdateDirection();
        }

        // Updates the current direction the player is traveling in.
        private void UpdateDirection() {
            this.shape.Direction.X = this.moveLeft + this.moveRight;
        }

        // Renders the player entity.
        public void Render() {
            entity.RenderEntity();
        }
    }
}