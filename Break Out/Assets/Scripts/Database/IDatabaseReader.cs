using System.Collections.Generic;

public interface IDatabaseReader : ISubject {

    public List<object> Leaders { get; }

    public void OnCreate();

    public void AddScoreToLeaders(string userId, int score);

}
