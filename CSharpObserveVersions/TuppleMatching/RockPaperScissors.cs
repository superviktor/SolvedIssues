namespace CSharpObserveVersions.TuppleMatching
{
    public enum GameElement
    {

        Rock,
        Paper,
        Scissors
    }

    public class RockPaperScissors
    {
        public string GetWinner(GameElement first, GameElement second) =>
            (first, second) switch
            {
                (GameElement.Rock, GameElement.Paper) => GameElement.Paper.ToString(),
                (GameElement.Rock, GameElement.Scissors) => GameElement.Rock.ToString(),
                (GameElement.Paper, GameElement.Rock) => GameElement.Paper.ToString(),
                (GameElement.Paper, GameElement.Scissors) => GameElement.Scissors.ToString(),
                (GameElement.Scissors, GameElement.Rock) => GameElement.Rock.ToString(),
                (GameElement.Scissors, GameElement.Paper) => GameElement.Scissors.ToString(),
                _ => "Tie"
            };
    }
}
