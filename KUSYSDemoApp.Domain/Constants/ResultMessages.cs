namespace KUSYSDemoApp.Domain.Constants
{
    /// <summary>
    /// İşlem Sonucu Mesajlarını İfade Eder
    /// </summary>
    public static class ResultMessages
    {        
        /// <summary>
        /// /// Evet
        /// /// </summary>
        public static readonly string Confirm = "Evet";

        /// <summary>
        /// Hayır
        /// </summary>
        public static readonly string Denial = "Hayır";

        /// <summary>
        /// İşlem Başarıyla Gerçekleştirilmiştir.
        /// </summary>
        public static readonly string Success = "İşlem Başarıyla Gerçekleştirilmiştir.";

        /// <summary>
        /// İşlem Sırasında Bir Hata Oluştu!
        /// </summary>
        public static readonly string Error = "İşlem Sırasında Bir Hata Oluştu!";

        /// <summary>
        /// Kullanıcı Girişi Başarılıdır.
        /// </summary>
        public static readonly string LoginSuccess = "Kullanıcı Girişi Başarılıdır.";

        /// <summary>
        /// Kullanıcı Adı veya Şifreniz Hatalıdır. Lütfen Kontrol Ediniz!
        /// </summary>
        public static readonly string WrongUsernameOrPassword = "Kullanıcı Adı veya Şifreniz Hatalıdır. Lütfen Kontrol Ediniz!";

        /// <summary>
        /// Kullanıcı Adı ve Şifre Alanları Boş Geçilemez. Lütfen Kontrol Ediniz!
        /// </summary>
        public static readonly string EmptyUsernameOrPassword = "Kullanıcı Adı ve Şifre Alanları Boş Geçilemez. Lütfen Kontrol Ediniz!";

        /// <summary>
        /// Bu İşlemi Yapmaya Yetkiniz Bulunmamaktadır!
        /// </summary>
        public static readonly string UnauthorizedAction = "Bu İşlemi Yapmaya Yetkiniz Bulunmamaktadır!";

        /// <summary>
        /// Zorunlu Alanlar Boş Geçilemez. Lütfen Kontrol Ediniz!
        /// </summary>
        public static readonly string InputEmpty = "Zorunlu Alanlar Boş Geçilemez. Lütfen Kontrol Ediniz!";

        /// <summary>
        /// Sözkonusu Kayıt Bulunamadı. Lütfen Kontrol Ediniz!
        /// </summary>
        public static readonly string NonExistingData = "Sözkonusu Kayıt Bulunamadı. Lütfen Kontrol Ediniz!";

        /// <summary>
        /// Lütfen Kayıt Seçiniz!
        /// </summary>
        public static readonly string RecordSelection = "Lütfen Kayıt Seçiniz!";

        /// <summary>
        /// Silmek İstediğinize Emin misiniz?
        /// </summary>
        public static readonly string RecordDeletionConfirm = "Silmek İstediğinize Emin misiniz?";

        /// <summary>
        /// İlgili kayıt(lar) sistemden silinecektir.
        /// </summary>
        public static readonly string RecordDeletionConfirmDetail = "İlgili kayıt(lar) sistemden silinecektir.";
    }
}