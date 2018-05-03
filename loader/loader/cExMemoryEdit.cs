using System;
using System.Runtime.InteropServices;
using System.Diagnostics;

public class cExMemoryEdit
{
    public cExMemoryEdit(string pProcessName)
    {
        this.pName = pProcessName;
    }
    [DllImport("kernel32.dll")]
    private static extern bool VirtualProtectEx(IntPtr hProcess, IntPtr lpAddress, UIntPtr dwSize, uint flNewProtect, out uint lpflOldProtect);
    [DllImport("kernel32.dll")]
    private static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, uint nSize, uint lpNumberOfBytesWritten);
    [DllImport("kernel32.dll")]
    private static extern IntPtr OpenProcess(uint dwDesiredAccess, bool bInheritHandle, int dwProcessId);
    uint PAGE_READWRITE = 4;
    uint PROCESS_ALL_ACCESS = 0x1f0fff;
    public string pName { get; set; }
    private Process[] m_pProcess;
    private IntPtr pHandle;
    public bool GetProcess()
    {
        m_pProcess = Process.GetProcessesByName(pName);
        if (m_pProcess.Length != 0)
        {
            pHandle = OpenProcess(PROCESS_ALL_ACCESS, false, m_pProcess[0].Id);
            if (pHandle != IntPtr.Zero)
                return true;
            else
                return false;
        }
        return false;
    }
    public bool ExWriteMemory(IntPtr dAdress, byte[] pBytes)
    {
        if (pHandle == IntPtr.Zero)
            GetProcess();
        uint flNewProtect;
        VirtualProtectEx(pHandle, dAdress, (UIntPtr)((ulong)((long)pBytes.Length)), PAGE_READWRITE, out flNewProtect);
        bool flag = WriteProcessMemory(pHandle, dAdress, pBytes, (uint)pBytes.Length, 0u);
        VirtualProtectEx(pHandle, dAdress, (UIntPtr)((ulong)((long)pBytes.Length)), flNewProtect, out flNewProtect);
        return flag;
    }
    public void OpenWarFace()
    {
        try
        {
            Process.Start("mailrugames://play/0.1177");
        }
        catch { }
    }





}

