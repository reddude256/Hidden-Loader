using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Windows.Forms;
using System.Threading;
namespace JF_CrossFireMemoryEditor
{
    public class PrivilegeManager
    {
        [DllImport("kernel32.dll")]
        static extern bool TerminateThread(IntPtr hThread, uint dwExitCode);

        [DllImport("kernel32.dll")]
        static extern uint SuspendThread(IntPtr hThread);
        [DllImport("kernel32.dll")]
        static extern uint ResumeThread(IntPtr hThread);
        [Flags]
        public enum ThreadAccess : int
        {
            TERMINATE = (0x0001),
            SUSPEND_RESUME = (0x0002),
            GET_CONTEXT = (0x0008),
            SET_CONTEXT = (0x0010),
            SET_INFORMATION = (0x0020),
            QUERY_INFORMATION = (0x0040),
            SET_THREAD_TOKEN = (0x0080),
            IMPERSONATE = (0x0100),
            DIRECT_IMPERSONATION = (0x0200)
        }

        [DllImport("kernel32.dll")]
        static extern IntPtr OpenThread(ThreadAccess dwDesiredAccess, bool bInheritHandle,
           uint dwThreadId);
        [Flags]
        enum ProcessAccessFlags : uint
        {
            All = 0x1FFFFF,
            Terminate = 0x00000001,
            CreateThread = 0x00000002,
            VMOperation = 0x00000008,
            VMRead = 0x00000010,
            VMWrite = 0x00000020,
            DupHandle = 0x00000040,
            SetInformation = 0x00000200,
            QueryInformation = 0x00000400,
            Synchronize = 0x00100000
        }
        [DllImport("kernel32.dll")]
        static extern IntPtr OpenProcess(ProcessAccessFlags dwDesiredAccess, [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle, int dwProcessId);
        [DllImport("ntdll.dll")]
        private static extern IntPtr NtSuspendProcess(IntPtr handle);
        [DllImport("ntdll.dll")]
        private static extern IntPtr NtResumeProcess(IntPtr handle);
        [DllImport("advapi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool OpenProcessToken(IntPtr ProcessHandle,
            UInt32 DesiredAccess, out IntPtr TokenHandle);

        private static uint STANDARD_RIGHTS_REQUIRED = 0x000F0000;
        private static uint STANDARD_RIGHTS_READ = 0x00020000;
        private static uint TOKEN_ASSIGN_PRIMARY = 0x0001;
        private static uint TOKEN_DUPLICATE = 0x0002;
        private static uint TOKEN_IMPERSONATE = 0x0004;
        private static uint TOKEN_QUERY = 0x0008;
        private static uint TOKEN_QUERY_SOURCE = 0x0010;
        private static uint TOKEN_ADJUST_PRIVILEGES = 0x0020;
        private static uint TOKEN_ADJUST_GROUPS = 0x0040;
        private static uint TOKEN_ADJUST_DEFAULT = 0x0080;
        private static uint TOKEN_ADJUST_SESSIONID = 0x0100;
        private static uint TOKEN_READ = (STANDARD_RIGHTS_READ | TOKEN_QUERY);
        private static uint TOKEN_ALL_ACCESS = (STANDARD_RIGHTS_REQUIRED | TOKEN_ASSIGN_PRIMARY |
            TOKEN_DUPLICATE | TOKEN_IMPERSONATE | TOKEN_QUERY | TOKEN_QUERY_SOURCE |
            TOKEN_ADJUST_PRIVILEGES | TOKEN_ADJUST_GROUPS | TOKEN_ADJUST_DEFAULT |
            TOKEN_ADJUST_SESSIONID);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr GetCurrentProcess();

        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool LookupPrivilegeValue(string lpSystemName, string lpName,
            out LUID lpLuid);

        #region Privelege constants

        public const string SE_ASSIGNPRIMARYTOKEN_NAME = "SeAssignPrimaryTokenPrivilege";
        public const string SE_AUDIT_NAME = "SeAuditPrivilege";
        public const string SE_BACKUP_NAME = "SeBackupPrivilege";
        public const string SE_CHANGE_NOTIFY_NAME = "SeChangeNotifyPrivilege";
        public const string SE_CREATE_GLOBAL_NAME = "SeCreateGlobalPrivilege";
        public const string SE_CREATE_PAGEFILE_NAME = "SeCreatePagefilePrivilege";
        public const string SE_CREATE_PERMANENT_NAME = "SeCreatePermanentPrivilege";
        public const string SE_CREATE_SYMBOLIC_LINK_NAME = "SeCreateSymbolicLinkPrivilege";
        public const string SE_CREATE_TOKEN_NAME = "SeCreateTokenPrivilege";
        public const string SE_DEBUG_NAME = "SeDebugPrivilege";
        public const string SE_ENABLE_DELEGATION_NAME = "SeEnableDelegationPrivilege";
        public const string SE_IMPERSONATE_NAME = "SeImpersonatePrivilege";
        public const string SE_INC_BASE_PRIORITY_NAME = "SeIncreaseBasePriorityPrivilege";
        public const string SE_INCREASE_QUOTA_NAME = "SeIncreaseQuotaPrivilege";
        public const string SE_INC_WORKING_SET_NAME = "SeIncreaseWorkingSetPrivilege";
        public const string SE_LOAD_DRIVER_NAME = "SeLoadDriverPrivilege";
        public const string SE_LOCK_MEMORY_NAME = "SeLockMemoryPrivilege";
        public const string SE_MACHINE_ACCOUNT_NAME = "SeMachineAccountPrivilege";
        public const string SE_MANAGE_VOLUME_NAME = "SeManageVolumePrivilege";
        public const string SE_PROF_SINGLE_PROCESS_NAME = "SeProfileSingleProcessPrivilege";
        public const string SE_RELABEL_NAME = "SeRelabelPrivilege";
        public const string SE_REMOTE_SHUTDOWN_NAME = "SeRemoteShutdownPrivilege";
        public const string SE_RESTORE_NAME = "SeRestorePrivilege";
        public const string SE_SECURITY_NAME = "SeSecurityPrivilege";
        public const string SE_SHUTDOWN_NAME = "SeShutdownPrivilege";
        public const string SE_SYNC_AGENT_NAME = "SeSyncAgentPrivilege";
        public const string SE_SYSTEM_ENVIRONMENT_NAME = "SeSystemEnvironmentPrivilege";
        public const string SE_SYSTEM_PROFILE_NAME = "SeSystemProfilePrivilege";
        public const string SE_SYSTEMTIME_NAME = "SeSystemtimePrivilege";
        public const string SE_TAKE_OWNERSHIP_NAME = "SeTakeOwnershipPrivilege";
        public const string SE_TCB_NAME = "SeTcbPrivilege";
        public const string SE_TIME_ZONE_NAME = "SeTimeZonePrivilege";
        public const string SE_TRUSTED_CREDMAN_ACCESS_NAME = "SeTrustedCredManAccessPrivilege";
        public const string SE_UNDOCK_NAME = "SeUndockPrivilege";
        public const string SE_UNSOLICITED_INPUT_NAME = "SeUnsolicitedInputPrivilege";
        #endregion

        [StructLayout(LayoutKind.Sequential)]
        public struct LUID
        {
            public UInt32 LowPart;
            public Int32 HighPart;
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool CloseHandle(IntPtr hHandle);

        public const UInt32 SE_PRIVILEGE_ENABLED_BY_DEFAULT = 0x00000001;
        public const UInt32 SE_PRIVILEGE_ENABLED = 0x00000002;
        public const UInt32 SE_PRIVILEGE_REMOVED = 0x00000004;
        public const UInt32 SE_PRIVILEGE_USED_FOR_ACCESS = 0x80000000;

        [StructLayout(LayoutKind.Sequential)]
        public struct TOKEN_PRIVILEGES
        {
            public UInt32 PrivilegeCount;
            public LUID Luid;
            public UInt32 Attributes;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct LUID_AND_ATTRIBUTES
        {
            public LUID Luid;
            public UInt32 Attributes;
        }

        [DllImport("advapi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AdjustTokenPrivileges(IntPtr TokenHandle,
           [MarshalAs(UnmanagedType.Bool)]bool DisableAllPrivileges,
           ref TOKEN_PRIVILEGES NewState,
           UInt32 Zero,
           IntPtr Null1,
           IntPtr Null2);

        /// <summary>
        /// Меняет привилегию
        /// </summary>
        /// <param name="PID">ID процесса</param>
        /// <param name="privelege">Привилегия</param>
        public static void SetPrivilege(IntPtr PID, string privilege)
        {
            IntPtr hToken;
            LUID luidSEDebugNameValue;
            TOKEN_PRIVILEGES tkpPrivileges;

            if (!OpenProcessToken(PID, TOKEN_ADJUST_PRIVILEGES | TOKEN_QUERY, out hToken))
            {
                throw new Exception("Произошла ошибка при выполнении OpenProcessToken(). Код ошибки "
                    + Marshal.GetLastWin32Error());
            }

            if (!LookupPrivilegeValue(null, privilege, out luidSEDebugNameValue))
            {
                CloseHandle(hToken);
                throw new Exception("Произошла ошибка при выполнении LookupPrivilegeValue(). Код ошибки "
                    + Marshal.GetLastWin32Error());
            }

            tkpPrivileges.PrivilegeCount = 1;
            tkpPrivileges.Luid = luidSEDebugNameValue;
            tkpPrivileges.Attributes = SE_PRIVILEGE_ENABLED;

            if (!AdjustTokenPrivileges(hToken, false, ref tkpPrivileges, 0, IntPtr.Zero, IntPtr.Zero))
            {
                throw new Exception("Произошла ошибка при выполнении LookupPrivilegeValue(). Код ошибки :"
                    + Marshal.GetLastWin32Error());
            }
            CloseHandle(hToken);
        }
        /// <summary>
        /// Заморозить процесс
        /// </summary>
        /// <param name="process">Процесс для заморозки</param>
        public static bool SuspendProcess(Process process)
        {
            IntPtr handle = OpenProcess(ProcessAccessFlags.All, false, process.Id);
            if (handle.ToInt64() > 0)
            {
                NtSuspendProcess(handle);
                CloseHandle(handle);
                return true;

            }
            return false;
        }
        /// <summary>
        /// Разморозить процесс
        /// </summary>
        /// <param name="process">Процесс для разморозки</param>
        public static void ResumeProcess(Process process)
        {
            IntPtr handle = OpenProcess(ProcessAccessFlags.All, false, process.Id);
            NtResumeProcess(handle);
            CloseHandle(handle);
        }
        /// <summary>
        /// Заморозка потока у процесса
        /// </summary>
        /// <param name="pth"></param>
        public static bool SuspendProcessThread(ProcessThread pth)
        {
            IntPtr handle = OpenThread(ThreadAccess.SUSPEND_RESUME, false, (uint)pth.Id);
            if (handle.ToInt64() > 0)
            {
                SuspendThread(handle);
                CloseHandle(handle);
                return true;

            }
            return false;

        }
        /// <summary>
        /// Разморозка потока у процесса
        /// </summary>
        /// <param name="pth"></param>
        public static bool ResumeProcessThread(ProcessThread pth)
        {
            IntPtr handle = OpenThread(ThreadAccess.SUSPEND_RESUME, false, (uint)pth.Id);
            if (handle.ToInt64() > 0)
            {
                ResumeThread(handle);
                CloseHandle(handle);
                return true;

            }
            return false;

        }
        /// <summary>
        /// Убийство потока у процесса
        /// </summary>
        /// <param name="pth"></param>
        public static bool KillProcessThread(ProcessThread pth)
        {
            IntPtr handle = OpenThread(ThreadAccess.TERMINATE, false, (uint)pth.Id);
            if (handle.ToInt64() > 0)
            {
                TerminateThread(handle, 0);
                CloseHandle(handle);
                return true;

            }
            return false;

        }

        static public IntPtr GetThreadStartAddress(int threadId)
        {
            var hThread = OpenThread(ThreadAccess.QUERY_INFORMATION, false, (uint)threadId);
            if (hThread == IntPtr.Zero)
                return IntPtr.Zero;
            var buf = Marshal.AllocHGlobal(IntPtr.Size);
            try
            {
                var result = NtQueryInformationThread(hThread,
                                 ThreadInfoClass.ThreadQuerySetWin32StartAddress,
                                 buf, IntPtr.Size, IntPtr.Zero);
                if (result != 0)
                    return IntPtr.Zero;
                return Marshal.ReadIntPtr(buf);
            }
            finally
            {
                CloseHandle(hThread);
                Marshal.FreeHGlobal(buf);
            }
        }

        [DllImport("ntdll.dll", SetLastError = true)]
        static extern int NtQueryInformationThread(
            IntPtr threadHandle,
            ThreadInfoClass threadInformationClass,
            IntPtr threadInformation,
            int threadInformationLength,
            IntPtr returnLengthPtr);


        public enum ThreadInfoClass : int
        {
            ThreadQuerySetWin32StartAddress = 9
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern Int32 ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [Out] byte[] buffer, UInt32 size, out IntPtr lpNumberOfBytesRead);
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, uint nSize, out UIntPtr lpNumberOfBytesWritten);

        /// <summary>
        /// Найти и заменить байты
        /// </summary>
        /// <param name="prc">Процесс в котором нужно будет вести поиск, перед этим его лучша заморозить</param>
        /// <param name="find">Массив байтов, которые нужно найти</param>
        /// <param name="paste">Массив байтов, на которые нужно будет заменить</param>


        [DllImport("kernel32.dll")]
        static extern int VirtualQueryEx(IntPtr hProcess, IntPtr lpAddress, out MEMORY_BASIC_INFORMATION lpBuffer, uint dwLength);
        [StructLayout(LayoutKind.Sequential)]
        public struct MEMORY_BASIC_INFORMATION
        {
            public IntPtr BaseAddress;
            public IntPtr AllocationBase;
            public uint AllocationProtect;
            public IntPtr RegionSize;
            public uint State;
            public uint Protect;
            public uint Type;
        }
        public enum AllocationProtect : uint
        {
            PAGE_EXECUTE = 0x00000010,
            PAGE_EXECUTE_READ = 0x00000020,
            PAGE_EXECUTE_READWRITE = 0x00000040,
            PAGE_EXECUTE_WRITECOPY = 0x00000080,
            PAGE_NOACCESS = 0x00000001,
            PAGE_READONLY = 0x00000002,
            PAGE_READWRITE = 0x00000004,
            PAGE_WRITECOPY = 0x00000008,
            PAGE_GUARD = 0x00000100,
            PAGE_NOCACHE = 0x00000200,
            PAGE_WRITECOMBINE = 0x00000400
        }
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetModuleHandle(
            string lpModuleName
            );

        [DllImport("kernel32")]
        internal static extern Int32 WaitForSingleObject(
            IntPtr handle,
            Int32 milliseconds
            );
        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        static extern bool VirtualFreeEx(
            IntPtr hProcess,
            IntPtr lpAddress,
            UIntPtr dwSize,
            uint dwFreeType
            );

        [DllImport("kernel32.dll", CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern UIntPtr GetProcAddress(
            IntPtr hModule,
            string procName
            );

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        static extern IntPtr VirtualAllocEx(
            IntPtr hProcess,
            IntPtr lpAddress,
            uint dwSize,
            uint flAllocationType,
            uint flProtect
            );
        [DllImport("kernel32")]
        public static extern IntPtr CreateRemoteThread(
          IntPtr hProcess,
          IntPtr lpThreadAttributes,
          uint dwStackSize,
          UIntPtr lpStartAddress, // raw Pointer into remote process  
          IntPtr lpParameter,
          uint dwCreationFlags,
          out UIntPtr lpThreadId
        );
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool VirtualProtect(IntPtr lpAddress, uint dwSize,
           uint flNewProtect, out uint lpflOldProtect);
        public enum Protection : uint
        {
            PAGE_NOACCESS = 0x01,
            PAGE_READONLY = 0x02,
            PAGE_READWRITE = 0x04,
            PAGE_WRITECOPY = 0x08,
            PAGE_EXECUTE = 0x10,
            PAGE_EXECUTE_READ = 0x20,
            PAGE_EXECUTE_READWRITE = 0x40,
            PAGE_EXECUTE_WRITECOPY = 0x80,
            PAGE_GUARD = 0x100,
            PAGE_NOCACHE = 0x200,
            PAGE_WRITECOMBINE = 0x400
        }
        [DllImport("kernel32.dll")]
        static extern bool WriteProcessMemory(
            IntPtr hProcess,
            IntPtr lpBaseAddress,
            string lpBuffer,
            UIntPtr nSize,
            out IntPtr lpNumberOfBytesWritten
        );
        // CreateRemoteThread, since ThreadProc is in remote process, we must use a raw function-pointer.
        [DllImport("kernel32")]
        public static extern IntPtr CreateRemoteThread(
          IntPtr hProcess,
          IntPtr lpThreadAttributes,
          uint dwStackSize,
          IntPtr lpStartAddress, // raw Pointer into remote process
          IntPtr lpParameter,
          uint dwCreationFlags,
          out uint lpThreadId
        );
        [DllImport("kernel32.dll")]
        static extern bool GetExitCodeThread(IntPtr hThread, out uint lpExitCode);
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern UInt32 WaitForSingleObject(IntPtr hHandle, UInt32 dwMilliseconds);
        // Helper to wait for a thread to exit and print its exit code
        static void WaitForThreadToExit(IntPtr hThread)
        {
            WaitForSingleObject(hThread, UInt32.MaxValue);

            uint exitCode;
            GetExitCodeThread(hThread, out exitCode);
            int pid = Process.GetCurrentProcess().Id;
            Console.WriteLine("Pid {0}: Thread exited with code: {1}", pid, exitCode);
        }
        /*  public static IntPtr InjectThread(Process prc, MainForm.ThreadProc proc, bool wait=false)
          {
               IntPtr fp=Marshal.GetFunctionPointerForDelegate(proc);
               GC.KeepAlive(proc);
               IntPtr hProcess = OpenProcess(ProcessAccessFlags.All, false, prc.Id);
               uint dwThreadId;
               // Create a thread in the first process.
               IntPtr hThread = CreateRemoteThread(
                   hProcess,
                   IntPtr.Zero,
                   0,
                   fp, new IntPtr(666),
                   0,
                   out dwThreadId);
               if (wait)
               {
                   WaitForThreadToExit(hThread);
               }
               return hThread;
          }*/
        public static bool InjectDLL(Process process, String strDLLName)
        {
            IntPtr hProcess = OpenProcess(ProcessAccessFlags.All, false, process.Id);
            if (hProcess == null)
            {
                return false;
            }
            IntPtr bytesout;

            // Length of string containing the DLL file name +1 byte padding  
            Int32 LenWrite = strDLLName.Length + 1;
            // Allocate memory within the virtual address space of the target process  
            IntPtr AllocMem = (IntPtr)VirtualAllocEx(hProcess, (IntPtr)null, (uint)LenWrite, 0x3000, (uint)Protection.PAGE_READWRITE); //allocation pour WriteProcessMemory  
            uint tmp;
            VirtualProtect(AllocMem, (uint)LenWrite, (uint)Protection.PAGE_READWRITE, out tmp);
            // Write DLL file name to allocated memory in target process  
            WriteProcessMemory(hProcess, AllocMem, strDLLName, (UIntPtr)LenWrite, out bytesout);
            // Function pointer "Injector"  
            UIntPtr Injector = (UIntPtr)GetProcAddress(GetModuleHandle("kernel32.dll"), "LoadLibraryA");

            if (Injector == null)
            {
                MessageBox.Show(" Injector Error! \n ");
                // return failed  
                return false;
            }

            // Create thread in target process, and store handle in hThread  

            UIntPtr tmpptr;
            IntPtr hThread = (IntPtr)CreateRemoteThread(hProcess, (IntPtr)null, 0, Injector, AllocMem, 0, out tmpptr);

            // Make sure thread handle is valid  
            if (hThread == null)
            {
                //incorrect thread handle ... return failed  
                // MessageBox.Show(" hThread [ 1 ] Error! \n ");
                return false;
            }
            // Time-out is 10 seconds...  
            int Result = WaitForSingleObject(hThread, 3000);
            // Check whether thread timed out...  
            if (Result == 0x00000080L || Result == 0x00020102L || Result == 0xFFFFFFFF)
            {
                /* Thread timed out....... */
                // MessageBox.Show(" hThread [ 2 ] Error! \n ");
                // Make sure thread handle is valid before closing... prevents crashes.  
                if (hThread != null)
                {
                    //Close thread in target process  
                    CloseHandle(hThread);
                }
                return false;
            }
            // Sleep thread for 1 second  
            System.Threading.Thread.Sleep(100);
            // Clear up allocated space ( Allocmem )  
            VirtualFreeEx(hProcess, AllocMem, (UIntPtr)0, 0x8000);
            // Make sure thread handle is valid before closing... prevents crashes.  
            if (hThread != null)
            {
                //Close thread in target process  
                CloseHandle(hThread);
            }
            CloseHandle(hProcess);
            // return succeeded  
            return true;
        }

    }
}

