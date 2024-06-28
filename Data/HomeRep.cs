namespace Lab1.Data;

/// <summary>
/// Простой POCO класс представляющий строку данных
/// </summary>
public class HomeRep
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public Guid? Id { get; set; }

    public string Fullname { get; set; }
    public string Job { get; set; }
    public bool Fired { get; set; }
    public decimal Salary { get; set; }
}