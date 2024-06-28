using System.Text.Json;

namespace Lab1.Data;

/// <summary>
/// Класс репозиторий для сохранения данных о Сотрудниках
/// </summary>
public class EmployeeRepository
{
    private static List<Employee>? _employees;

    private static string _dataLocation = "";
    private static string _storagePath = "";

    public void Init()
    {
        lock (this)
        {
            _storagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Storage");
            _dataLocation = Path.Combine(_storagePath, "Data.json");

            _employees = new List<Employee>();
            if (!Path.Exists(_dataLocation)) return;

            using var reader = new StreamReader(_dataLocation);
            var data = reader.ReadToEnd();
            try
            {
                _employees = JsonSerializer.Deserialize<List<Employee>>(data);
            }
            catch
            {
                _employees = new List<Employee>();
            }
        }
    }

    /// <summary>
    /// Возврщает все элементы списка
    /// </summary>
    /// <returns>Список сотрудников</returns>
    public IList<Employee>? List()
    {
        return _employees;
    }

    private void _syncChanges()
    {
        Directory.CreateDirectory(_storagePath);
        if (Path.Exists(_dataLocation)) File.Delete(_dataLocation);
        var content = JsonSerializer.Serialize(_employees);
        using var writer = new StreamWriter(_dataLocation);
        writer.Write(content);
    }

    /// <summary>
    /// Добавляет новую запись
    /// </summary>
    /// <param name="data">Новая запись о сотруднике</param>
    public void Add(Employee data)
    {
        lock (this)
        {
            data.Id ??= Guid.NewGuid();
            _employees.Add(data);
            _syncChanges();
        }
    }

    /// <summary>
    /// Обновляет запись о сотруднике
    /// </summary>
    /// <param name="data">Запись о сотруднике</param>
    public void Update(Employee data)
    {
        lock (this)
        {
            var index = _employees.FindIndex(_employee => _employee.Id == data.Id);
            if (index == -1) return;
            _employees[index] = data;
            _syncChanges();
        }
    }

    public Employee? GetById(Guid id)
    {
        return _employees?.FirstOrDefault(employee => employee.Id == id);
    }

    /// <summary>
    /// Удаляет запись о сотруднике по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор записи о сотруднике</param>
    public void Remove(Guid? id)
    {
        if (id == null) return;
        lock (this)
        {
            _employees = _employees.Where(employee => employee.Id != id).ToList();
            _syncChanges();
        }
    }
}