[System.Serializable]
public class UserGameData
{
    public int gold;            // 무료 재화
    public int bestScore;  // 일일 최고 점수

    public void Reset()
    {
        gold = 0;
        bestScore = 0;
    }
}

