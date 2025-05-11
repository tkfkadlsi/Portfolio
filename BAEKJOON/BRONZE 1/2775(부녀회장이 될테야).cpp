#include <iostream>
#include <vector>
using namespace std;

int array[15][15] = { 0 };

int HumanCount(int k, int n)
{
	int sum = 0;

	if (array[k][n] != 0)
	{
		return array[k][n];
	}

	if (k > 0)
	{
		for (int i = 1; i <= n; i++)
		{
			sum += HumanCount(k - 1, i);
		}
	}
	else
	{
		sum += n;
	}

	array[k][n] = sum;

	return sum;
}

int main()
{
	ios_base::sync_with_stdio(0);
	cin.tie(0);

	int t, k, n;

	cin >> t;

	for (int i = 0; i < t; i++)
	{
		cin >> k >> n;

		cout << HumanCount(k, n) << '\n';
	}
	
	return 0;
}