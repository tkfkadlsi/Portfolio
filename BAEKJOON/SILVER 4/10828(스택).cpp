#include <iostream>
#include <stack>
using namespace std;

stack<int> sInt;

void Command(string s)
{
	if (s == "push")
	{
		int input;
		cin >> input;
		sInt.push(input);
	}
	if (s == "pop")
	{
		if (sInt.empty())
		{
			cout << -1 << '\n';
		}
		else
		{
			cout << sInt.top() << '\n';
			sInt.pop();
		}
	}
	if (s == "size")
	{
		cout << sInt.size() << '\n';
	}
	if (s == "empty")
	{
		if (sInt.empty())
		{
			cout << 1 << '\n';
		}
		else
		{
			cout << 0 << '\n';
		}
	}
	if (s == "top")
	{
		if (sInt.empty())
		{
			cout << -1 << '\n';
		}
		else
		{
			cout << sInt.top() << '\n';
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