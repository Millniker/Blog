delete "Post";
insert into "Post" ("Id","Title","Description", "readingTime", "Image","AuthorId","Author","Likes","HasLike","Created","CommentCount","UserEntityId")      
values('05d8b266-8e63-11ed-a1eb-0242ac120002',N'Борщ',N'тмоатмомвмыв','1',NULL,'05d8b266-8e63-11ed-a1eb-0242ac120002',N'Кто-то7','0','false','2023-01-07T08:37:56.655Z','0','05d8b266-8e63-11ed-a1eb-0242ac120002'),
('05d8b4f0-8e63-11ed-a1eb-0242ac120002',N'Кино',N'вмывмывивпивма','2',NULL,'05d8b266-8e63-11ed-a1eb-0242ac120002',N'Кто-то','0','true','2023-01-07T08:37:56.655Z','0','05d8b266-8e63-11ed-a1eb-0242ac120002'),
('05d8b626-8e63-11ed-a1eb-0242ac120002',N'Пиво', N'киецкикипвикцпи','3',NULL,'05d8b4f0-8e63-11ed-a1eb-0242ac120002',N'Кто-то5','0','false','2023-01-07T08:37:56.655Z','0','05d8b4f0-8e63-11ed-a1eb-0242ac120002'),
('05d8b8ec-8e63-11ed-a1eb-0242ac120002',N'Смех',N'цкимпицицп','3',NULL,'05d8b626-8e63-11ed-a1eb-0242ac120002',N'Кто-то4','0','false','2023-01-07T08:37:56.655Z','0','05d8b626-8e63-11ed-a1eb-0242ac120002'),
('05d8ba90-8e63-11ed-a1eb-0242ac120002',N'Кек',N'ыикрипыиыиыпи','4',NULL,'05d8b626-8e63-11ed-a1eb-0242ac120002',N'Кто-то3','0','true','2023-01-07T08:37:56.655Z','0','05d8b626-8e63-11ed-a1eb-0242ac120002'),
('05d8bbda-8e63-11ed-a1eb-0242ac120002',N'Лол',N'ыивпыиыпаирпыаим','5',NULL,'05d8bbda-8e63-11ed-a1eb-0242ac120002',N'Кто-то2','0','false','2023-01-07T08:37:56.655Z','0','05d8bbda-8e63-11ed-a1eb-0242ac120002'),
('05d8bcfc-8e63-11ed-a1eb-0242ac120002',N'Чебурек',N'иыиыпиыиви','6',NULL,'05d8bbda-8e63-11ed-a1eb-0242ac120002',N'Кто-то1','0','true','2023-01-07T08:37:56.655Z','0','05d8bbda-8e63-11ed-a1eb-0242ac120002');

delete "PostEntityTagEntity";
insert into "PostEntityTagEntity" ("PostsId", "TagsId")
values('05d8b266-8e63-11ed-a1eb-0242ac120002', '3b781828-f329-4785-aecd-08d9b9f3d27c'),
('05d8b266-8e63-11ed-a1eb-0242ac120002', 'b4e44ca7-7b96-4d12-aed3-08d9b9f3d27c'),
('05d8b266-8e63-11ed-a1eb-0242ac120002', '5fb99899-1833-4e9e-aed7-08d9b9f3d27c'),
('05d8b266-8e63-11ed-a1eb-0242ac120002', '279e470a-11dc-46e2-aeda-08d9b9f3d27c'),
('05d8ba90-8e63-11ed-a1eb-0242ac120002', '3b781828-f329-4785-aecd-08d9b9f3d27c'),
('05d8bbda-8e63-11ed-a1eb-0242ac120002', '3b781828-f329-4785-aecd-08d9b9f3d27c'),
('05d8bcfc-8e63-11ed-a1eb-0242ac120002', '3b781828-f329-4785-aecd-08d9b9f3d27c');
