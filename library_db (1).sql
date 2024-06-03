-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Хост: 127.0.0.1:3306
-- Время создания: Июн 03 2024 г., 07:37
-- Версия сервера: 10.8.4-MariaDB
-- Версия PHP: 7.2.34

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- База данных: `library_db`
--

-- --------------------------------------------------------

--
-- Структура таблицы `Book`
--

CREATE TABLE `Book` (
  `inventory_number` int(11) NOT NULL,
  `title` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `author` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL
) ;

--
-- Дамп данных таблицы `Book`
--

INSERT INTO `Book` (`inventory_number`, `title`, `author`) VALUES
(1000, 'Убить пересмешника', 'Харпер Ли'),
(1001, 'Гордость и предубеждение', 'Джейн Остен'),
(1002, 'Дневник Анны Франк', 'Анна Франк'),
(1003, '1984', 'Джордж Оруэлл'),
(1004, 'Гарри Потер', 'Джоан Роулинг'),
(1005, 'Властелин Колец', 'Дж. Р.Р. Толкин'),
(1006, 'Маленький принц', 'Антуан де Сент-Экзюпери'),
(1007, 'Великий Гэтсби', 'Фрэнсис Скотт Фицджеральд'),
(1008, 'Моби Дик', 'Герман Мелвилл'),
(1009, 'Шерлок Холмс', 'Артур Конан Дойл'),
(1010, 'Кровь и пепел', 'Дженнифер Арментраут'),
(1011, 'Война и мир', 'Лев Толстой'),
(1012, 'Анна Каренина', 'Лев Толстой'),
(1013, 'Преступление и наказание', 'Федор Достоевский'),
(1014, 'Братья Карамазовы', 'Федор Достоевский'),
(1015, 'Мастер и Маргарита', 'Михаил Булгаков'),
(1016, 'Охота на Снарка', 'Льюис Кэрролл'),
(1017, 'Винни-Пух', 'Алан Милн'),
(1018, 'Алиса в стране чудес', 'Льюис Кэрролл'),
(1019, 'Три мушкетера', 'Александр Дюма'),
(1020, 'Записки юного врача', 'Михаил Булгаков'),
(1021, 'Человек-невидимка', 'Герберт Уэллс'),
(1022, 'Франкенштейн', 'Мэри Шелли'),
(1023, 'Дракула', 'Брэм Стокер'),
(1024, 'Призрак оперы', 'Гастон Леру'),
(1025, 'Сияние', 'Стивен Кинг');

-- --------------------------------------------------------

--
-- Структура таблицы `Reader`
--

CREATE TABLE `Reader` (
  `reader_ticket_number` int(11) NOT NULL,
  `name` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `address` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `phone` varchar(15) COLLATE utf8mb4_unicode_ci DEFAULT NULL
) ;

--
-- Дамп данных таблицы `Reader`
--

INSERT INTO `Reader` (`reader_ticket_number`, `name`, `address`, `phone`) VALUES
(1, 'Акуневич Антон Николаевич', 'Чита, 9 мкр., д.2', '89144497171'),
(2, 'Романов Михаил Русланович', 'Чита, Бабушкина д.66', '89134629532'),
(3, 'Синякин Юрий Александрович', 'Чита, Лермонтова 10, д.3', '85234652634'),
(4, 'Запекин Пётр Александрович', 'Чита, Ленина, д.6', NULL),
(5, 'Куранов Алексей Егорович', 'Чита, Журавлева, д.6', '89642853674');

-- --------------------------------------------------------

--
-- Структура таблицы `Registration`
--

