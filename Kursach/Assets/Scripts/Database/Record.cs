using MongoDB.Bson;

public class Record 
{
    public ObjectId Id { get; set; }
    public string Login { get; set; }

    public float Time;
}
