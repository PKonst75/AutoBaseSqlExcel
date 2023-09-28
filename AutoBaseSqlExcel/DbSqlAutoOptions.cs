using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Collections;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbSqlAutoOptions.
	/// </summary>
	public class DbSqlAutoOptions:DbSql
	{
		public static SqlCommand insert_option;
		public static SqlCommand select_option;
		public static SqlCommand select_option_find;
		public static SqlCommand select_option_group_complect;
		public static SqlCommand set_option_find;
		public static SqlCommand remove_option_find;

		public static SqlCommand insert_option_variant;
		public static SqlCommand select_option_variant;

		public static SqlCommand select_group;

		public static SqlCommand insert_complect;
		public static SqlCommand remove_complect;
		public static SqlCommand select_option_complect;

		public DbSqlAutoOptions()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public static void Init(SqlConnection connection)
		{
			select_option = new SqlCommand("¿¬“ŒÃŒ¡»À‹_Œœ÷»ﬂ_¬€¡Œ– ¿", connection);
			select_option.CommandType = CommandType.StoredProcedure;

			select_option_find = new SqlCommand("¿¬“ŒÃŒ¡»À‹_Œœ÷»ﬂ_¬€¡Œ– ¿_œŒ»— ", connection);
			select_option_find.CommandType = CommandType.StoredProcedure;

			select_option_group_complect = new SqlCommand("¿¬“ŒÃŒ¡»À‹_Œœ÷»ﬂ_¬€¡Œ– ¿_ ŒÃœÀ≈ “¿÷»ﬂ_√–”œœ¿", connection);
			select_option_group_complect.Parameters.Add("@code_group", SqlDbType.BigInt);
			select_option_group_complect.Parameters.Add("@code_model", SqlDbType.BigInt);
			select_option_group_complect.Parameters.Add("@code_model_variant", SqlDbType.BigInt);
			select_option_group_complect.CommandType = CommandType.StoredProcedure;

			select_group = new SqlCommand("¿¬“ŒÃŒ¡»À‹_Œœ÷»ﬂ_√–”œœ¿_¬€¡Œ– ¿", connection);
			select_group.CommandType = CommandType.StoredProcedure;

			select_option_variant = new SqlCommand("¿¬“ŒÃŒ¡»À‹_Œœ÷»ﬂ_¬¿–»¿Õ“_¬€¡Œ– ¿", connection);
			select_option_variant.Parameters.Add("@code_option", SqlDbType.BigInt);
			select_option_variant.CommandType = CommandType.StoredProcedure;

			select_option_complect = new SqlCommand("¿¬“ŒÃŒ¡»À‹_Œœ÷»ﬂ_¬€¡Œ– ¿_ ŒÃœÀ≈ “¿÷»ﬂ", connection);
			select_option_complect.Parameters.Add("@code_model", SqlDbType.BigInt);
			select_option_complect.Parameters.Add("@code_model_variant", SqlDbType.BigInt);
			select_option_complect.CommandType = CommandType.StoredProcedure;

			insert_option = new SqlCommand("¿¬“ŒÃŒ¡»À‹_Œœ÷»ﬂ_ƒŒ¡¿¬À≈Õ»≈", connection);
			insert_option.Parameters.Add("@code", SqlDbType.BigInt);
			insert_option.Parameters.Add("@name", SqlDbType.VarChar);
			insert_option.Parameters.Add("@code_group", SqlDbType.BigInt);
			insert_option.CommandType = CommandType.StoredProcedure;
			insert_option.Parameters["@code"].Direction = ParameterDirection.Output;
			DbSql.SetReturnError(insert_option);

			set_option_find = new SqlCommand("¿¬“ŒÃŒ¡»À‹_Œœ÷»ﬂ_¬_œŒ»— ", connection);
			set_option_find.Parameters.Add("@code", SqlDbType.BigInt);
			set_option_find.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(set_option_find);

			remove_option_find = new SqlCommand("¿¬“ŒÃŒ¡»À‹_Œœ÷»ﬂ_»«_œŒ»— ¿", connection);
			remove_option_find.Parameters.Add("@code", SqlDbType.BigInt);
			remove_option_find.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(remove_option_find);

			insert_option_variant = new SqlCommand("¿¬“ŒÃŒ¡»À‹_Œœ÷»ﬂ_¬¿–»¿Õ“_ƒŒ¡¿¬À≈Õ»≈", connection);
			insert_option_variant.Parameters.Add("@code", SqlDbType.BigInt);
			insert_option_variant.Parameters.Add("@name", SqlDbType.VarChar);
			insert_option_variant.Parameters.Add("@code_option", SqlDbType.BigInt);
			insert_option_variant.CommandType = CommandType.StoredProcedure;
			insert_option_variant.Parameters["@code"].Direction = ParameterDirection.Output;
			DbSql.SetReturnError(insert_option_variant);

			insert_complect = new SqlCommand("¿¬“ŒÃŒ¡»À‹_ ŒÃœÀ≈ “¿÷»ﬂ_Œœ»—¿Õ»≈_ƒŒ¡¿¬À≈Õ»≈", connection);
			insert_complect.Parameters.Add("@code_model", SqlDbType.BigInt);
			insert_complect.Parameters.Add("@code_model_variant", SqlDbType.BigInt);
			insert_complect.Parameters.Add("@code_option", SqlDbType.BigInt);
			insert_complect.Parameters.Add("@code_option_variant", SqlDbType.BigInt);
			insert_complect.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(insert_complect);

			remove_complect = new SqlCommand("¿¬“ŒÃŒ¡»À‹_ ŒÃœÀ≈ “¿÷»ﬂ_Œœ»—¿Õ»≈_”ƒ¿À≈Õ»≈", connection);
			remove_complect.Parameters.Add("@code_model", SqlDbType.BigInt);
			remove_complect.Parameters.Add("@code_model_variant", SqlDbType.BigInt);
			remove_complect.Parameters.Add("@code_option", SqlDbType.BigInt);
			remove_complect.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(remove_complect);
		}

		#region —œ»—Œ  Œœ÷»…
		public static ListViewItem MakeLVItemOption(SqlDataReader reader)
		{
			DtAutoOption element = (DtAutoOption)MakeElementOption(reader);
			ListViewItem item = new ListViewItem();
			if(element != null)
			{
				element.SetLVItem(item);
			}
			else
			{
				item.Tag			= 0;
				item.Text			= "Œ¯Ë·Í‡";
			}
			return item;
		}
		public static void SelectInListOption(ListView list)
		{
			// œÓ‰„ÓÚÓ‚Í‡ ÍÓÏ‡Ì‰˚ ÔÓËÒÍ‡
			DbSql.FillList(list, select_option, new DbSql.DelegateMakeLVItem(MakeLVItemOption));
		}
		public static void SelectInListOptionFind(ListView list)
		{
			// œÓ‰„ÓÚÓ‚Í‡ ÍÓÏ‡Ì‰˚ ÔÓËÒÍ‡
			DbSql.FillList(list, select_option_find, new DbSql.DelegateMakeLVItem(MakeLVItemOption));
		}
		public static object MakeElementOption(SqlDataReader reader)
		{
			DtAutoOption element			= new DtAutoOption();
			element.SetData(" Œƒ", DbSql.GetValueLong(reader, " Œƒ"));
			element.SetData("Œœ÷»ﬂ_Õ¿»Ã≈ÕŒ¬¿Õ»≈", DbSql.GetValueString(reader, "Œœ÷»ﬂ_Õ¿»Ã≈ÕŒ¬¿Õ»≈"));
			element.SetData("——€À ¿_ Œƒ_Œœ÷»ﬂ_√–”œœ¿", DbSql.GetValueLong(reader, "——€À ¿_ Œƒ_Œœ÷»ﬂ_√–”œœ¿"));
			
			return (object)element;
		}

		public static DtAutoOption InsertOption(DtAutoOption element)
		{
			insert_option.Parameters["@name"].Value = (string)element.GetData("Œœ÷»ﬂ_Õ¿»Ã≈ÕŒ¬¿Õ»≈");
			insert_option.Parameters["@code_group"].Value = (long)element.GetData("——€À ¿_ Œƒ_Œœ÷»ﬂ_√–”œœ¿");
			if(DbSql.ExecuteCommandError(insert_option) != true) return null;
			element.SetData(" Œƒ", (long)insert_option.Parameters["@code"].Value);
			return element;
		}

		public static bool SetOptionFind(long code_option)
		{
			set_option_find.Parameters["@code"].Value = (long)code_option;
			if(DbSql.ExecuteCommandError(set_option_find) != true) return false;
			return true;
		}
		public static bool RemoveOptionFind(long code_option)
		{
			remove_option_find.Parameters["@code"].Value = (long)code_option;
			if(DbSql.ExecuteCommandError(remove_option_find) != true) return false;
			return true;
		}
		#endregion

		#region —œ»—Œ  ¬¿–»¿Õ“Œ¬ Œœ÷»…
		public static ListViewItem MakeLVItemOptionVariant(SqlDataReader reader)
		{
			DtAutoOptionVariant element = (DtAutoOptionVariant)MakeElementOptionVariant(reader);
			ListViewItem item = new ListViewItem();
			if(element != null)
			{
				element.SetLVItem(item);
			}
			else
			{
				item.Tag			= 0;
				item.Text			= "Œ¯Ë·Í‡";
			}
			return item;
		}
		public static void SelectInListVariant(ListView list, long code_option)
		{
			// œÓ‰„ÓÚÓ‚Í‡ ÍÓÏ‡Ì‰˚ ÔÓËÒÍ‡
			select_option_variant.Parameters["@code_option"].Value = (long)code_option;
			DbSql.FillList(list, select_option_variant, new DbSql.DelegateMakeLVItem(MakeLVItemOptionVariant));
		}
		public static object MakeElementOptionVariant(SqlDataReader reader)
		{
			DtAutoOptionVariant element			= new DtAutoOptionVariant();
			element.SetData(" Œƒ", DbSql.GetValueLong(reader, " Œƒ"));
			element.SetData("¬¿–»¿Õ“_Õ¿»Ã≈ÕŒ¬¿Õ»≈", DbSql.GetValueString(reader, "¬¿–»¿Õ“_Õ¿»Ã≈ÕŒ¬¿Õ»≈"));
			element.SetData("——€À ¿_ Œƒ_Œœ÷»ﬂ", DbSql.GetValueLong(reader, "——€À ¿_ Œƒ_Œœ÷»ﬂ"));
			
			return (object)element;
		}

		public static DtAutoOptionVariant InsertOptionVariant(DtAutoOptionVariant element)
		{
			insert_option_variant.Parameters["@name"].Value = (string)element.GetData("¬¿–»¿Õ“_Õ¿»Ã≈ÕŒ¬¿Õ»≈");
			insert_option_variant.Parameters["@code_option"].Value = (long)element.GetData("——€À ¿_ Œƒ_Œœ÷»ﬂ");
			if(DbSql.ExecuteCommandError(insert_option_variant) != true) return null;
			element.SetData(" Œƒ", (long)insert_option_variant.Parameters["@code"].Value);
			return element;
		}
		#endregion

		#region —œ»—Œ  √–”œœ Œœ÷»…
		public static object MakeElementOptionGroup(SqlDataReader reader)
		{
			DtAutoOptionGroup element			= new DtAutoOptionGroup();
			element.SetData(" Œƒ_√–”œœ¿", DbSql.GetValueLong(reader, " Œƒ_√–”œœ¿"));
			element.SetData("Õ¿»Ã≈ÕŒ¬¿Õ»≈_√–”œœ¿", DbSql.GetValueString(reader, "Õ¿»Ã≈ÕŒ¬¿Õ»≈_√–”œœ¿"));
			
			return (object)element;
		}
		public static void SelectInArrayGroup(ArrayList array)
		{
			// œÓ‰„ÓÚÓ‚Í‡ ÍÓÏ‡Ì‰˚ ‚˚·Ó‡
			DbSql.FillArray(array, select_group, new DbSql.DelegateMakeElement(MakeElementOptionGroup));
		}

		#endregion

		#region  ŒÃœÀ≈ “¿÷»ﬂ
		public static object MakeElementOptionComplect(SqlDataReader reader)
		{
			DtAutoOption element			= new DtAutoOption();
			element.SetData(" Œƒ", DbSql.GetValueLong(reader, " Œƒ"));
			element.SetData("Œœ÷»ﬂ_Õ¿»Ã≈ÕŒ¬¿Õ»≈", DbSql.GetValueString(reader, "Œœ÷»ﬂ_Õ¿»Ã≈ÕŒ¬¿Õ»≈"));
			element.SetData("——€À ¿_ Œƒ_Œœ÷»ﬂ_√–”œœ¿", DbSql.GetValueLong(reader, "——€À ¿_ Œƒ_Œœ÷»ﬂ_√–”œœ¿"));

			element.SetData(" Œƒ_ÃŒƒ≈À‹", DbSql.GetValueLong(reader, " Œƒ_ÃŒƒ≈À‹"));
			element.SetData("¬¿–»¿Õ“_Õ¿»Ã≈ÕŒ¬¿Õ»≈", DbSql.GetValueString(reader, "¬¿–»¿Õ“_Õ¿»Ã≈ÕŒ¬¿Õ»≈"));

			// œÓ‰„ÓÚÓ‚Í‡ ‰ÓÔÓÎÌËÚÂÎ¸Ì˚ı ‰‡ÌÌ˚ı ‰Îˇ ÍÓÌÚÓÎˇ
			if (element.tmp_code_model > 0) element.tmp_active = true;
			
			return (object)element;
		}
		public static ListViewItem MakeLVItemOptionComplect(SqlDataReader reader)
		{
			DtAutoOption element = (DtAutoOption)MakeElementOptionComplect(reader);
			ListViewItem item = new ListViewItem();
			if(element != null)
			{
				element.SetLVItemComplect(item);
			}
			else
			{
				item.Tag			= 0;
				item.Text			= "Œ¯Ë·Í‡";
			}
			return item;
		}

		public static bool InsertComplect(long code_model, long code_model_variant, long code_option, long code_option_variant)
		{
			insert_complect.Parameters["@code_model"].Value = code_model;
			insert_complect.Parameters["@code_model_variant"].Value = code_model_variant;
			insert_complect.Parameters["@code_option"].Value = code_option;
			insert_complect.Parameters["@code_option_variant"].Value = code_option_variant;
			if(DbSql.ExecuteCommandError(insert_complect) != true) return false;
			return true;
		}
		public static bool RemoveComplect(long code_model, long code_model_variant, long code_option)
		{
			remove_complect.Parameters["@code_model"].Value = code_model;
			remove_complect.Parameters["@code_model_variant"].Value = code_model_variant;
			remove_complect.Parameters["@code_option"].Value = code_option;
			if(DbSql.ExecuteCommandError(remove_complect) != true) return false;
			return true;
		}
		public static void SelectInListOtionComplect(ListView list, long code_model, long code_model_variant)
		{
			// œÓ‰„ÓÚÓ‚Í‡ ÍÓÏ‡Ì‰˚ ÔÓËÒÍ‡
			select_option_complect.Parameters["@code_model"].Value = (long)code_model;
			select_option_complect.Parameters["@code_model_variant"].Value = (long)code_model_variant;
			DbSql.FillList(list, select_option_complect, new DbSql.DelegateMakeLVItem(MakeLVItemOptionComplect));
		}
		public static void SelectInArrayOptionGroupCpmplect(ArrayList array, long code_group, long code_model, long code_variant)
		{
			// œÓ‰„ÓÚÓ‚Í‡ ÍÓÏ‡Ì‰˚ ÔÓËÒÍ‡
			select_option_group_complect.Parameters["@code_group"].Value = (long)code_group;
			select_option_group_complect.Parameters["@code_model"].Value = (long)code_model;
			select_option_group_complect.Parameters["@code_model_variant"].Value = (long)code_variant;
			DbSql.FillArray(array, select_option_group_complect, new DbSql.DelegateMakeElement(MakeElementOptionComplect));
		}
		#endregion

	}
}
