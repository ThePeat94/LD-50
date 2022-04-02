using Nidavellir.Utils;

namespace EventArgs
{
    public class GameStateChangedEventArgs
    {
        public GameStateChangedEventArgs(GameState newGameState)
        {
            this.NewGameState = newGameState;
        }

        public GameState NewGameState { get; }
    }
}