using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Driver.Linq;
using System.Threading.Tasks;

internal static class CRUD
{
    const string uri = "mongodb://localhost:27017";
    const string db = "AdvDb";
    static MongoClient client = new MongoClient(uri);

    private static IMongoCollection<T> GetCollection<T>()
    {
        return client.GetDatabase(db).GetCollection<T>(typeof(T).Name + "s");
    }

    public static void CreateUser(User user)
    {
        GetCollection<User>().InsertOne(user);
    }
    public static Task<User> GetUserWithLoginAsync(string login)
    {
        return GetCollection<User>().AsQueryable().FirstOrDefaultAsync(x => x.Login == login);
    }

    public static void CreateRecord(Record record)
    {
        GetCollection<Record>().InsertOne(record);
    }

    public static List<Record> GetAllRecords(User user)
    {
        return GetCollection<Record>().AsQueryable().Where(x => x.Login == user.Login).ToList();
    }

    public static Record GetRecord(ObjectId Id)
    {
        return GetCollection<Record>().Find(x => x.Id == Id).FirstOrDefault();
    }
}