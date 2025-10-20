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
Nesne oluşturma sürecini merkezi, kontrollü ve esnek hale getiren tasarım kalıplarıdır.
Yani bu kalıplar, “nasıl nesne oluşturulur?” sorusuna farklı çözümler sunar.
Basitçe, “new” kullanmadan nesne oluşturmayı yönetir.Nesne oluşturma sürecini kontrol altına alır.

| Pattern | Kısa Açıklama |
|----------|----------------|
| **Singleton** | Sınıfın yalnızca tek bir örneğini oluşturur. |
| **Factory Method** | Alt sınıfların hangi nesneyi oluşturacağına karar vermesini sağlar. |
| **Abstract Factory** | İlgili nesne ailelerini (ör. tema bileşenleri) oluşturur. |
| **Builder** | Karmaşık nesneleri adım adım inşa eder. |
| **Prototype** | Var olan nesneleri kopyalayarak yeni nesneler oluşturur. |

---

### 2. **Structural Patterns (Yapısal Kalıplar)**
Sınıf ve nesnelerin **birbirleriyle nasıl ilişkilendirileceğini** tanımlayan kalıplardır.  
Amaç, sistemin parçalarını **daha esnek, yeniden kullanılabilir** ve **bakımı kolay** hale getirmektir.  
Bu kalıplar, büyük yapıları küçük, yönetilebilir bileşenlere ayırarak kodun organizasyonunu güçlendirir.

| Pattern | Kısa Açıklama |
|----------|----------------|
| **Adapter** | Farklı arayüzlere sahip sınıfların birlikte çalışmasını sağlar. |
| **Bridge** | Soyutlama ile implementasyonu birbirinden ayırır. |
| **Composite** | Nesneleri hiyerarşik yapı (ağaç) içinde temsil eder. |
| **Decorator** | Nesnelere dinamik olarak yeni özellikler ekler. |
| **Facade** | Karmaşık sistemlere basit bir arayüz sunar. |
| **Flyweight** | Aynı nesneleri paylaşarak bellek kullanımını azaltır. |
| **Proxy** | Başka bir nesneye erişimi kontrol eden aracı nesnedir. |

---

### 3. **Behavioral Patterns (Davranışsal Kalıplar)**
Nesneler arasındaki **iletişimi ve iş birliğini** düzenleyen kalıplardır.  
Odak noktası, bir sistemde **sorumlulukların nasıl dağıtılacağı** ve **nesnelerin birbirleriyle nasıl etkileşeceğidir**.  
Bu kalıplar, esnek ve genişletilebilir davranış modelleri oluşturmayı sağlar.

| Pattern | Kısa Açıklama |
|----------|----------------|
| **Chain of Responsibility** | İstekleri sırayla işleyebilen nesne zinciri kurar. |
| **Command** | İşlemleri nesneler olarak kapsüller. |
| **Interpreter** | Basit diller veya ifadeleri yorumlar. |
| **Iterator** | Koleksiyon elemanlarına sırayla erişim sağlar. |
| **Mediator** | Nesneler arası iletişimi merkezi bir aracı üzerinden yönetir. |
| **Memento** | Nesnenin geçmiş durumunu saklar ve geri yükler. |
| **Observer** | Bir nesne değiştiğinde, bağlı nesneleri otomatik bilgilendirir. |
| **State** | Nesnenin davranışını durumuna göre değiştirir. |
| **Strategy** | Bir işlemi farklı algoritmalarla gerçekleştirme olanağı sunar. |
| **Template Method** | Algoritmanın iskeletini tanımlar, alt sınıflar detayları doldurur. |
| **Visitor** | Nesne yapısına yeni işlemler eklemeyi kolaylaştırır. |

---

## 💡 Kısaca
| Kategori | Amaç |
|-----------|-------|
| **Creational** | Nesne oluşturma sürecini yönetir. |
| **Structural** | Nesnelerin yapısını ve ilişkilerini düzenler. |
| **Behavioral** | Nesneler arası etkileşimi ve davranışı tanımlar. |
