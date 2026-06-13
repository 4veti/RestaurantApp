# Инструкции за изпълнение

1. За изпълнението на проекта е нужно да е наличен следният софтуер:
   - Visual Studio (.NET 8)
   - SQL Server
2. От Visual Studio Installer трябва да се инсталира .NET MAUI. Избира се 'Modify' опцията за текущата версия на Visual Studio, след това в секцията 'Desktop & Mobile' се избира '.NET Multi-platform App UI Development'.
3. В проектът 'RestaurantApp.Client.Api' в конфигурационният файл 'appsettings.development.json' се задава правилното име на инстанцията на наличният SQL Server (може да остане localhost, ако е конфигуриран така).
4. Задава се 'RestaurantApp.Client.Api' като стартиращ проект на Solution-а.
5. В 'Package Manager Console' за проект по подразбиране се задава 'RestaurantApp.Infrastructure' и се изпълнява командата 'update-database'.
6. Преместват се конфигурационните файлове от директорията 'Configs' в директорията '...\RestaurantApp\bin\Debug\net8.0-windows10.0.19041.0\win10-x64\AppX'.
7. Извършва се Build на целия Solution, а след това се стартира процесът на API проекта от: '...\RestaurantApp.Api\bin\Debug\net8.0\RestaurantApp.ClientApi.exe' 
8. Сменя се стартиращият проект на Solution-a да е 'RestaurantApp.Client' и се стартира профилът 'Cashier (Windows Machine)'.
9. Добавят се различни ястия и напитки към менюто.

Забележки:
- Възможно е понякога при стартиране да се появи размит блед екран и да не се зареди самото приложение. В този случай е необходимо процесът да се спре и стартира наново.

Видео демонстрация на функционалността на проекта:
https://github.com/user-attachments/assets/715301b9-88ed-4836-bcc7-506a8619db17

