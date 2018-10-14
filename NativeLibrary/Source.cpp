#include <iostream>
#include <Windows.h>
#include <TlHelp32.h>

extern "C"
{
	__declspec(dllexport) int ParentProcessId(PROCESSENTRY32 process)
	{
		return process.th32ParentProcessID;
	}
}