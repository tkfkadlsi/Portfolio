#include <iostream>
#include <queue>
using namespace std;

queue<int> qInt;

void Command(string s)
{
	if (s == "push")
	{
		int input;
		cin >> input;
		qInt.push(input);
	}
	if (s == "pop")
	{
		if (qInt.empty())
		{
			cout << -1 << '\n';
		}
		else
		{
			cout << qInt.front() << '\n';
			qInt.pop();
		}
	}
	if (s == "size")
	{
		cout << qInt.size() << '\n';
	}
	if (s == "empty")
	{
		if (qInt.empty())
		{
			cout << 1 << '\n';
		}
		else
		{
			cout << 0 << '\n';
		}
	}
	if (s == "front")
	{
		if (qInt.empty())
		{
			cout << -1 << '\n';
		}
		else
		{
			cout << qInt.front() << '\n';
		}
	}
	if (s == "back")
	{
		if (qInt.empty())
		{
			cout << -1 << '\n';
		}
		else
		{
			cout << qInt.back() << '\n';
		}
	}
}

int main()
{
	ios_base::sync_with_stdio(0);
	cin.tie(0);

	int i;
	string s;

	cin >> i;

	for (; i > 0; i--)
	{
		cin >> s;
		Command(s);
	}

	return 0;
}