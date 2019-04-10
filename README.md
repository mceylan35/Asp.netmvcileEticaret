Ortak İsterler
Projede ‘kayıtlı kullanıcı’, ‘ziyaretçi’ ve ‘admin’ olacak şekilde üç farklı kullanıcı tipi bulunmalıdır. Bu kullanıcıların rolleri kapsamında yapabilecekleri aşağıdaki tabloda sıralanmıştır.
Ziyaretçi
o	Üye girişi yapmadan tüm temel sayfalara (admin ve kayıtlı kullanıcının özel sayfaları hariç) erişebilmeli
o	Ürünleri inceleme ve sepete ekleme işlemlerini yapabilmeli (Sepete eklenen ürünleri satın almak istediğinde üye kaydı veya üye girişi yapması istenecektir.)
o	Sepete birden fazla ürün ekleyerek alışverişe devam edebilmelidir. Kullanıcı ürünü sepete eklediğinde direkt olarak ‘sepet’ sayfasına yönlendirilmemelidir. Bu sayfaya alışverişi tamamlamak isteyen kullanıcı tıklaması ile geçilmelidir.
o	Üye kaydı / Üye girişi yapabilmeli
o	Ürün arama modülünü kullanabilmeli
o	İletişim menüsünden yönetime mesaj gönderebilmeli (iletişim formundan gönderilen ziyaretçi mesajları gerçek bir e-posta adresine gönderilmeli ve admin tarafından okunabilmelidir.)
Kayıtlı Kullanıcı
Ziyaretçi rolüne tanımlanan tüm işlemleri yapabilmelidir. İlave olarak;
o	Sepete eklenen ürünleri satın alma seçeneğine sahip olmalıdır. Satın alma senaryosunda herhangi bir ödeme işlemi gerçekleştirilmeden kullanıcının sınırsız bütçesi olduğu kabul edilmelidir. Satın alınan ürünün stok bilgisi güncellenmeli, şayet bitmiş ise satışa kapatılmalı fakat ürünün kullanıcıya gösterimi engellenmemelidir.
o	Hesabım / Profilim şeklinde bir menü ile; geçmiş siparişlerini inceleyebilmeli, kişisel bilgilerini düzenleyebilmelidir.
o	İletişim menüsünden göndereceği mesajlar ziyaretçi tipinden farklı olarak kullanıcıyı tanımlayan bir ifade ile (id vb.) gönderilmelidir.
o	Ürünlerin tanıtıldığı sayfalarda ‘favorilerime ekle’ şeklinde bir yapı bulunmalı ve kullanıcı istediği ürünü favori listesine ekleyebilmelidir. Bu listeye istediği zaman hesabım / profilim şeklindeki menüden ulaşabilmelidir.

Admin
o	Yeni kategori ekleme / kategori silme / kategori güncelleme işlemlerini yapabilmeli
o	Yeni ürün ekleme / ürün stoğu belirleme / ürün silme / ürün güncelleme işlemlerini yapabilmeli
o	Toplu promosyon / kategori bazlı promosyon / ürün bazlı promosyonlar tanımlayabilmeli
o	Siteye girişte açılır pop-up, duyuru vb. şeklinde bildirim paylaşabilmeli
o	İstatistikleri görebileceği bir ‘dashboard’ sayfasına erişebilmelidir. Bu istatistikler;
o	Günlük/haftalık ve toplam ziyaret sayısı
o	Günlük/haftalık ve toplam ürün satış sayısı
o	Günlük/haftalık ve toplam üye kaydı sayısı
o	En çok satış yapılan kategori
o	En çok satışı yapılan ürünler (ilk 5)
o	Sepete en çok eklenen ürünler (ilk 5)


•	Yukarıdaki kullanıcı rollerinin gerçekleştirileceği tüm ön yüz sayfalarının hazırlanması beklenmektedir. Bu sayfalara ilave olarak kullanıcıyı genel bir e-ticaret sistemine uygun ‘Ana sayfa’ karşılamalıdır.

•	Her bir proje içerisinde en az 100 farklı ürün bulunmalı ve değerlendirme testlerinin yapılabilmesi için yeterli sayıda girdiye sahip olmalıdır.

Özel İstek
Bir ürünü satın alan müşteriye o ürün ile ilgili yorum yapma ve yıldızlı puan verme imkânı tanınacaktır. Ürün sayfalarında o ürünü satın almayanlar için puan verme kısmı pasif şekilde görünecek fakat ürünün puan ortalaması ve yorumları gösterilecektir. 
Ürün arama kısmından yapılan bir aramada ürünler listelenirken en çok oy alan ve en çok puan alan ürünlerin üst sırada görünmesi beklenmektedir. Burada her bir oy ve her bir yorum eşit ağırlığa sahip olmalıdır.
