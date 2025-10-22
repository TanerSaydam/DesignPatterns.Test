# 🧩 Design Patterns (Tasarım Kalıpları)

## 🔹 Nedir?
**Design Patterns**, yazılım geliştirmede sık karşılaşılan problemler için **tekrar kullanılabilir**, **test edilmiş** ve **genel çözümler** sunan **tasarım şablonlarıdır**.  
Yani doğrudan kopyalanacak kod değil, bir **yaklaşım** veya **tasarım fikridir**.

---

## 🎯 Amacı
- Kodun **yeniden kullanılabilirliğini** artırmak  
- **Bakımı kolay**, **esnek** ve **ölçeklenebilir** yazılım oluşturmak  
- Geliştiriciler arasında **ortak bir dil** sağlamak  

---

## 🔢 Kaç Çeşit?
Yapısal olarak **2 ana gruba** ayrılır.  
Her ana grup altında kendi alt grupları bulunur:

1. 🧩 **Classic Design Patterns**  
   Yazılım mühendisliğinde uzun süredir kullanılan, GoF (Gang of Four) tarafından tanımlanan klasik tasarım kalıplarıdır.  
   *(Örnek: Singleton, Factory, Observer...)*  

2. ⚙️ **Modern Design Patterns**  
   Günümüz teknolojilerine ve framework'lerine (örneğin: Dependency Injection, Repository, CQRS, Mediator vb.) uyarlanmış, çağdaş tasarım yaklaşımlarıdır.  
   *(Örnek: Dependency Injection, CQRS, Event Sourcing...)*  


## 🧱 Classic Design Patterns
3 ana başlıkta incelenir.  
Her ana başlık, belirli bir türdeki yazılım problemlerini **yapısal olarak bir grupta toplayıp çözmeyi** hedefler.

### 1. **Creational Patterns (Yaratımsal Kalıplar)**
Obje oluşturma sürecini merkezi, kontrollü ve esnek hale getiren tasarım kalıplarıdır.
Yani bu kalıplar, “nasıl obje oluşturulur?” sorusuna farklı çözümler sunar. 
Basitçe, “new” kullanmadan obje oluşturmayı yönetir.Obje oluşturma sürecini kontrol altına alır. 

| Pattern | Kısa Açıklama |
|----------|----------------|
| **Singleton** | Classın yalnızca tek bir örneğini oluşturur. |
| **Factory Method** | Alt classların hangi objeyi oluşturacağına karar vermesini sağlar. |
| **Abstract Factory** | İlgili obje ailelerini (ör. tema bileşenleri) oluşturur. |
| **Builder** | Karmaşık objeleri adım adım inşa eder. |
| **Prototype** | Var olan objeleri kopyalayarak yeni objeler oluşturur. |

---

### 2. **Structural Patterns (Yapısal Kalıplar)**
Class ve objelerin **birbirleriyle nasıl ilişkilendirileceğini** tanımlayan kalıplardır. 
Amaç, sistemin parçalarını **daha esnek, yeniden kullanılabilir** ve **bakımı kolay** hale getirmektir. 
Bu kalıplar, büyük yapıları küçük, yönetilebilir bileşenlere ayırarak kodun organizasyonunu güçlendirir. 

| Pattern | Kısa Açıklama |
|----------|----------------|
| **Adapter** | Farklı arayüzlere sahip classların birlikte çalışmasını sağlar. |
| **Bridge** | Soyutlama ile implementasyonu birbirinden ayırır. |
| **Composite** | Objeleri hiyerarşik yapı (ağaç) içinde temsil eder. |
| **Decorator** | Objelere dinamik olarak yeni özellikler ekler. |
| **Facade** | Karmaşık sistemlere basit bir arayüz sunar. |
| **Flyweight** | Aynı objeleri paylaşarak bellek kullanımını azaltır. |
| **Proxy** | Başka bir objeye erişimi kontrol eden aracı objedir. |

---

### 3. **Behavioral Patterns (Davranışsal Kalıplar)**
Objeler arasındaki **iletişimi ve iş birliğini** düzenleyen kalıplardır. 
Odak noktası, bir sistemde **sorumlulukların nasıl dağıtılacağı** ve **objelerin birbirleriyle nasıl etkileşeceğidir**. 
Bu kalıplar, esnek ve genişletilebilir davranış modelleri oluşturmayı sağlar. 

