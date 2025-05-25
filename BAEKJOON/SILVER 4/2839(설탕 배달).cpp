#include <iostream>
#include <unordered_map>
using namespace std;

unordered_map<int, int> iumap;

int PaperbagCount(int i)
{
	if (i < 3)
	{
		return -1;
	}

	if (i == 3 || i == 5)
	{
		return 1;
	}

	int minus3 = 0, minus5 = 0;

	if (iumap.find(i - 3) != iumap.end())
	{
		minus3 = iumap[i - 3];
	}
	else
	{
		minus3 = PaperbagCount(i - 3);
		if (minus3 != -1)
		{
			iumap[i - 3] = minus3;
		}
	}


	if (iumap.find(i - 5) != iumap.end())
	{
		minus5 = iumap[i - 5];
	}
	else
	{
		minus5 = PaperbagCount(i - 5);
		if (minus5 != -1)
		{
			iumap[i - 5] = minus5;
		}
	}

	if (minus3 == -1 && minus5 == -1)
	{
		return -1;
	}
	else if (minus3 == -1)
	{
		return minus5 + 1;
	}
	else if (minus5 == -1)
	{
		return minus3 + 1;
	}
	else
	{
		return (minus3 < minus5 ? minus3 : minus5) + 1;
	}
}

int main()
{
	ios_base::sync_with_stdio(0);
	cin.tie(0);

	int n;

	cin >> n;

	cout << PaperbagCount(n);

	return 0;
}