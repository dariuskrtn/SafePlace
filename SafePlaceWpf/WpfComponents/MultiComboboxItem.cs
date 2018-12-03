using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafePlaceWpf.WpfComponents
{
    class MultiComboboxItem<T>
    {
        public bool IsChecked { get; set; }
        public T Item { get; set; }
        public string Name { get; set; }
        public ICollection<T> SelectedItems { get; set; }
    }
}
