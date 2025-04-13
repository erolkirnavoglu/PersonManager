# PersonManager
PersonManager, basit bir rehber uygulamasıdır. 
Rehberde kişi oluşturma
Rehberde kişi kaldırma
Rehberdeki kişiye iletişim bilgisi ekleme
Rehberdeki kişiden iletişim bilgisi kaldırma
Rehberdeki kişilerin listelenmesi
Rehberdeki bir kişiyle ilgili iletişim bilgilerinin de yer aldığı detay bilgilerin getirilmesi
Rehberdeki kişilerin bulundukları konuma göre istatistiklerini çıkartan bir rapor talebi
Sistemin oluşturduğu raporların listelenmesi
Sistemin oluşturduğu bir raporun detay bilgilerinin getirilmesi
Bu uygulama .NET Core ile geliştirilmiş olup, Entity Framework Core, RabbitMQ,PostgreSQL ve diğer modern .NET teknolojileri kullanmaktadır.

- [Özellikler](#özellikler)
- [Teknolojiler](#teknolojiler)
- [Kullanım](#kullanım)
- [Test](#test)

## Özellikler

- **CRUD İşlemleri**: Kullanıcılar, kişileri listeleyebilir, yeni kişileri ekleyebilir, var olanları güncelleyebilir veya silebilir.
- **Raporlar**: Kullanıcılar, kişisel verilerle ilgili raporlar oluşturabilir.
- **Asenkron Mesajlaşma**: RabbitMQ üzerinden raporları işlemek için mesaj kuyrukları kullanılır.

## Teknolojiler

- **.NET Core / .NET 8**: Uygulama platformu.
- **ASP.NET Core**: Web API ve UI katmanı için.
- **Entity Framework Core**: Veritabanı işlemleri için ORM.
- **RabbitMQ**: Asenkron mesajlaşma ve rapor yönetimi.
- **PostgreSQL**: Veritabanı yönetimi.

## Kullanım

- **UI**: Uygulama arayüzü https://localhost:7116/persons adresinde yayınlanmaktadır.
- **Api**: Swagger dokümantasyonu https://localhost:7013/swagger/index.html adresinde erişilebilir durumdadır.
- **PostgreSQL**: Veritabanı, uzaktaki ücretsiz bir bulut servisi üzerinde barındırılmaktadır. Bağlantı bilgileri appsettings.json dosyasındaki ConnectionStrings:DefaultConnection alanında tanımlıdır.
- **RabbitMQ**: Mesaj kuyruğu sistemi, uzaktaki ücretsiz bir bulut servisi üzerinden çalışmaktadır. Bağlantı bilgileri appsettings.json dosyasındaki RabbitMq:Url alanında tanımlıdır.

## Test

- Servis katmanı ve API controller'ları için birim testler yazılmıştır.