В папке с проектом введите команду:
docker build . -t store-image

После того, как образ будет готов:
docker compose up -d

Для того, чтобы остановить работу контейнеров:
docker compose stop

Для того, чтобы перезапустить контейнеры:
docker compose start

Для того, удалить контейнеры:
docker compose down
