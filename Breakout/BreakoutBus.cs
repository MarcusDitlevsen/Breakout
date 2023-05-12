using DIKUArcade.Events;
using System.Collections.Generic;

namespace Breakout {
    public static class BreakoutBus {
        private static GameEventBus eventBus;
        public static GameEventBus GetBus() {
            if (eventBus == null) {
                eventBus = new GameEventBus();
                // Just cram everything in there lmao
                eventBus.InitializeEventBus(new List<GameEventType> {
                    GameEventType.PlayerEvent, GameEventType.GraphicsEvent,
                    GameEventType.InputEvent, GameEventType.ControlEvent,
                    GameEventType.MovementEvent, GameEventType.GameStateEvent,
                    GameEventType.SoundEvent, GameEventType.StatusEvent,
                    GameEventType.TimedEvent, GameEventType.WindowEvent
                });

                return eventBus;
            }
            else {
                return eventBus;
            }
        }
    }
}