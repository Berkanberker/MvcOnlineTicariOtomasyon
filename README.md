# 🚀 MVC Online Ticari Otomasyon

Bu proje, orta ve küçük ölçekli işletmelerin (KOBİ) tüm ticari süreçlerini dijital ortamda yönetmelerini sağlayan kapsamlı bir **Ticari Otomasyon ve Yönetim Paneli** uygulamasıdır. **ASP.NET MVC 5** mimarisi kullanılarak geliştirilmiştir.

## 🎯 Proje Hakkında

İşletmelerin cari hesap takibinden stok yönetimine, fatura kesiminden personel takibine kadar ihtiyaç duyduğu temel modülleri tek bir çatı altında toplar. Modern ve kullanıcı dostu arayüzü sayesinde muhasebe bilgisi gerektirmeden kolayca kullanılabilir.

## ✨ Öne Çıkan Özellikler

*   **📦 Stok & Ürün Yönetimi:** Ürünlerinizi kategorize edin, stok miktarlarını ve fiyatlarını anlık takip edin.
*   **👥 Cari Hesap Takibi:** Müşteri ve tedarikçi hesaplarını detaylıca izleyin.
*   **🛒 Satış Hareketleri:** Satış işlemlerini kaydedin, otomatik tutar hesaplaması yapın.
*   **🧾 Fatura Modülü:** Dinamik fatura oluşturun ve kalemlerini yönetin.
*   **🚚 Kargo Takibi:** Siparişlerin kargo süreçlerini (Hazırlanıyor, Yolda, Teslim Edildi) ve takip numaralarını yönetin.
*   **📊 İstatistik & Raporlama:** Grafiklerle desteklenmiş Dashboard üzerinden işletmenizin durumunu analiz edin.
*   **👮 Personel & Departman:** Şirket çalışanlarını ve departmanları organize edin.
*   **🔐 Yetkilendirme:** Admin ve personel girişleri ile güvenli erişim.
*   **🔔 QR Kod & Bildirimler:** (Geliştirilme aşamasında) Ürün QR kodları ve bildirim entegrasyonları.

## 🛠️ Teknolojiler

*   **Backend:** C#, ASP.NET MVC 5
*   **ORM:** Entity Framework 6 (Code First)
*   **Veritabanı:** MS SQL Server
*   **Frontend:** HTML5, CSS3, Bootstrap 4, JavaScript
*   **Kütüphaneler:**
    *   *SweetAlert2* (Modern uyarı pencereleri)
    *   *DataTables* (Gelişmiş tablo yönetimi)
    *   *Toastr* (Bildirimler)

## 🚀 Kurulum

1.  Bu projeyi bilgisayarınıza klonlayın:
    ```bash
    git clone https://github.com/Berkanberker/MvcOnlineTicariOtomasyon.git
    ```
2.  Visual Studio ile `MvcOnlineTicariOtomasyon.sln` dosyasını açın.
3.  `web.config` dosyasındaki `connectionStrings` alanını kendi SQL Server bilgilerinize göre düzenleyin.
4.  **Package Manager Console**'u açın ve veritabanını oluşturmak için şu komutu çalıştırın:
    ```powershell
    Update-Database
    ```
5.  Projeyi derleyin (Ctrl+Shift+B) ve çalıştırın (F5).

---
© 2025 Ticari Otomasyon Projesi - Tüm Hakları Saklıdır.
