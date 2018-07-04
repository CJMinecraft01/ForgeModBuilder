using System.Collections.Generic;

namespace ForgeModBuilder.Gradle
{

    public class GObject
    {
        public string Name { get; set; }
        public int NestedLevel { get; set; } = -1;

        public override string ToString()
        {
            return GetTab() + Name;
        }

        protected string GetTab()
        {
            string tab = "";
            if (NestedLevel > 0)
            {
                tab = new string(' ', NestedLevel * 4);
            }
            return tab;
        }
    }

    public class GVariable : GObject
    {
        public object Value { get; private set; }

        public GVariable(string name, object value)
        {
            Name = name;
            Value = value;
        }

        public override string ToString()
        {
            return GetTab() + Name + " = " + Value;
        }
    }

    public class GBlock : GObject
    {
        public List<GObject> Children { get; private set; } = new List<GObject>();

        public override string ToString()
        {
            string tab = GetTab();
            string text = tab + Name + " {\n";
            foreach (GObject child in Children)
            {
                text += child + "\n";
            }
            text += tab + "}";
            return text;
        }
    }

    public class GTask : GBlock
    {
        public GTask(GBlock block)
        {
            Name = block.Name;
            Children.AddRange(block.Children);
            NestedLevel = block.NestedLevel;
        }

        public override string ToString()
        {
            string tab = GetTab();
            string text = tab + "task " + Name + " {\n";
            foreach (GObject child in Children)
            {
                text += child + "\n";
            }
            text += tab + "}";
            return text;
        }
    }

    public static class GradleExtensions
    {
        public static bool ContainsObject(this List<GObject> list, string name)
        {
            foreach (GObject child in list)
            {
                if (child.Name == name)
                {
                    return true;
                }
            }
            return false;
        }

        public static GObject GetObject(this List<GObject> list, string name)
        {
            foreach (GObject child in list)
            {
                if (child.Name == name)
                {
                    return child;
                }
            }
            return null;
        }
    }

}
