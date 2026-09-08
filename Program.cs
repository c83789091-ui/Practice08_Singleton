// Подключаем стандартную библиотеку System.
// Она нужна для вывода текста в консоль и получения текущего времени.
using System;

// Подключаем коллекции.
// List<string> используется для хранения истории сообщений логгера.
using System.Collections.Generic;

// Выводим название практической, паттерна и варианта.
Console.WriteLine("Практическая 8. Singleton. Вариант 1 - логирование событий.");
Console.WriteLine();

// Получаем первый доступ к логгеру.
// Мы не пишем new Logger(), потому что конструктор закрытый.
// Единственный правильный способ получить логгер - Logger.GetInstance().
Logger firstLogger = Logger.GetInstance();

// Получаем второй доступ к логгеру.
// По паттерну Singleton здесь не создается новый объект.
// Метод GetInstance() возвращает тот же самый логгер, который уже был создан.
Logger secondLogger = Logger.GetInstance();

// Записываем первое сообщение через первую переменную.
firstLogger.Log("Приложение запущено");

// Записываем второе сообщение через вторую переменную.
// Хотя переменная другая, объект логгера внутри тот же самый.
secondLogger.Log("Пользователь выполнил действие");

// Записываем третье сообщение снова через первую переменную.
firstLogger.Log("Приложение завершает работу");

Console.WriteLine();

// Проверяем, что firstLogger и secondLogger указывают на один и тот же объект.
// ReferenceEquals возвращает True, если две переменные ссылаются на один объект в памяти.
Console.WriteLine("Обе переменные указывают на один объект: " + ReferenceEquals(firstLogger, secondLogger));

// Выводим всю историю сообщений.
// Так видно, что сообщения от обеих переменных попали в один общий журнал.
firstLogger.PrintHistory();

Console.WriteLine();
Console.WriteLine("Нажмите любую клавишу для выхода...");
Console.ReadKey();

// Класс Logger - это логгер, то есть объект для записи событий программы.
// Он сделан по паттерну Singleton, поэтому в программе может существовать только один Logger.
class Logger
{
    // Статическое поле хранит единственный экземпляр Logger.
    // static означает, что поле принадлежит не конкретному объекту, а самому классу Logger.
    // Сначала здесь null, потому что логгер еще не создан.
    private static Logger? instance;

    // Список для хранения всех сообщений.
    // private означает, что список доступен только внутри класса Logger.
    private List<string> messages = new List<string>();

    // Закрытый конструктор.
    // Из-за private нельзя написать new Logger() снаружи класса.
    // Это важная часть Singleton: объект нельзя создать напрямую.
    private Logger()
    {
    }

    // Метод получения единственного экземпляра логгера.
    // Через него вся программа получает доступ к Logger.
    public static Logger GetInstance()
    {
        // Если логгер еще не создан, создаем его один раз.
        if (instance == null)
        {
            instance = new Logger();
        }

        // Возвращаем готовый логгер.
        // При следующих вызовах вернется тот же самый объект.
        return instance;
    }

    // Метод Log добавляет новое сообщение в журнал.
    public void Log(string message)
    {
        // Создаем строку записи: время + текст сообщения.
        string record = DateTime.Now.ToString("HH:mm:ss") + " - " + message;

        // Добавляем запись в общий список сообщений.
        messages.Add(record);

        // Сразу выводим запись на экран.
        Console.WriteLine(record);
    }

    // Метод PrintHistory выводит все сообщения, которые были записаны раньше.
    public void PrintHistory()
    {
        Console.WriteLine();
        Console.WriteLine("История сообщений:");

        // Проходим по каждому сообщению в списке messages.
        foreach (string message in messages)
        {
            // Выводим одно сообщение.
            Console.WriteLine(message);
        }
    }
}
