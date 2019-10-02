
using System.ComponentModel.DataAnnotations;

namespace ClassStudent.DAL.Interfaces
{
    public interface IEntity<T>
    {
        [Key]
        T Id { get; set; }
    }
}
