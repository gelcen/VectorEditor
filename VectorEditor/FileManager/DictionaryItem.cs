using SDK;

namespace VectorEditor.FileManager
{
    /// <summary>
    /// Класс для элемента словаря.
    /// Используется для сериализации 
    /// десериализации в формате JSON
    /// </summary>
    public class DictionaryItem
    {
        public int Key
        {
            get;
            private set;
        }

        public BaseFigure Value
        {
            get;
            private set;
        }

        public DictionaryItem(int key, BaseFigure value)
        {
            Key = key;
            Value = value;
        }
    }
}
