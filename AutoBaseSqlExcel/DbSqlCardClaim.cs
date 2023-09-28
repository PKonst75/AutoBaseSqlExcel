using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Collections;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbSqlCardClaim.
	/// </summary>
	public class DbSqlCardClaim
	{
		public static SqlCommand insert;
		public static SqlCommand select;
		public static SqlCommand remove;

		public DbSqlCardClaim()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public static void Init(SqlConnection connection)
		{
			select = new SqlCommand("ÊÀĞÒÎ×ÊÀ_ÇÀßÂÊÀ_ÂÛÁÎĞÊÀ", connection);
			select.Parameters.Add("@card_number", SqlDbType.BigInt);
			select.Parameters.Add("@card_year", SqlDbType.Int);
			select.CommandType = CommandType.StoredProcedure;
			
			insert = new SqlCommand("ÊÀĞÒÎ×ÊÀ_ÇÀßÂÊÀ_ÄÎÁÀÂËÅÍÈÅ", connection);
			insert.Parameters.Add("@card_number", SqlDbType.BigInt);
			insert.Parameters.Add("@card_year", SqlDbType.Int);
			insert.Parameters.Add("@code_claim", SqlDbType.BigInt);
			insert.Parameters.Add("@position", SqlDbType.BigInt);
			insert.CommandType = CommandType.StoredProcedure;
			insert.Parameters["@position"].Direction = ParameterDirection.Output;
			DbSql.SetReturnError(insert);

			remove = new SqlCommand("ÊÀĞÒÎ×ÊÀ_ÇÀßÂÊÀ_ÓÄÀËÅÍÈÅ", connection);
			remove.Parameters.Add("@card_number", SqlDbType.BigInt);
			remove.Parameters.Add("@card_year", SqlDbType.Int);
			remove.Parameters.Add("@position", SqlDbType.BigInt);
			remove.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(remove);
		}

		public static DtCardClaim Insert(DtCardClaim element)
		{
			insert.Parameters["@card_number"].Value = (long)element.GetData("ÍÎÌÅĞ_ÊÀĞÒÎ×ÊÀ");
			insert.Parameters["@card_year"].Value = (int)element.GetData("ÃÎÄ_ÊÀĞÒÎ×ÊÀ");
			insert.Parameters["@code_claim"].Value = (long)element.GetData("ÑÑÛËÊÀ_ÊÎÄ_ÇÀßÂÊÀ");
			if(DbSql.ExecuteCommandError(insert) != true) return null;
			element.SetData("ÏÎÇÈÖÈß", (long)insert.Parameters["@position"].Value);
			return element;
		}

		public static object MakeElement(SqlDataReader reader)
		{
			DtCardClaim element			= new DtCardClaim();
			element.SetData("ÍÎÌÅĞ_ÊÀĞÒÎ×ÊÀ", DbSql.GetValueLong(reader, "ÍÎÌÅĞ_ÊÀĞÒÎ×ÊÀ"));
			element.SetData("ÃÎÄ_ÊÀĞÒÎ×ÊÀ", DbSql.GetValueInt(reader, "ÃÎÄ_ÊÀĞÒÎ×ÊÀ"));
			element.SetData("ÏÎÇÈÖÈß", DbSql.GetValueLong(reader, "ÏÎÇÈÖÈß"));
			element.SetData("ÑÑÛËÊÀ_ÊÎÄ_ÇÀßÂÊÀ", DbSql.GetValueLong(reader, "ÑÑÛËÊÀ_ÊÎÄ_ÇÀßÂÊÀ"));
			element.SetData("ÄÅÔÅÊÒ", DbSql.GetValueBool(reader, "ÄÅÔÅÊÒ"));
			element.SetData("ÏÎÄÒÂÅĞÆÄÅÍÎ", DbSql.GetValueBool(reader, "ÏÎÄÒÂÅĞÆÄÅÍÎ"));
			element.SetData("ÃÀĞÀÍÒÈß", DbSql.GetValueBool(reader, "ÃÀĞÀÍÒÈß"));
			element.SetData("ÏĞÈÌÅ×ÀÍÈÅ", DbSql.GetValueString(reader, "ÏĞÈÌÅ×ÀÍÈÅ"));
			if(DbSql.IsValueNULL(reader, "ÄÀÒÀ_ÑÎÃËÀÑÎÂÀÍÈÅ") == true)
			{
				element.SetData("ÅÑÒÜ_ÄÀÒÀ_ÑÎÃËÀÑÎÂÀÍÈß", false);
				element.SetData("ÄÀÒÀ_ÑÎÃËÀÑÎÂÀÍÈß", DateTime.Now);
			}
			else
			{
				element.SetData("ÅÑÒÜ_ÄÀÒÀ_ÑÎÃËÀÑÎÂÀÍÈß", true);
				element.SetData("ÄÀÒÀ_ÑÎÃËÀÑÎÂÀÍÈß", DbSql.GetValueDate(reader, "ÄÀÒÀ_ÑÎÃËÀÑÎÂÀÍÈß"));
			}
			element.SetData("ÍÀÈÌÅÍÎÂÀÍÈÅ_ÇÀßÂÊÀ", DbSql.GetValueString(reader, "ÍÀÈÌÅÍÎÂÀÍÈÅ_ÇÀßÂÊÀ"));
			
			return (object)element;
		}

		public static ListViewItem MakeLVItem(SqlDataReader reader)
		{
			DtCardClaim element = (DtCardClaim)MakeElement(reader);
			ListViewItem item = new ListViewItem();
			if(element != null)
			{
				element.SetLVItem(item);
			}
			else
			{
				item.Tag			= 0;
				item.Text			= "Îøèáêà";
			}
			return item;
		}
		public static ListViewItem MakeLVItemFromElement(object srcElement)
		{
			DtCardClaim element = (DtCardClaim)srcElement;
			ListViewItem item = new ListViewItem();
			if (element != null)
			{
				element.SetLVItem(item);
			}
			else
			{
				item.Tag = 0;
				item.Text = "Îøèáêà";
			}
			return item;
		}
		public static void SelectInList(ListView list, long card_number, int card_year)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî ìàñêå
			select.Parameters["@card_number"].Value	= (long)card_number;
			select.Parameters["@card_year"].Value	= (int)card_year;
			//DbSql.FillList(list, select, new DbSql.DelegateMakeLVItem(MakeLVItem));
			ArrayList array = new ArrayList();
			DbSql.FillArray(array, select, new DbSql.DelegateMakeElement(MakeElement));
			DbSql.FillListFromArray(list, array, new DbSql.DelegateMakeLVItemFromElement(MakeLVItemFromElement));

		}

		public static void SelectInArray(ArrayList array, long card_number, int card_year)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî ìàñêå
			select.Parameters["@card_number"].Value	= (long)card_number;
			select.Parameters["@card_year"].Value	= (int)card_year;
			DbSql.FillArray(array, select, new DbSql.DelegateMakeElement(MakeElement));
		}

		public static bool Remove(DtCard card, long position)
		{
			remove.Parameters["@card_number"].Value = (long)card.GetData("ÍÎÌÅĞ_ÊÀĞÒÎ×ÊÀ");
			remove.Parameters["@card_year"].Value = (int)card.GetData("ÃÎÄ_ÊÀĞÒÎ×ÊÀ");
			remove.Parameters["@position"].Value = (long)position;
			return DbSql.ExecuteCommandError(remove);
		}
		public static ArrayList Select(DtCard srcCard)
        {
			ArrayList array = new ArrayList();
			SelectInArray(array, srcCard.Number, srcCard.Year);
			return array;
        }

		public static void SelectInListNoDatabase(ListView list, DtCard srcCard)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî ìàñêå
			ArrayList array = srcCard.Claims.Collection;
			DbSql.FillListFromArray(list, array, new DbSql.DelegateMakeLVItemFromElement(MakeLVItemFromElement));

		}
	}
}
