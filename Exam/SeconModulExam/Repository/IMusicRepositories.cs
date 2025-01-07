using SeconModulExam.DataAccess.Entities;

namespace SeconModulExam.Repository;
public interface IMusicRepositories
{
    Guid AddMusic(Music music);
    Music GetMusicById(Guid id);
    void DeleteMusic(Guid id);
    void UpdateMusic(Music music);
    List<Music> GetAll();

}