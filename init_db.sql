select MessageId, Title, Body, CreateDate, GuestBookId, UserId from dbo.Messages order by CreateDate

select * from GuestBooks

insert into Messages(Title, Body, UserId, GuestBookId) values('�� ������� �����������', '�������� ������ 403 ��� ������� ����������', null, 1)
insert into Messages(Title, Body, UserId, GuestBookId) values('�� ������� �����������', '�� ������ ���� � �� ��������', null, 1)
insert into Messages(Title, Body, UserId, GuestBookId) values('������� ������', '����� ������� ������ ������', null, 1)

insert into Messages(Title, Body, UserId, GuestBookId) values('����� � �������', '��� �������� �����', null, 2)
insert into Messages(Title, Body, UserId, GuestBookId) values('������������ �����', '�� ������� ������������ �����', null, 2)

insert into Messages(Title, Body, UserId, GuestBookId) values('������ � ��������� �� ��������', '�� ���� ������������', null, 3)
insert into Messages(Title, Body, UserId, GuestBookId) values('������ �����', '��������� ����������� �����', null, 3)


insert into Messages(Title, Body, UserId, GuestBookId) values(@szTitle, @szBody, null, @szGuestBookId)