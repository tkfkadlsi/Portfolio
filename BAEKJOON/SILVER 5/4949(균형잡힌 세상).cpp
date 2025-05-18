#include <iostream>
#include <stack>
#include <string>
using namespace std;

int main()
{
	ios_base::sync_with_stdio(0);
	cin.tie(0);

	string s;
	stack<char> stChar;
	bool isYes;

	while (true)
	{
		while (!stChar.empty())
		{
			stChar.pop();
		}

		isYes = true;
		getline(cin, s);
		if (s == ".")
		{
			break;
		}

		for (int i = 0; i < s.size(); i++)
		{
			if (s[i] == '(' || s[i] == '[')
			{
				stChar.push(s[i]);
			}
			
			if (s[i] == ')')
			{
				if (!stChar.empty() && stChar.top() == '(')
				{
					stChar.pop();
				}
				else
				{
					isYes = false;
					break;
				}
			}

			if (s[i] == ']')
			{
				if (!stChar.empty() && stChar.top() == '[')
				{
					stChar.pop();
				}
				else
				{
					isYes = false;
					break;
				}
			}
		}

		if (isYes == true && stChar.empty())
		{
			cout << "yes" << '\n';
		}
		else
		{
			cout << "no" << '\n';
		}
	}

	return 0;
}