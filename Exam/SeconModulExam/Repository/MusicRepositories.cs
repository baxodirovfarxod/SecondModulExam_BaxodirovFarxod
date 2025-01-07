using SeconModulExam.DataAccess.Entities;
using System.Text.Json;

namespace SeconModulExam.Repository;
public class MusicRepositories : IMusicRepositories
{
    private readonly string path;
    private readonly List<Music> musicList;
    public MusicRepositories()
    {
        path = "../../../DataAccess/Data/Music.json";
        if (File.Exists(path) is false)
        {
            File.WriteAllText(path, "[]");
        }
        musicList = ReadMusicList();
    }
    public Guid AddMusic(Music music)
    {
        musicList.Add(music);
        SaveData();
        return music.Id;
    }
    public Music GetMusicById(Guid id)
    {
        foreach (var music in musicList)
        {
            if (music.Id == id)
            {
                return music;
            }
        }

        throw new Exception($"Bunday {id} lik musiqa mavjud emas !");
    }
    public void DeleteMusic(Guid id)
    {
        var musicFromDB = GetMusicById(id);
        musicList.Remove(musicFromDB);
        SaveData();
    }
    public void UpdateMusic(Music music)
    {
        var musicFromDB = GetMusicById(music.Id);
        var index = musicList.IndexOf(musicFromDB);
        musicList[index] = music;
        SaveData();
    }
    public List<Music> GetAll()
    {
        return musicList;
    }
    private List<Music> ReadMusicList()
    {
        var musicJson = File.ReadAllText(path);
        var music = JsonSerializer.Deserialize<List<Music>>(musicJson);
        return music;
    }
    private void SaveData()
    {
        var musicJson = JsonSerializer.Serialize(musicList);
        File.WriteAllText(path, musicJson);
    }
}
