#include <iostream>
#include <string>
using namespace std;

int main()
{
	ios_base::sync_with_stdio(0);
	cin.tie(0);

	string s;
	int answer;

	for (int i = 0; i < 3; i++)
	{
		cin >> s;

		if (s[0] == 'F' || s[0] == 'B')
		{
			continue;
		}

		answer = stoi(s) + 3 - i;
		break;
	}

	if (answer % 15 == 0)
	{
		cout << "FizzBuzz";
	}
	else if (answer % 5 == 0)
	{
		cout << "Buzz";
	}
	else if (answer % 3 == 0)
	{
		cout << "Fizz";
	}
	else
	{
		cout << answer;
	}

	return 0;
}