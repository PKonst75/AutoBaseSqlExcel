using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Collections;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbSqlAutoComment.
	/// </summary>
	public class DbSqlAutoComment
	{
		public static SqlCommand		insert;
		public static SqlCommand		select_unexe;
		public static SqlCommand		select;
		public static SqlCommand		delete;
		public static SqlCommand		set_executable;
		public static SqlCommand		unset_executable;
		public static SqlCommand		set_exe;

		public DbSqlAutoComment()
		{
		
		}

		public static void Init(SqlConnection connection)
		{
			insert = new SqlCommand("ÀÂÒÎÌÎÁÈËÜ_ÏÐÈÌÅ×ÀÍÈÅ_ÄÎÁÀÂËÅÍÈÅ", connection);
			insert.Parameters.Add("@code_auto", SqlDbType.BigInt);
			insert.Parameters.Add("@number", SqlDbType.BigInt);
			insert.Parameters.Add("@comment", SqlDbType.VarChar);
			insert.CommandType = CommandType.StoredProcedure;
			insert.Parameters["@number"].Direction = ParameterDirection.Output;
			DbSql.SetReturnError(insert);

			select = new SqlCommand("ÀÂÒÎÌÎÁÈËÜ_ÏÐÈÌÅ×ÀÍÈÅ_ÂÛÁÎÐÊÀ", connection);
			select.Parameters.Add("@code_auto", SqlDbType.BigInt);
			select.CommandType = CommandType.StoredProcedure;

			select_unexe = new SqlCommand("ÀÂÒÎÌÎÁÈËÜ_ÏÐÈÌÅ×ÀÍÈÅ_ÂÛÁÎÐÊÀ_ÍÅÂÛÏÎËÍÅÍÍÛÅ", connection);
			select_unexe.Parameters.Add("@code_auto", SqlDbType.BigInt);
			select_unexe.CommandType = CommandType.StoredProcedure;

			set_executable = new SqlCommand("ÀÂÒÎÌÎÁÈËÜ_ÏÐÈÌÅ×ÀÍÈÅ_ÓÑÒÀÍÎÂÈÒÜ_ÈÑÏÎËÍßÅÌÎÅ", connection);
			set_executable.Parameters.Add("@code_auto", SqlDbType.BigInt);
			set_executable.Parameters.Add("@number", SqlDbType.BigInt);
			set_executable.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(set_executable);

			unset_executable = new SqlCommand("ÀÂÒÎÌÎÁÈËÜ_ÏÐÈÌÅ×ÀÍÈÅ_ÑÍßÒÜ_ÈÑÏÎËÍßÅÌÎÅ", connection);
			unset_executable.Parameters.Add("@code_auto", SqlDbType.BigInt);
			unset_executable.Parameters.Add("@number", SqlDbType.BigInt);
			unset_executable.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(unset_executable);

			delete = new SqlCommand("ÀÂÒÎÌÎÁÈËÜ_ÏÐÈÌÅ×ÀÍÈÅ_ÓÄÀËÅÍÈÅ", connection);
			delete.Parameters.Add("@code_auto", SqlDbType.BigInt);
			delete.Parameters.Add("@number", SqlDbType.BigInt);
			delete.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(delete);

			set_exe = new SqlCommand("ÀÂÒÎÌÎÁÈËÜ_ÏÐÈÌÅ×ÀÍÈÅ_ÓÑÒÀÍÎÂÈÒÜ_ÂÛÏÎËÍÅÍÈÅ", connection);
			set_exe.Parameters.Add("@code_auto", SqlDbType.BigInt);
			set_exe.Parameters.Add("@number", SqlDbType.BigInt);
			set_exe.Parameters.Add("@person_exe", SqlDbType.BigInt);
			set_exe.Parameters.Add("@comment_exe", SqlDbType.VarChar);
			set_exe.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(set_exe);

		}

		public static long Insert(long code_auto, string comment)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî áðåíäó
			insert.Parameters["@code_auto"].Value		= (long)code_auto;
			insert.Parameters["@comment"].Value			= (string)comment;
			if(DbSql.ExecuteCommandError(insert) == false) return 0;
			long number	= (long)insert.Parameters["@number"].Value;
			return number;
		}

		public static object MakeElement(SqlDataReader reader)
		{
			DtAutoComment element		= new DtAutoComment();
			element.SetData("ÊÎÄ_ÀÂÒÎÌÎÁÈËÜ", DbSql.GetValueLong(reader, "ÊÎÄ_ÀÂÒÎÌÎÁÈËÜ"));
			element.SetData("ÍÎÌÅÐ", DbSql.GetValueLong(reader, "ÍÎÌÅÐ"));
			element.SetData("ÏÐÈÌÅ×ÀÍÈÅ", DbSql.GetValueString(reader, "ÏÐÈÌÅ×ÀÍÈÅ"));
			element.SetData("ÄÀÒÀ_ÂÛÏÎËÍÅÍÈÅ", DbSql.GetValueDate(reader, "ÄÀÒÀ_ÂÛÏÎËÍÅÍÈÅ"));
			element.SetData("ÂÛÏÎËÍÈË", DbSql.GetValueLong(reader, "ÂÛÏÎËÍÈË"));
			element.SetData("ÒÐÅÁÓÅÒÑß_ÂÛÏÎËÍÅÍÈÅ", DbSql.GetValueBool(reader, "ÒÐÅÁÓÅÒÑß_ÂÛÏÎËÍÅÍÈÅ"));
			element.SetData("ÏÐÈÌÅ×ÀÍÈÅ_ÂÛÏÎËÍÅÍÈÅ", DbSql.GetValueString(reader, "ÏÐÈÌÅ×ÀÍÈÅ_ÂÛÏÎËÍÅÍÈÅ"));
			element.SetData("ÂÛÏÎËÍÈË_ÈÌß", DbSql.GetValueString(reader, "ÂÛÏÎËÍÈË_ÈÌß"));
			return (object)element;
		}

		public static void SelectInArray(ArrayList array, long code_auto)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî ìàñêå
			select.Parameters["@code_auto"].Value = code_auto;
			DbSql.FillArray(array, select, new DbSql.DelegateMakeElement(MakeElement));
		}

		public static bool SetExecutable(long code_auto, long number)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî áðåíäó
			set_executable.Parameters["@code_auto"].Value			= code_auto;
			set_executable.Parameters["@number"].Value				= number;
			return DbSql.ExecuteCommandError(set_executable);
		}

		public static bool Delete(long code_auto, long number)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî áðåíäó
			delete.Parameters["@code_auto"].Value			= code_auto;
			delete.Parameters["@number"].Value				= number;
			return DbSql.ExecuteCommandError(delete);
		}

		public static bool UnsetExecutable(long code_auto, long number)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî áðåíäó
			unset_executable.Parameters["@code_auto"].Value			= code_auto;
			unset_executable.Parameters["@number"].Value			= number;
			return DbSql.ExecuteCommandError(unset_executable);
		}

		public static bool SetExe(long code_auto, long number, long person_exe, string comment_exe)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî áðåíäó
			set_exe.Parameters["@code_auto"].Value			= code_auto;
			set_exe.Parameters["@number"].Value				= number;
			set_exe.Parameters["@person_exe"].Value			= person_exe;
			set_exe.Parameters["@comment_exe"].Value		= comment_exe;
			return DbSql.ExecuteCommandError(set_exe);
		}

		public static void SelectInArrayUnexe(ArrayList array, long code_auto)
		{
			// Ïîäãîòîâêà êîìàíäû ïîèñêà ïî ìàñêå
			select_unexe.Parameters["@code_auto"].Value = code_auto;
			DbSql.FillArray(array, select_unexe, new DbSql.DelegateMakeElement(MakeElement));
		}

		public static bool IsUnexe(long code_auto)
		{
			ArrayList array = new ArrayList();
			SelectInArrayUnexe(array, code_auto);
			if(array.Count > 0) return true;
			return false;
		}

		public static bool IsComments(long code_auto)
		{
			ArrayList array = new ArrayList();
			SelectInArray(array, code_auto);
			if(array.Count > 0) return true;
			return false;
		}

	}
}
