using Nevaa.Library.GenericRepository.Base.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Nevaa.Library.GenericRepository.Base
{
    public class BaseModel : IBaseModel
    {
        public int Id { get; set; }
        [DisplayName("Aktif")]
        public bool IsActive { get; set; }

        [DisplayName("Kayıt Yapan Kullanıcı")]
        public int CreatedBy { get; set; }
        [DisplayName("Kayıt Tarihi")]
        public DateTime CreatedDate { get; set; }

        [DisplayName("Günceleme Yapan Kullanıcı")]
        public int? UpdatedBy { get; set; }
        [DisplayName("Güncellenme Tarihi")]
        public DateTime? UpdatedDate { get; set; }
    }
}