CREATE TABLE `Registration` (
  `id` int(11) NOT NULL,
  `inventory_number` int(11) DEFAULT NULL,
  `reader_ticket_number` int(11) DEFAULT NULL,
  `issue_date` date NOT NULL,
  `planned_return_date` date NOT NULL,
  `actual_return_date` date DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Дамп данных таблицы `Registration`
--

INSERT INTO `Registration` (`id`, `inventory_number`, `reader_ticket_number`, `issue_date`, `planned_return_date`, `actual_return_date`) VALUES
(1, 1000, 1, '2024-01-01', '2024-02-01', '2024-01-30'),
(2, 1001, 2, '2024-01-02', '2024-02-02', '2024-01-31'),
(3, 1002, 3, '2024-01-03', '2024-02-03', '2024-02-01'),
(4, 1003, 4, '2024-01-04', '2024-02-04', NULL),
(5, 1004, 5, '2024-01-05', '2024-02-05', NULL),
(6, 1005, 1, '2024-01-06', '2024-02-06', NULL),
(7, 1006, 2, '2024-01-07', '2024-02-07', NULL),
(8, 1007, 3, '2024-01-08', '2024-02-08', NULL),
(9, 1008, 4, '2024-01-09', '2024-02-09', NULL),
(10, 1009, 5, '2024-01-10', '2024-02-10', NULL),
(11, 1010, 1, '2024-01-11', '2024-02-11', NULL),
(12, 1011, 2, '2024-01-12', '2024-02-12', NULL),
(13, 1012, 3, '2024-01-13', '2024-02-13', NULL),
(14, 1013, 4, '2024-01-14', '2024-02-14', NULL),
(15, 1014, 5, '2024-01-15', '2024-02-15', NULL),
(16, 1015, 1, '2024-01-16', '2024-02-16', NULL),
(17, 1016, 2, '2024-01-17', '2024-02-17', NULL),
(18, 1017, 3, '2024-01-18', '2024-02-18', NULL),
(19, 1018, 4, '2024-01-19', '2024-02-19', NULL),
(20, 1019, 5, '2024-01-20', '2024-02-20', NULL);

-- --------------------------------------------------------

--
-- Структура таблицы `Role`
--

CREATE TABLE `Role` (
  `role_id` int(11) NOT NULL,
  `role_name` varchar(50) COLLATE utf8mb4_unicode_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Дамп данных таблицы `Role`
--

INSERT INTO `Role` (`role_id`, `role_name`) VALUES
(1, 'Зав. библиотекой'),
(2, 'Читатели'),
(3, 'Библиотекари');

-- --------------------------------------------------------

--
-- Структура таблицы `User`
--

CREATE TABLE `User` (
  `user_id` int(11) NOT NULL,
  `username` varchar(50) COLLATE utf8mb4_unicode_ci NOT NULL,
  `password` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `role_id` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Дамп данных таблицы `User`
--

INSERT INTO `User` (`user_id`, `username`, `password`, `role_id`) VALUES
(1, 'bibliotekar', 'bibliotekar', 1),
(2, 'listing', 'listing', 2),
(3, 'ivanova', 'ivanova', 3),
(4, 'petrova', 'petrova', 3);

--
-- Индексы сохранённых таблиц
--

--
-- Индексы таблицы `Book`
--
ALTER TABLE `Book`
  ADD PRIMARY KEY (`inventory_number`);

--
-- Индексы таблицы `Reader`
--
ALTER TABLE `Reader`
  ADD PRIMARY KEY (`reader_ticket_number`);

--
-- Индексы таблицы `Registration`
--
ALTER TABLE `Registration`
  ADD PRIMARY KEY (`id`),
  ADD KEY `inventory_number` (`inventory_number`),
  ADD KEY `reader_ticket_number` (`reader_ticket_number`);

--
-- Индексы таблицы `Role`
--
ALTER TABLE `Role`
  ADD PRIMARY KEY (`role_id`);

--
-- Индексы таблицы `User`
--
ALTER TABLE `User`
  ADD PRIMARY KEY (`user_id`),
  ADD UNIQUE KEY `username` (`username`),
  ADD KEY `role_id` (`role_id`);

--
-- AUTO_INCREMENT для сохранённых таблиц
--

--
-- AUTO_INCREMENT для таблицы `Registration`
--
ALTER TABLE `Registration`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=21;

--
-- AUTO_INCREMENT для таблицы `Role`
--
ALTER TABLE `Role`
  MODIFY `role_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT для таблицы `User`
--
ALTER TABLE `User`
  MODIFY `user_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- Ограничения внешнего ключа сохраненных таблиц
--

--
-- Ограничения внешнего ключа таблицы `Registration`
--
ALTER TABLE `Registration`
  ADD CONSTRAINT `registration_ibfk_1` FOREIGN KEY (`inventory_number`) REFERENCES `Book` (`inventory_number`),
  ADD CONSTRAINT `registration_ibfk_2` FOREIGN KEY (`reader_ticket_number`) REFERENCES `Reader` (`reader_ticket_number`);

--
-- Ограничения внешнего ключа таблицы `User`
--
ALTER TABLE `User`
  ADD CONSTRAINT `user_ibfk_1` FOREIGN KEY (`role_id`) REFERENCES `Role` (`role_id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
