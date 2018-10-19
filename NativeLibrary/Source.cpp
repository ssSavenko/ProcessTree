#include <iostream>
#include <Windows.h>
#include <TlHelp32.h>

extern "C"
{
	static HANDLE snapshot;

	__declspec(dllexport) void MakeSnapshot()
	{
		snapshot = nullptr;
		snapshot = CreateToolhelp32Snapshot(TH32CS_SNAPPROCESS, 0);
	}

	__declspec(dllexport) int ParentProcessId(int processId)
	{
		int returnID = 0;

		PROCESSENTRY32 entry = {};
		entry.dwSize = sizeof(entry);

		if (Process32First(snapshot, &entry))
		{
			do
			{
				if (processId == entry.th32ProcessID)
				{
					returnID = entry.th32ParentProcessID;
					break;
				}
			} while (Process32Next(snapshot, &entry));
		}

		return returnID;
	}
}