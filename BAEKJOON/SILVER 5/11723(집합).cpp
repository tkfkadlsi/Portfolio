#include <iostream>
using namespace std;
	
bool iArray[21] = { 0 };

void AddCommand(int a)
{
	iArray[a] = 1;
}

void RemoveCommand(int a)
{
	iArray[a] = 0;
}

int CheckCommand(int a)
{
	return iArray[a];
}

void ToggleCommand(int a)
{
	iArray[a] = !iArray[a];
}

void AllCommand()
{
	for (int i = 1; i <= 20; i++)
	{
		iArray[i] = 1;
	}
}

void EmptyCommand()
{
	for (int i = 1; i <= 20; i++)
	{
		iArray[i] = 0;
	}
}

int main()
{
	ios_base::sync_with_stdio(0);
	cin.tie(0);


	int m, inputInt;
	string inputStr;

	cin >> m;

	for (int i = 0; i < m; i++)
	{
		cin >> inputStr;

		if (inputStr == "add")
		{
			cin >> inputInt;
			AddCommand(inputInt);
		}
		else if (inputStr == "remove")
		{
			cin >> inputInt;
			RemoveCommand(inputInt);
		}
		else if (inputStr == "check")
		{
			cin >> inputInt;
			cout << CheckCommand(inputInt) << '\n';
		}
		else if (inputStr == "toggle")
		{
			cin >> inputInt;
			ToggleCommand(inputInt);
		}
		else if (inputStr == "all")
		{
			AllCommand();
		}
		else if (inputStr == "empty")
		{
			EmptyCommand();
		}
	}

	return 0;
}