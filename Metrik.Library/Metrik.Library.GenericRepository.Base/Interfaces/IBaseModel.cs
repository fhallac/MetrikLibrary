using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Metrik.Library.GenericRepository.Base.Interfaces
{
    public interface IBaseModel
    {
        [Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        int Id { get; set; }
    }
}
