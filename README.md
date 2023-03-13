В папке с проектом введите команду:
```
docker build . -t store-image
```

После того, как образ будет готов:
```
docker compose up -d
```

Для того, чтобы остановить работу контейнеров:
```
docker compose stop
```

Для того, чтобы перезапустить контейнеры:
```
docker compose start
```

Для того, удалить контейнеры:
```
docker compose down
```
В браузере запустить 
```
http://localhost:5000
```

Пользователи для входа:
- Логин: Admin, Пароль: MyAdmin174# - имеет полный доступ к адмнистративной панели
- Логин: Moderator, Пароль: MyModerator174# - имеет доступ к изменению статуса заказа. Не имеет доступа к CRUD'у продуктов
- Логин: TestUser, Пароль: MyTest174# - не имеет доступ к адмнистративной панели. 
