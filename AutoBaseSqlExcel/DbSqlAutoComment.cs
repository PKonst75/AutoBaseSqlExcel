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
			insert = new SqlCommand("����������_����������_����������", connection);
			insert.Parameters.Add("@code_auto", SqlDbType.BigInt);
			insert.Parameters.Add("@number", SqlDbType.BigInt);
			insert.Parameters.Add("@comment", SqlDbType.VarChar);
			insert.CommandType = CommandType.StoredProcedure;
			insert.Parameters["@number"].Direction = ParameterDirection.Output;
			DbSql.SetReturnError(insert);

			select = new SqlCommand("����������_����������_�������", connection);
			select.Parameters.Add("@code_auto", SqlDbType.BigInt);
			select.CommandType = CommandType.StoredProcedure;

			select_unexe = new SqlCommand("����������_����������_�������_�������������", connection);
			select_unexe.Parameters.Add("@code_auto", SqlDbType.BigInt);
			select_unexe.CommandType = CommandType.StoredProcedure;

			set_executable = new SqlCommand("����������_����������_����������_�����������", connection);
			set_executable.Parameters.Add("@code_auto", SqlDbType.BigInt);
			set_executable.Parameters.Add("@number", SqlDbType.BigInt);
			set_executable.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(set_executable);

			unset_executable = new SqlCommand("����������_����������_�����_�����������", connection);
			unset_executable.Parameters.Add("@code_auto", SqlDbType.BigInt);
			unset_executable.Parameters.Add("@number", SqlDbType.BigInt);
			unset_executable.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(unset_executable);

			delete = new SqlCommand("����������_����������_��������", connection);
			delete.Parameters.Add("@code_auto", SqlDbType.BigInt);
			delete.Parameters.Add("@number", SqlDbType.BigInt);
			delete.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(delete);

			set_exe = new SqlCommand("����������_����������_����������_����������", connection);
			set_exe.Parameters.Add("@code_auto", SqlDbType.BigInt);
			set_exe.Parameters.Add("@number", SqlDbType.BigInt);
			set_exe.Parameters.Add("@person_exe", SqlDbType.BigInt);
			set_exe.Parameters.Add("@comment_exe", SqlDbType.VarChar);
			set_exe.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(set_exe);

		}

		public static long Insert(long code_auto, string comment)
		{
			// ���������� ������� ������ �� ������
			insert.Parameters["@code_auto"].Value		= (long)code_auto;
			insert.Parameters["@comment"].Value			= (string)comment;
			if(DbSql.ExecuteCommandError(insert) == false) return 0;
			long number	= (long)insert.Parameters["@number"].Value;
			return number;
		}

		public static object MakeElement(SqlDataReader reader)
		{
			DtAutoComment element		= new DtAutoComment();
			element.SetData("���_����������", DbSql.GetValueLong(reader, "���_����������"));
			element.SetData("�����", DbSql.GetValueLong(reader, "�����"));
			element.SetData("����������", DbSql.GetValueString(reader, "����������"));
			element.SetData("����_����������", DbSql.GetValueDate(reader, "����_����������"));
			element.SetData("��������", DbSql.GetValueLong(reader, "��������"));
			element.SetData("���������_����������", DbSql.GetValueBool(reader, "���������_����������"));
			element.SetData("����������_����������", DbSql.GetValueString(reader, "����������_����������"));
			element.SetData("��������_���", DbSql.GetValueString(reader, "��������_���"));
			return (object)element;
		}

		public static void SelectInArray(ArrayList array, long code_auto)
		{
			// ���������� ������� ������ �� �����
			select.Parameters["@code_auto"].Value = code_auto;
			DbSql.FillArray(array, select, new DbSql.DelegateMakeElement(MakeElement));
		}

		public static bool SetExecutable(long code_auto, long number)
		{
			// ���������� ������� ������ �� ������
			set_executable.Parameters["@code_auto"].Value			= code_auto;
			set_executable.Parameters["@number"].Value				= number;
			return DbSql.ExecuteCommandError(set_executable);
		}

		public static bool Delete(long code_auto, long number)
		{
			// ���������� ������� ������ �� ������
			delete.Parameters["@code_auto"].Value			= code_auto;
			delete.Parameters["@number"].Value				= number;
			return DbSql.ExecuteCommandError(delete);
		}

		public static bool UnsetExecutable(long code_auto, long number)
		{
			// ���������� ������� ������ �� ������
			unset_executable.Parameters["@code_auto"].Value			= code_auto;
			unset_executable.Parameters["@number"].Value			= number;
			return DbSql.ExecuteCommandError(unset_executable);
		}

		public static bool SetExe(long code_auto, long number, long person_exe, string comment_exe)
		{
			// ���������� ������� ������ �� ������
			set_exe.Parameters["@code_auto"].Value			= code_auto;
			set_exe.Parameters["@number"].Value				= number;
			set_exe.Parameters["@person_exe"].Value			= person_exe;
			set_exe.Parameters["@comment_exe"].Value		= comment_exe;
			return DbSql.ExecuteCommandError(set_exe);
		}

		public static void SelectInArrayUnexe(ArrayList array, long code_auto)
		{
			// ���������� ������� ������ �� �����
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
