#include <iostream>
#include <Windows.h>
#include <TlHelp32.h>
#include <vector>

extern "C"
{
	static std::vector<PROCESSENTRY32> processes;

	__declspec(dllexport) void MakeSnapshot()
	{
		HANDLE snapshot = nullptr;
		snapshot = CreateToolhelp32Snapshot(TH32CS_SNAPPROCESS, 0);

		PROCESSENTRY32 entry = {};
		entry.dwSize = sizeof(entry);

		if (Process32First(snapshot, &entry))
		{
			do
			{
				processes.push_back(entry);
			} while (Process32Next(snapshot, &entry));
		}
		CloseHandle(snapshot);
	}

	__declspec(dllexport) int ParentProcessId(int processId)
	{
		int returnID = 0;

		for (auto currentProcess = processes.begin(); currentProcess != processes.end; currentProcess++)
		{
			if (processId == currentProcess->th32ProcessID)
			{
				returnID = currentProcess->th32ParentProcessID;
				break;
			}
		}

		return returnID;
	}
}