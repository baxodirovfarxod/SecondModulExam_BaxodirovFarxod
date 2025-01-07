using SeconModulExam.Service.DTOs;

namespace SeconModulExam.Service.Services;
public interface IMusicServices
{
    Guid AddMusic(MusicCreatDTO music);
    MusicDTO GetMusicById(Guid id);
    void DeleteMusic(Guid id);
    void UpdateMusic(MusicDTO music);
    List<MusicDTO> GetAll();
    List<MusicDTO> GetAllMusicByAuthorName(string name);
    MusicDTO GetMostLikedMusic();
    MusicDTO GetMusicByName(string name);
    List<MusicDTO> GetAllMusicAboveSize(double minSize);
    List<MusicDTO> GetAllMusicBelowSize(double maxSize);
    List<MusicDTO> GetTopMostLikedMusic(int count);
    List<MusicDTO> GetMusicByDescriptionKeyword(string keyword);
    List<MusicDTO> GetMusicWithLikesInRange(int minLikes, int maxLikes);
    List<string> GetAllUniqueAuthors();
    double GetTotalMusicSizeByAuthor(string authorName);
}