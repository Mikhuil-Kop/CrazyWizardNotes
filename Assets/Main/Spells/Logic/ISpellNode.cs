
/// <summary>
/// Часть заклинания. Отвечает за иерархию частей в заклинании. Сущестует все, пока заклинание собрано.
/// </summary>
public interface ISpellNode
{
    /// <summary>
    /// Создать оператор заклинания.
    /// </summary>
    ISpellOperator CreateSpellOperator();
}
