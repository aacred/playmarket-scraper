# playmarket-scraper
Google Play  Market Scraper with grpc services

Назначение:

Получение информации о приложениях на google market

Проект состоит из следующих модулей:

1. GrpcApplication - сервис скрейпинга приложений на маркете
2. InputApi - сервис для работы с api
3. Repository - вспомогательный модуль для работы с БД

Чтобы запустить проект, выполнить:

docker-compose -f "docker-compose.yml" up -d --build 

Документация доступна на http://localhost:5000/swagger/index.html
