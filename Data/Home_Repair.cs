using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Lab1.Data
{
    /// <summary>
    /// Класс репозиторий для сохранения данных о задачах для домашнего ремонта
    /// </summary>
    public class Home_Repair
    {
        private static List<HomeRep>? _tasks;
        private static string _dataLocation = "";
        private static string _storagePath = "";

        public void Init()
        {
            lock (this)
            {
                _storagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Storage");
                _dataLocation = Path.Combine(_storagePath, "Data.json");

                _tasks = new List<HomeRep>();
                if (!Path.Exists(_dataLocation)) return;

                using var reader = new StreamReader(_dataLocation);
                var data = reader.ReadToEnd();
                try
                {
                    _tasks = JsonSerializer.Deserialize<List<HomeRep>>(data);
                }
                catch
                {
                    _tasks = new List<HomeRep>();
                }
            }
        }

        /// <summary>
        /// Возвращает все элементы списка
        /// </summary>
        /// <returns>Список задач</returns>
        public IList<HomeRep>? List()
        {
            return _tasks;
        }

        private void _syncChanges()
        {
            Directory.CreateDirectory(_storagePath);
            if (Path.Exists(_dataLocation)) File.Delete(_dataLocation);
            var content = JsonSerializer.Serialize(_tasks);
            using var writer = new StreamWriter(_dataLocation);
            writer.Write(content);
        }

        /// <summary>
        /// Добавляет новую запись
        /// </summary>
        /// <param name="data">Новая запись о задаче</param>
        public void Add(HomeRep data)
        {
            lock (this)
            {
                data.Id ??= Guid.NewGuid();
                _tasks.Add(data);
                _syncChanges();
            }
        }

        /// <summary>
        /// Обновляет запись о задаче
        /// </summary>
        /// <param name="data">Запись о задаче</param>
        public void Update(HomeRep data)
        {
            lock (this)
            {
                var index = _tasks.FindIndex(task => task.Id == data.Id);
                if (index == -1) return;
                _tasks[index] = data;
                _syncChanges();
            }
        }

        public HomeRep? GetById(Guid id)
        {
            return _tasks?.FirstOrDefault(task => task.Id == id);
        }

        /// <summary>
        /// Удаляет запись о задаче по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор записи о задаче</param>
        public void Remove(Guid? id)
        {
            if (id == null) return;
            lock (this)
            {
                _tasks = _tasks.Where(task => task.Id != id).ToList();
                _syncChanges();
            }
        }
    }
}
