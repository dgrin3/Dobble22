using Web22.Constants;

namespace Web22.Models
{
    public class DobbleCard
    {
        public int[] Pictures { get; set; } //= new int[DobbleConstants.GameDimension + 1];
        public int Counter { get; set; } = 0;
        public void AddPicture(int picture)
        {
            Pictures[Counter] = picture;
            Counter++;
        }
        public void RemovePicture()
        {
            Counter--;
            Pictures[Counter] = 0;
        }
        public bool HaveSamePicture(DobbleCard card)
        {
            bool haveSamePicture = false;
            for (int i = 0; i < card.Pictures.Length; i++)
            {
                if (HavePicture(card.Pictures[i]))
                {
                    haveSamePicture = true;
                    break;
                }
            }
            return haveSamePicture;
        }

        public bool HavePicture(int picture)
        {
            bool havePicture = false;
            for (int i = 0; i < Pictures.Length; i++)
            {
                if (Pictures[i] == picture)
                {
                    havePicture = true;
                    break;
                }
            }
            return havePicture;
        }
    }
}
