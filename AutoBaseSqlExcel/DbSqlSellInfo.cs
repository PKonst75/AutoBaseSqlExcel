using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Collections;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbSqlSellInfo.
	/// </summary>
	public class DbSqlSellInfo
	{
		public static SqlCommand		insert;
		public static SqlCommand		find;

		public DbSqlSellInfo()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public static void Init(SqlConnection connection)
		{
			insert = new SqlCommand("����������_�������_����������_����������", connection);
			insert.Parameters.Add("@code_sell", SqlDbType.BigInt);
			insert.Parameters.Add("@code_position", SqlDbType.BigInt);
			insert.Parameters.Add("@code_reklama", SqlDbType.BigInt);
			insert.Parameters.Add("@flag_credit_inner", SqlDbType.Bit);
			insert.Parameters.Add("@flag_credit_outer", SqlDbType.Bit);
			insert.Parameters.Add("@flag_lising", SqlDbType.Bit);
			insert.Parameters.Add("@flag_cashless", SqlDbType.Bit);
			insert.Parameters.Add("@flag_partner", SqlDbType.Bit);
			insert.Parameters.Add("@flag_util", SqlDbType.Bit);
            insert.Parameters.Add("@flag_tin", SqlDbType.Bit);
            insert.Parameters.Add("@tinprice", SqlDbType.BigInt);
			insert.CommandType = CommandType.StoredProcedure;
			DbSql.SetReturnError(insert);

			find = new SqlCommand("����������_�������_����������_�����", connection);
			find.Parameters.Add("@code_sell", SqlDbType.BigInt);
			find.CommandType = CommandType.StoredProcedure;
		}

		public static bool Insert(CS_SellInfo element)
		{
			// ���������� ������� ������ �� ������
			insert.Parameters["@code_sell"].Value			= element.code_sell;
			insert.Parameters["@code_position"].Value		= element.code_position;
			insert.Parameters["@code_reklama"].Value		= element.code_reklama;
			insert.Parameters["@flag_credit_inner"].Value	= element.flag_credit_inner;
			insert.Parameters["@flag_credit_outer"].Value	= element.flag_credit_outer;
			insert.Parameters["@flag_lising"].Value			= element.flag_lising;
			insert.Parameters["@flag_cashless"].Value		= element.flag_cashless;
			insert.Parameters["@flag_partner"].Value		= element.flag_partner;
			insert.Parameters["@flag_util"].Value			= element.flag_util;
            insert.Parameters["@flag_tin"].Value = element.flag_tin;
            insert.Parameters["@tinprice"].Value = element.tinprice;
			
			return DbSql.ExecuteCommandError(insert);
		}

		public static object MakeElement(SqlDataReader reader)
		{
			CS_SellInfo element			= new CS_SellInfo();
			element.code_sell			= DbSql.GetValueLong(reader, "���_�������");
			element.code_position		= DbSql.GetValueLong(reader, "��������������");
			element.code_reklama		= DbSql.GetValueLong(reader, "�������");
			element.flag_credit_inner	= DbSql.GetValueBool(reader, "����_������_���������");
			element.flag_credit_outer	= DbSql.GetValueBool(reader, "����_������_�������");
			element.flag_lising			= DbSql.GetValueBool(reader, "����_������");
			element.flag_cashless		= DbSql.GetValueBool(reader, "����_�����������");
			element.flag_partner		= DbSql.GetValueBool(reader, "����_�������");
			element.flag_util			= DbSql.GetValueBool(reader, "����_����������");
            element.flag_tin = DbSql.GetValueBool(reader, "����_�������");
            element.tinprice = DbSql.GetValueLong(reader, "���������_�������");
			
			return (object)element;
		}

		public static CS_SellInfo Find(long code_sell)
		{
			find.Parameters["@code_sell"].Value	= (long)code_sell;
			return (CS_SellInfo)DbSql.Find(find, new DbSql.DelegateMakeElement(MakeElement));
		}
	}
}
