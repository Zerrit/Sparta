namespace Zparta.ScoreLogic
{
    public interface IScoreChanger
    {
        public void IncreaseScore(int value);
        public void ChangeScore(int value);
    }
}