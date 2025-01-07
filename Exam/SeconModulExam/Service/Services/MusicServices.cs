using SeconModulExam.DataAccess.Entities;
using SeconModulExam.Repository;
using SeconModulExam.Service.DTOs;

namespace SeconModulExam.Service.Services;
public class MusicServices : IMusicServices
{
    private readonly List<MusicDTO> musicList;
    private IMusicRepositories musicRepositories;
    public MusicServices()
    {
        musicRepositories = new MusicRepositories();
        musicList = GetAll();
    }
    public Guid AddMusic(MusicCreatDTO music)
    {
        var musicEntity = ConvertMusicDTOToEntity(music);
        musicRepositories.AddMusic(musicEntity);
        return musicEntity.Id;
    }
    public MusicDTO GetMusicById(Guid id)
    {
        var musicEntity = musicRepositories.GetMusicById(id);
        var musicDTO = ConvertEntityToDTO(musicEntity);
        return musicDTO;
    }
    public void DeleteMusic(Guid id)
    {
        musicRepositories.DeleteMusic(id);
    }
    public void UpdateMusic(MusicDTO music)
    {
        var musicEntity = ConvertMusicDTOToEntity(music);
        musicRepositories.UpdateMusic(musicEntity);
    }
    public List<MusicDTO> GetAll()
    {
        var musicList = new List<MusicDTO>();
        var musicEntityList = musicRepositories.GetAll();
        foreach (var music in musicEntityList)
        {
            musicList.Add(ConvertEntityToDTO(music));
        }

        return musicList;
    }
    public List<MusicDTO> GetAllMusicAboveSize(double minSize)
    {
        var musicListAboveSize = new List<MusicDTO>();
        foreach (var music in musicList)
        {
            if (music.MB > minSize)
            {
                musicListAboveSize.Add(music);
            }
        }
        return musicListAboveSize;
    }
    public List<MusicDTO> GetAllMusicBelowSize(double maxSize)
    {
        var musicListBelowSize = new List<MusicDTO>();
        foreach (var music in musicList)
        {
            if (music.MB < maxSize)
            {
                musicListBelowSize.Add(music);
            }
        }
        return musicListBelowSize;
    }
    public List<MusicDTO> GetAllMusicByAuthorName(string name)
    {
        var musicListByAuthorName = new List<MusicDTO>();
        foreach (var music in musicList)
        {
            if (music.AuthorName ==  name)
            {
                musicListByAuthorName.Add(music);
            }
        }
        return musicListByAuthorName;
    }
    public List<string> GetAllUniqueAuthors()
    {
        var uniqueAuthorsList = UniqueAuthors();
        var authors = new List<string>();
        foreach (var _authors in uniqueAuthorsList)
        {
            authors.Add(_authors.Name);
        }

        return authors;
    }
    public MusicDTO GetMostLikedMusic()
    {
        var mostLikedMusic = musicList[0];
        foreach (var music in musicList)
        {
            if (mostLikedMusic.QuentityLikes < music.QuentityLikes)
            {
                mostLikedMusic = music;
            }
        }

        return mostLikedMusic;
    }
    public List<MusicDTO> GetMusicByDescriptionKeyword(string keyword)
    {
        var musicListByDescription = new List<MusicDTO>();
        foreach (var music in musicList)
        {
            if (music.Description.Contains(keyword) is true)
            {
                musicListByDescription.Add(music);
            }
        }

        return musicListByDescription;
    }
    public MusicDTO GetMusicByName(string name)
    {
        foreach (var music in musicList)
        {
            if (music.Name == name)
            {
                return music;
            }
        }

        throw new Exception($"{name} ismli musiqa yo'q !");
    }
    public List<MusicDTO> GetMusicWithLikesInRange(int minLikes, int maxLikes)
    {
        var musicListtWithLikesInRange = new List<MusicDTO>();
        foreach (var music in musicList)
        {
            if (music.QuentityLikes > minLikes && music.QuentityLikes < maxLikes)
            {
                musicListtWithLikesInRange.Add(music);  
            }
        }

        return musicListtWithLikesInRange;
    }
    public List<MusicDTO> GetTopMostLikedMusic(int count)
    {
        var topLikedMusicList = new List<MusicDTO>();
        foreach (var music in musicList)
        {
            if (music.QuentityLikes == count)
            {
                topLikedMusicList.Add(music);
            }
        }

        return topLikedMusicList;
    }
    public double GetTotalMusicSizeByAuthor(string authorName)
    {
        var totalSize = 0d;
        foreach (var music in musicList)
        {
            if (music.AuthorName == authorName)
            {
                totalSize += music.MB;
            }
        }

        return totalSize;
    }
    private List<MusicDTO> UniqueAuthors()
    {
        var authorsList = new List<MusicDTO>();
        var count = 0;
        for (var i = 0; i < musicList.Count; i++)
        {
            count = 0;
            for (var j = i; j < musicList.Count; j++)
            {
                if (musicList[i].AuthorName == musicList[j].Name)
                {
                    count++;
                }
            }

            if (count == 1)
            {
                authorsList.Add(musicList[i]);
            }
        }

        return authorsList;
    }
    private Music ConvertMusicDTOToEntity(MusicDTO music)
    {
        return new Music
        {
            Id = music.Id,
            Name = music.Name,
            MB = music.MB,
            AuthorName = music.AuthorName,
            Description = music.Description,
            QuentityLikes = music.QuentityLikes,    
        };
    }
    private Music ConvertMusicDTOToEntity(MusicCreatDTO music)
    {
        return new Music
        {
            Id = Guid.NewGuid(),
            Name = music.Name,
            MB = music.MB,
            AuthorName = music.AuthorName,
            Description = music.Description,
            QuentityLikes = music.QuentityLikes,
        };
    }
    private MusicDTO ConvertEntityToDTO(Music music)
    {
        return new MusicDTO 
        {
            Id = music.Id,
            Name = music.Name,
            MB = music.MB,
            AuthorName = music.AuthorName,
            Description = music.Description,
            QuentityLikes = music.QuentityLikes
        };

    }
}
