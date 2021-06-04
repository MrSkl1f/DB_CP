# Система трансферов игроков

## Формализация данных
### Команда
Название, аналитик, менеджер, главный тренер, страна, стадион, баланс
### Игрок
Имя, команда, статистика, позиция, вес, рост, номер, возраст, происхождение, цена
### Статистика игрока
Количество голов, среднее время игры
### Пользователь
Логин, пароль, права доступа
### Желаемые игроки (у аналитика и менеджера)
Игрок, менеджер
### Доступные сделки (у менеджера)
Игрок, цена, статус

## Типы пользователей и их функционал
### Аналитик
> Имеет доступ к данным о командах и игроках, может отправлять игрока на покупку менеджеру
### Менеджер
> Может просматривать игроков, посылаемых аналитиком, отправлять и подтверждать сделки с другими менеджерами
### Модератор
> Имеет список сделок от менеджеров, может менять денежный баланс команд, команду игрока. Изменение другой информации игроков и команд.
### Администратор
> Может изменять информацию игроков и команд. Изменяет права доступа пользователя

## UseCases
![UserCases](./doc/UseCase.svg)

## Диаграмма БД
![DB](./doc/DB.svg)

## ER
![ER](./doc/ER.svg)

## Описание типа приложения и выбранного технического стека
Тип приложения - Desktop
Технологический стек - C#, PostgreSQL

## UML диаграмма классов доступа к данным
![UmlDB](./doc/UmlDB.svg)

## UML диаграмма классов бизнес-логики
![UmlBL](./doc/UmlBL.svg)

## UML диаграмма классов-сущностей базы данных
![ModelsDB](./doc/ModelsDB.svg)

## Сущности системы
![Core](./doc/Core.svg)

## UML диаграмма классов-dto
![DTO](./doc/DTO.svg)