| Pattern | Kısa Açıklama |
|----------|----------------|
| **Chain of Responsibility** | İstekleri sırayla işleyebilen obje zinciri kurar. |
| **Command** | İşlemleri objeler olarak kapsüller. |
| **Interpreter** | Basit diller veya ifadeleri yorumlar. |
| **Iterator** | Koleksiyon elemanlarına sırayla erişim sağlar. |
| **Mediator** | Objeler arası iletişimi merkezi bir aracı üzerinden yönetir. |
| **Memento** | Objenin geçmiş durumunu saklar ve geri yükler. |
| **Observer** | Bir obje değiştiğinde, bağlı objeleri otomatik bilgilendirir. |
| **State** | Objenin davranışını durumuna göre değiştirir. |
| **Strategy** | Bir işlemi farklı algoritmalarla gerçekleştirme olanağı sunar. |
| **Template Method** | Algoritmanın iskeletini tanımlar, alt classlar detayları doldurur. |
| **Visitor** | Obje yapısına yeni işlemler eklemeyi kolaylaştırır. |

---

# 🧭 Modern Yaklaşımlara Geçiş – Design Patterns’in Evrimi

## 🎯 Giriş  
1994 yılında yayımlanan *“Design Patterns: Elements of Reusable Object-Oriented Software”* kitabı ile popülerleşen klasik tasarım kalıpları,  
zamanla teknolojilerin, dillerin ve framework’lerin gelişmesiyle birlikte **modern yazılım mimarilerinin temelini** oluşturdu.  

Bu süreçte:
- **Bazı kalıplar birebir korunarak kullanılmaya devam etti,**
- **Bazıları modern yapılara evrildi,**
- **Bazıları ise framework’lerin içine gömülerek soyutlandı.**

---

## 🔄 Örneklerle Klasik → Modern Evrim

| Klasik Design Pattern | Modern Karşılığı / Evrimi | Açıklama |
|------------------------|---------------------------|-----------|
| **Singleton** | Dependency Injection Container + Lifetime (`Singleton`, `Scoped`, `Transient`) | Artık global erişim yerine, DI container yaşam süresi yönetimiyle kontrol ediliyor. |
| **Proxy** | Service Pattern + Attribute / Filter / Middleware | Proxy’nin “çağrıyı sarmalama” görevi framework seviyesine taşındı. Cross-cutting concern’ler artık AOP, pipeline veya filter yapılarıyla yönetiliyor. |
| **Factory / Abstract Factory** | IoC Container (Dependency Injection) | Nesne üretim süreci artık container tarafından otomatik yönetiliyor. |
| **Observer** | Event Bus, MediatR, SignalR, Rx, Pub/Sub | Gözlemci mantığı event-driven (olay güdümlü) mimarilere dönüştü. |
| **Decorator** | Pipeline, Middleware, ActionFilter, Behavior | Çağrı zincirine davranış ekleme artık pipeline tabanlı hale geldi. |
| **Command Pattern** | CQRS + Mediator | Komutların bağımsız olarak işlenmesi CQRS yapısı ve handler’lar aracılığıyla yapılıyor. |
| **Facade** | API Gateway, Application Service | Alt sistemleri sadeleştiren yapı artık gateway veya application-level servis olarak uygulanıyor. |
| **Flyweight** | Cache, Object Pooling, Shared Immutable State | Nesne paylaşımı modern cache mekanizmalarıyla sağlanıyor. |

---

## 🧠 Genel Değerlendirme  

> Klasik Design Pattern’lar hâlâ yaşıyor;  
> ancak artık framework’lerin, DI container’ların, ve middleware yapıların **temeline entegre olmuş** durumda.

Bir başka ifadeyle:  
- 1994’te bu kalıplar **manuel olarak** uygulanıyordu,  
- 2025’te ise bu prensipler **framework seviyesinde otomatikleşmiş** durumda.

---

## 💬 Sonuç  
> “Klasik Design Pattern’lar, modern yazılım mimarilerinin temel yapı taşları olmuş,  
> ancak teknolojik evrimle birlikte birçoğu framework’lerin içerisine entegre edilerek soyutlanmıştır.” ✅
