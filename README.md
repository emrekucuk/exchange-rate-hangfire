# Exchange Rate Hangfire Projesi

### Her saat başı döviz kurlarını bir web sitesinden çekip veritabanına kaydediyoruz.

### Eksik olan Döviz türlerini ekleyebiliyoruz.

### Veritabanında kayıtlı olan kur değerlerini listeleyebiliriz. (Hepsini, Bugünkileri ve İstediğimiz Bir Tarihi)

### Anlık olarak kurları görmek istersek "Hangfire" üzerinden job'ı çalıştırabiliriz


## Database Update
```sh
dotnet-ef database update
```

## Projeyi Çalıştırma
```sh
dotnet run
```

## URL'ler
- Swagger: http://localhost:5032/swagger/index.html
- Hangfire: http://localhost:5032/hangfire/recurring

## Hangfire Bilgileri
- Username: emre
- Password: 12345