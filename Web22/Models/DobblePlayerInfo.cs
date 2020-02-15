namespace Web22.Models
{
    public class DobblePlayerInfo
    {
        public string Name { get; set; }
        public int Score { get; set; }

        public DobblePlayerInfo (string name, int score)
        {
            Name = name;
            Score = score;
        }
    }
}
