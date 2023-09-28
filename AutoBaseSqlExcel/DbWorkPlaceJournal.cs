using System;
using System.Drawing;

namespace AutoBaseSql
{
	/// <summary>
	/// ������ � ������� ��� ������������� �������� ����� �� ������������ ����
	/// ������������, ��� ���������������� �������� � ����, ����� ����� DbJournal
	/// </summary>
	public class DbWorkPlaceJournal
	{
		private DbWorkPlace		workPlace;							// ������� ����� (����)
		private DateTime		date;								// ���� ������� � �������
		private DbJournal[]		journalList = new DbJournal[96];	// ���� ������ (�� ����� �����) (15 �������� ���������)

		private static int readerLength;			// ���������� ����� ��� ���������� �� ���� ������
		public static int ReaderLength
		{
			get{ return readerLength;}
		}

		#region �����������
		public DbWorkPlaceJournal(DbWorkPlace sourceWorkPlace)
		{
			workPlace = sourceWorkPlace;
			workPlace.MakeHour();
		}
		#endregion

		#region ����������� �� ������
		public void Draw(Graphics graph, int offsetX, int offsetY)
		{
			// ������ ��������� �������� ����� � �����
			workPlace.DrawTitle(graph, offsetX, offsetY);
			// ������ ���������
			workPlace.DrawHours(graph, journalList, offsetX, offsetY);
		}
		#endregion

		public DbWorkPlace WorkPlace
		{
			get
			{
				return workPlace;
			}
		}

		public void ChangeStatus(Point pnt, int offsetX, int offsetY)
		{
			workPlace.ChangeStatus(pnt, offsetX, offsetY);
		}

		public int GetIndex(Point pnt, int offsetX, int offsetY)
		{
			return workPlace.GetIndex(pnt, offsetX, offsetY);
		}

		public DbJournal GetJournalAt(int index)
		{
			return journalList[index];
		}

		public bool IsAvaliableAt(int index)
		{
			return workPlace.IsAvalaiableAt(index);
		}

		public DbJournal SetJournalAt(int index, DbJournal journal)
		{
			return journalList[index] = journal;
		}

		public DateTime Date
		{
			get
			{
				return date;
			}
			set
			{
				date = value;
			}
		}

		#region �������� ������
		public bool Add(int start, int end)
		{
			if(DbJournal.Add(journalList, start, end) == false)
			{
				for(int index = start; index <= end; index++) journalList[index] = null;
				return false;
			}
			return true;
		}

		public bool Remove(DbJournal journal)
		{
			if(DbJournal.Remove(journal) == true)
			{
				for(int index = 0; index < 96; index++)
				{
					if((journalList[index] != null)&&(journalList[index].CodeGroup == journal.CodeGroup))
					{
						journalList[index] = null;
					}
				}
				return true;
			}
			return false;
		}

		public bool Update(DbJournal journal)
		{
			if(DbJournal.Update(journal) == true)
			{
				for(int index = 0; index < 96; index++)
				{
					if((journalList[index] != null)&&(journalList[index].CodeGroup == journal.CodeGroup))
					{
						journalList[index].SetJournal(journal);
					}
				}
				return true;
			}
			return false;
		}
		#endregion

		public void GetDateJournal(DateTime sourceDate)
		{
			date = sourceDate;
			for(int index = 0; index < 96; index++) journalList[index] = null;
			DbJournal.Select(journalList, date, workPlace.Code);
		}

		public long GetGroupCode(int index)
		{
			// ��������� ��� � ���� ������ - ��� ������� + ��� �������� ����� * 100
			if((index < 0)||(index > 95)) return 0;
			if(journalList[index] == null) return 0;
			return journalList[index].CodeGroup + journalList[index].CodeWorkPlace * 100;
		}

		public string GetToolTip(int index)
		{
			if((index < 0)||(index > 95)) return "";
			if(journalList[index] == null) return "";
			return journalList[index].ToolTip;
		}
	}
}
