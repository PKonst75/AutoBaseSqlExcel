using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace AutoBaseSql
{
	public interface ListViewItemSetting
	{
		void SetListViewItem(ListViewItem item);
	}
    class WindowsFormCommon
    {
		public static ListViewItem GetListItemSelected(ListView srcListView) // Возвращаем первый выбранный элемент списка ListView
		{
			if (srcListView == null) return null;
			if (srcListView.SelectedItems == null) return null;
			if (srcListView.SelectedItems.Count == 0) return null;
			return srcListView.SelectedItems[0];
		}
		public static object GetListItemSelectedTag(ListView srcListView) // Возвращаем TAG первого выбранного элемента списка ListView
		{
			ListViewItem item = GetListItemSelected(srcListView);
			if (item == null) return null;
			return item.Tag;
        }
		public static long GetListItemSelectedTagLong(ListView srcListView)
        {
			object tag = GetListItemSelectedTag(srcListView);
			if (tag == null) return 0;
			if (tag.GetType() != typeof(long)) return 0;
			return (long)tag;
        }
		public static long GetListItemTagLong(ListViewItem srcListViewItem)
		{
			if(srcListViewItem == null) return 0;
			object tag = srcListViewItem.Tag;
			if (tag == null) return 0;
			if (tag.GetType() != typeof(long)) return 0;
			return (long)tag;
		}

		public static void FillListView(ListView srcList, ArrayList collection)
        {
			srcList.Items.Clear();
			foreach(object o in collection)
            {
				ListViewItemSetting element = (ListViewItemSetting)o;
				ListViewItem item = srcList.Items.Add("Не заполненные данные");
				element.SetListViewItem(item);
            }
        }
	}
}
