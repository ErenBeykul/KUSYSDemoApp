using System.ComponentModel.DataAnnotations;

namespace KUSYSDemoApp.Domain.Enums
{
    public enum ResultName
    {   
        /// <summary>
        /// Başarı
        /// </summary>
        [Display(Name = "Başarı")]
        Success,

        /// <summary>
        /// Uyarı
        /// </summary>
        [Display(Name = "Uyarı")]
        Warning,

        /// <summary>
        /// Hata
        /// </summary>
        [Display(Name = "Hata")]
        Error,
    }
}