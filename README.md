# Sense Events

Сервис мероприятий

### Как развернуть?

Первым делом переходим в корневую папку локальных репозиториев. В данном примере используется `/source`.
Клонируем репозиторий.

```
cd /source
git clone git@github.com:zar4za/SenseEvents.git
```

Переходим в папку `/SenseEvents` и запускаем сервисы при помощи docker compose.

```
cd SenseEvents
docker compose up
```

Сервис аутентификации будет доступен по адресу http://localhost:5001/swagger
Сервис мероприятий по адресу http://localhost:8080/swagger

Чтобы использовать API мероприятий необходимо добавить JWT токен в запрос. В Swagger это делается при помощи кнопки **Authorize**.
В открывшемся диалоговом окне надо вписать токен в формате `Bearer XXXXX...`.

Сообщения в RabbitMQ отправляются через интерфейс, развернутый локально
http://localhost:15672/#/queues . Для каждого эвента существует выделенная очередь.

Сообщения отправляются в следующем формате:
```
{
	"messageType": [
		"urn:message:NAMESPACE:TYPE"
	,
	"message": {
		// Свойства самого контракта эвента
	}
}
```

Пример сообщения при удалении спейса:
```
{
	"messageType": [
		"urn:message:SenseEvents.Infrastructure.RabbitMQ.Events.SpaceDelete:SpaceDeleteEvent"
	],
	"message": { 
		"SpaceId": "3fa85f64-5717-4562-b3fc-000000000000"
	}
}
```