using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for ExSerial.
	/// </summary>
	public class ExSerial
	{
		ListView connected_list;
		public struct SECURITY_ATTRIBUTES 
		{ 
			long  nLength; 
			object lpSecurityDescriptor; 
			bool   bInheritHandle; 
		};

		struct DCB 
		{ 
			public uint DCBlength; 
			public uint BaudRate; 
			public uint fBinary; 
			public uint fParity; 
			public uint fOutxCtsFlow; 
			public uint fOutxDsrFlow; 
			public uint fDtrControl; 
			public uint fDsrSensitivity; 
			public uint fTXContinueOnXoff; 
			public uint fOutX; 
			public uint fInX; 
			public uint fErrorChar; 
			public uint fNull; 
			public uint fRtsControl; 
			public uint fAbortOnError; 
			public uint fDummy2; 
			public ushort wReserved; 
			public ushort XonLim; 
			public ushort XoffLim; 
			public byte ByteSize; 
			public byte Parity; 
			public byte StopBits; 
			public char XonChar; 
			public char XoffChar; 
			public char ErrorChar; 
			public char EofChar; 
			public char EvtChar; 
			public ushort wReserved1; 
		}; 

		public struct COMSTAT 
		{ 
			public uint fCtsHold; 
			public uint fDsrHold; 
			public uint fRlsdHold; 
			public uint fXoffHold; 
			public uint fXoffSent; 
			public uint fEof; 
			public uint fTxim; 
			public uint fReserved; 
			public uint cbInQue; 
			public uint cbOutQue; 
		};

		public static string comm_data;

 
		[DllImport("KERNEL32.DLL")]
		public static unsafe extern IntPtr CreateFile(
											 string lpFileName,                         // file name
											 uint dwDesiredAccess,                      // access mode
											 uint dwShareMode,                          // share mode
											 void* lpSecurityAttributes, // SD
											 uint dwCreationDisposition,                // how to create
											 uint dwFlagsAndAttributes,                 // file attributes
											 IntPtr hTemplateFile                        // handle to template file
											 );
		[DllImport("KERNEL32.DLL")]
		public static extern bool CloseHandle(IntPtr hObject);

		[DllImport("KERNEL32.DLL")]
		public static extern uint GetLastError();

		[DllImport("KERNEL32.DLL")]
		public static extern unsafe bool ReadFile(
				IntPtr hFile,               // handle to file
				void* lpBuffer,             // data buffer
				int nNumberOfBytesToRead,  // number of bytes to read
				int* lpNumberOfBytesRead,	// number of bytes read
				int lpOverlapped    // overlapped buffer
				);
		[DllImport("KERNEL32.DLL")]
		public static extern unsafe bool ClearCommError(
			IntPtr hFile,     // handle to communications device
			int* lpErrors, // error codes
			void* lpStat  // communications status
			);
		[DllImport("KERNEL32.DLL")]
		public static extern unsafe bool GetCommState(
			IntPtr hFile,     // handle to communications device
			void* lpDCB  // communications status
			);

		public ExSerial(ListView list)
		{
			connected_list = list;
		}
		public  void SerialOpen(string port)
		{
			const uint GENERIC_READ			= 0x80000000;
			const uint GENERIC_WRITE		= 0x40000000;
			const uint FILE_FLAG_OVERLAPPED = 0x40000000;
			const uint OPEN_EXISTING = 3;
			
			IntPtr INVALID_HANDLE_VALUE = new IntPtr(-1);
			IntPtr NULL_HANDLE = new IntPtr(0);
			IntPtr hComm;
			unsafe
			{
				hComm = CreateFile( port,  
					GENERIC_READ | GENERIC_WRITE, 
					0, 
					null, 
					OPEN_EXISTING,
					0,//FILE_FLAG_OVERLAPPED,
					NULL_HANDLE);
			}
			if (hComm == INVALID_HANDLE_VALUE)
			{
				uint err = GetLastError();
				MessageBox.Show("НЕ OK = " + err.ToString());
				return;
			}
			// error opening port; abort
			// Пытаемся прочитать COM порт
			byte[] com_data = new byte[1000];
			COMSTAT[] com_stat = new COMSTAT[1];
			DCB dcb;// = new DCB[1];
			int READ_BUF_SIZE = 1000;
			int dwRead = 0;
			int dwReadi = 0;
			bool res = true;
			unsafe
			{
				/*
				fixed(void* p1 = com_stat)
				{
					res = ClearCommError(hComm, &dwReadi, p1);
				}
				if(res==false)
				{	
					uint err = GetLastError();
					MessageBox.Show("НЕ OK 2 =" + err.ToString());
					CloseHandle(hComm);
					return;
				}
				MessageBox.Show("ОСТАЛОСЬ ПРОЧИТАТЬ БАЙТ " + com_stat[0].cbInQue.ToString() + " out " + com_stat[0].cbOutQue.ToString());
				*/
				res = true;
				//fixed(void* p2 = dcb)
				{
					dcb.DCBlength = 80;
					res = GetCommState(hComm, &dcb);//p2);
				
					if(res==false)
					{	
						uint err = GetLastError();
						MessageBox.Show("НЕ OK 2 =" + err.ToString());
						CloseHandle(hComm);
						return;
					}
					// Данные о настройке порта
					ListViewItem itm = connected_list.Items.Add("DCBlength");
					itm.SubItems.Add(dcb.DCBlength.ToString());
					itm = connected_list.Items.Add("BaudRate");
					itm.SubItems.Add(dcb.BaudRate.ToString());
					itm = connected_list.Items.Add("fBinary");
					itm.SubItems.Add(dcb.fBinary.ToString());
					itm = connected_list.Items.Add("fParity");
					itm.SubItems.Add(dcb.fParity.ToString());
					itm = connected_list.Items.Add("fOutxCtsFlow");
					itm.SubItems.Add(dcb.fOutxCtsFlow.ToString());
					itm = connected_list.Items.Add("fOutxDsrFlow");
					itm.SubItems.Add(dcb.fOutxDsrFlow.ToString());
					itm = connected_list.Items.Add("fDtrControl");
					itm.SubItems.Add(dcb.fDtrControl.ToString());
					itm = connected_list.Items.Add("fDsrSensitivity");
					itm.SubItems.Add(dcb.fDsrSensitivity.ToString());
					itm = connected_list.Items.Add("fTXContinueOnXoff");
					itm.SubItems.Add(dcb.fTXContinueOnXoff.ToString());
					itm = connected_list.Items.Add("fOutX");
					itm.SubItems.Add(dcb.fOutX.ToString());
					itm = connected_list.Items.Add("fInX");
					itm.SubItems.Add(dcb.fInX.ToString());
				}
				// MessageBox.Show("СКОРОСТЬ ОБМЕНА " + dcb[0].BaudRate.ToString());
				res = true;
				string str = "";
				fixed(void* p = com_data)
				{
					res = ReadFile(hComm, p, READ_BUF_SIZE, &dwRead, 0);
					byte[] bytedata = new byte[dwRead];
					// Обработка данных побитовая
					for (int j=0;j < dwRead; j++)
					{
						byte code = (byte)((byte*)p)[j];
						bytedata[j] = code;
						//byte mask = 15;
						//code = (byte)(mask & code);
						//string chr = code.ToString();
						//str += chr + " ";
					}
					str = System.Text.Encoding.ASCII.GetString(bytedata, 0, dwRead);
				}
				if(res==false)
				{	
					uint err = GetLastError();
					MessageBox.Show("НЕ OK 3 =" + err.ToString());
					CloseHandle(hComm);
					return;
				}
				MessageBox.Show("ПРОЧЛИ БАЙТ " + dwRead.ToString());
				// А теперь посмотрим эти байты:
				MessageBox.Show(str);
			}
			
			CloseHandle(hComm);		// Закрыли открытый COM порт
			// Заполнение внешнего листа
			//for(int i = 0; i < 100; i++)
			//{
			//	ListViewItem itm = connected_list.Items.Add(i.ToString());
			//	itm.SubItems.Add(com_data[i].ToString());
			//}
			//MessageBox.Show("OK = " + com_stat[0].cbInQue.ToString());
			//comm_data = new String((char[])com_data);
			//comm_data = com_data[9].ToString() + "|";
			//MessageBox.Show("OK|" + comm_data + "|");
		}
	}
}
