В папке с проектом введите команду:
docker build . -t store-image
После того, как образ будет готов:
docker compose up -d
Для того, чтобы остановить контейнеры:
docker compose stop
Для того, удалить контейнеры:
docker compose down
