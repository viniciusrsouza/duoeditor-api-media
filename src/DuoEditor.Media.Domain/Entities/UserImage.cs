using System.ComponentModel.DataAnnotations;

namespace DuoEditor.Media.Domain.Entities
{
  public class UserImage
  {
    [Key]
    public int UserId { get; set; }
    public string FileName { get; set; }

    public UserImage(int userId, string fileName)
    {
      UserId = userId;
      FileName = fileName;
    }
  }
}