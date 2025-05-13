#include <iostream>
using namespace std;

int plusArray[1000001] = { 0 }, minusArray[1000001] = { 0 };

int main()
{
	ios_base::sync_with_stdio(0);
	cin.tie(0);

	int n, input;
	
	cin >> n;

	for (int i = 0; i < n; i++)
	{
		cin >> input;
		if (input < 0)
		{
			minusArray[-input]++;
		}
		else
		{
			plusArray[input]++;
		}
	}

	for (int i = 1000000; i > 0; i--)
	{
		if (minusArray[i] != 0)
		{
			cout << -i << '\n';
		}
	}

	for (int i = 0; i < 1000001; i++)
	{
		if (plusArray[i] != 0)
		{
			cout << i << '\n';
		}
	}

	return 0;
}