select MessageId, Title, Body, CreateDate, GuestBookId, UserId from dbo.Messages order by CreateDate

select * from GuestBooks

insert into Messages(Title, Body, UserId, GuestBookId) values('не удалось соединиться', 'возникла ошибка 403 при попытке соединения', null, 1)
insert into Messages(Title, Body, UserId, GuestBookId) values('не удалось дозвониться', 'на звоник никт о не отвечает', null, 1)
insert into Messages(Title, Body, UserId, GuestBookId) values('сменить дизайн', 'нужно сменить дизайн центра', null, 1)

insert into Messages(Title, Body, UserId, GuestBookId) values('логин к системе', 'как получить логин', null, 2)
insert into Messages(Title, Body, UserId, GuestBookId) values('восстановить логин', 'не удается восстановить логин', null, 2)

insert into Messages(Title, Body, UserId, GuestBookId) values('звонки в петербург не работают', 'не могу прозвониться', null, 3)
insert into Messages(Title, Body, UserId, GuestBookId) values('обрывы связи', 'стабильно прерывается связь', null, 3)


insert into Messages(Title, Body, UserId, GuestBookId) values(@szTitle, @szBody, null, @szGuestBookId)