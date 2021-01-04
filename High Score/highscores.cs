using System;

class highscores : IComparable<highscores>
	{
	public int Score { get; set;}
	public string Name { get; set;}
	public int ID { get; set;}

	public highscores(int id,string name, int score){
		Score = score;
		Name = name;
		ID = id;
	}
	public int CompareTo(highscores other) // Vai comparar o score mais alto e ordenar de acordo.
	{
		if (other.Score < Score) {
			return -1;
		} else
        {
            return other.Score > this.Score ? 1 : 0;
        }
    }
}

