using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
namespace WarFuck
{
	public class SigScan
	{
		private byte[] byte_0;
		private int int_0;
		private IntPtr intptr_0;
		private Process process_0;
		public IntPtr Address
		{
			get
			{
				return this.intptr_0;
			}
			set
			{
				this.intptr_0 = value;
			}
		}
		public Process Process
		{
			get
			{
				return this.process_0;
			}
			set
			{
				this.process_0 = value;
			}
		}
		public SigScan()
		{
			this.process_0 = null;
			this.intptr_0 = IntPtr.Zero;
			this.int_0 = 0;
			this.byte_0 = null;
		}
		public SigScan(Process proc, IntPtr addr, int size)
		{
			this.process_0 = proc;
			this.intptr_0 = addr;
			this.int_0 = size;
		}
		public IntPtr FindPattern(byte[] btPattern, string strMask, int nOffset)
		{
			IntPtr result;
			try
			{
				IntPtr zero;
				if ((this.byte_0 != null && this.byte_0.Length != 0) || this.method_0())
				{
					if (strMask.Length != btPattern.Length)
					{
						zero = IntPtr.Zero;
						return zero;
					}
					for (int i = 0; i < this.byte_0.Length; i++)
					{
						if (this.method_1(i, btPattern, strMask))
						{
							result = new IntPtr((int)this.intptr_0 + (i + nOffset));
							return result;
						}
					}
				}
				zero = IntPtr.Zero;
				return zero;
			}
			catch (Exception)
			{
				result = IntPtr.Zero;
			}
			return result;
		}
		private bool method_1(int int_1, byte[] byte_1, string string_0)
		{
			for (int i = 0; i < byte_1.Length; i++)
			{
				if (string_0[i] != '?' && string_0[i] == 'x' && byte_1[i] != this.byte_0[int_1 + i])
				{
					return false;
				}
			}
			return true;
		}
		private bool method_0()
		{
			bool result;
			try
			{
				if (this.process_0 == null)
				{
					result = false;
				}
				else
				{
					if (this.process_0.HasExited)
					{
						result = false;
					}
					else
					{
						if (this.intptr_0 == IntPtr.Zero)
						{
							result = false;
						}
						else
						{
							if (this.int_0 == 0)
							{
								result = false;
							}
							else
							{
								this.byte_0 = new byte[this.int_0];
								int num = 0;
								if (!SigScan.ReadProcessMemory(this.process_0.Handle, this.intptr_0, this.byte_0, this.int_0, out num) || num != this.int_0)
								{
									result = false;
								}
								else
								{
									result = true;
								}
							}
						}
					}
				}
			}
			catch (Exception)
			{
				result = false;
			}
			return result;
		}
		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern bool ReadProcessMemory(IntPtr intptr_1, IntPtr intptr_2, [Out] byte[] byte_1, int int_1, out int int_2);
		public void ResetRegion()
		{
			this.byte_0 = null;
		}
	}
}
